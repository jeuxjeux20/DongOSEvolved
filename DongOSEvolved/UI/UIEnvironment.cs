using Cosmos.HAL;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.UI
{
    public class UIEnvironment
    {
        public UICanvas Canvas { get; }
        public static Mouse Mouse { get; private set; }
        public List<UIElement> Elements { get; } = new List<UIElement>();
        public UIEnvironment(UICanvas c)
        {
            Canvas = c;
        }
        public bool Started { get; private set; }
        public void Start()
        { 
            Mouse = new Mouse();          
            Mouse.Initialize((uint)Canvas.Mode.Columns, (uint)Canvas.Mode.Rows);
            Started = true;
            AddBackground();
            AddBottomBar();
            while (true)
            {
                MouseRoutine();
            }
        }
        private void DrawAllElements() // rip this method because of our QUADRUPLE-BUFFERING SCIENCE DRAWING BOARRRD
        {
            Kernel.PrintDebug("Drawing everything...");
            // Quicksort(elements, 0, elements.Count - 1);
            foreach (var item in Elements)
            {
                Kernel.PrintDebug("Drawing item in foreach...");
                item.Draw();
            }
        }
        public void AddBackground()
        {
            AddUIElement(new BackgroundUIElement(this));
        }
        public void AddUIElement(UIElement element)
        {
            if (!Started)
            {
                throw new InvalidOperationException("The environment has not started yet.");
            } 
            Elements.Add(element);  
        }
        private void AddBottomBar()
        {
            Cosmos.System.Kernel.PrintDebug("BottomBar added");
            AddUIElement(new DelegateUIElement(this, c =>
            {
                var height = 30;
                c.DrawFilledRectangle(new Pen(Color.Blue), 0, Canvas.Mode.Rows - height, Canvas.Mode.Columns, height);
            }));
        }
        private void AddTV()
        {
            Elements.Add(new DelegateUIElement(this, c => { c.DrawImage(Images.TV, 200, 300); }) );
        }
        private List<Pixel> lastMousePixels = new List<Pixel>();
        int lastMouseX = -1;
        int lastMouseY = -1;
        private void MouseRoutine()
        {
            // If this is the first time we draw it :
            if (lastMouseX == -1)
            {
                SetMouse();
                return;
            }
            // If it changed position from yesterday... hmmm sorry last time.
            if ((Mouse.X != lastMouseX || Mouse.X != lastMouseY))
            {
                Kernel.PrintDebug("hmmm mouse changed");
                var newer = DrawMouse();
                List<Pixel> pixels = newer.GetMissingElements(lastMousePixels);
                Kernel.PrintDebug("newer length : " + newer.Count + " lastMoustPixels count : " + lastMousePixels.Count);
                foreach (var item in pixels)
                {
                    Kernel.PrintDebug("oWo there is something MISSISISISNG");
                    var onScreen = Canvas.PixelsLocation[item.Coords.X][item.Coords.Y];
                    int count = onScreen.Pixels.Count;
                    if (count >= 2)
                    {
                        Color color = onScreen.Pixels[count - 1].Color;
                        Kernel.PrintDebug("Count more than two. i don't care lol");
                        Canvas.DrawPoint(new Pen(color), item.Coords);
                    }
                    else if (count >= 1)
                    {
                        Color color = onScreen.Pixels[count].Color;
                        Kernel.PrintDebug("Count more than one, behind color's red level : " + color.R);
                        Canvas.DrawPoint(new Pen(color), item.Coords);
                    }
                    else
                    {
                        Kernel.PrintDebug("wtf there is nothing behind me.");
                    }
                    SetMouse();
                }
            }
        }

        private void SetMouse()
        {
            lastMousePixels = DrawMouse();
            lastMouseX = Mouse.X;
            lastMouseY = Mouse.Y;
        }

        private List<Pixel> DrawMouse()
        {
            return Canvas.ExecuteWhileRecording(c => c.DrawFilledRectangle(new Pen(Color.Black), Mouse.X, Mouse.Y, 2, 2));
        }
    }
}
