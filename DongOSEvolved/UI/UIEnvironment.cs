using Cosmos.HAL;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.UI
{
    public class UIEnvironment
    {
        private readonly Canvas canvas;
        private Mouse mouse;
        private readonly List<UIElement> elements = new List<UIElement>();
        public UIEnvironment(Canvas c)
        {
            canvas = c;
        }
        public bool Started { get; private set; }
        public void Start()
        {
            mouse = new Mouse();          
            mouse.Initialize((uint)canvas.Mode.Columns, (uint)canvas.Mode.Rows);
            Started = true;
            AddBottomBar();
            while (true)
            {
                int iteration = 1;
                Kernel.PrintDebug("Drawing iteration : " + iteration);
                iteration++;
                // var time = DateTime.Now;
                canvas.Clear(Color.White);
                DrawAllElements();
                MouseRoutine();
                System.Threading.Thread.Sleep(33 /* - time.Millisecond */); // 30 fps cap
                // For some reason timespan doesn´t work because it converts to string for no reason :( 
            }
        }
        // found on internet :v
        private static void Quicksort(List<UIElement> elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    UIElement tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }
        private void DrawAllElements()
        {
            Kernel.PrintDebug("Drawing everything...");
            // Quicksort(elements, 0, elements.Count - 1);
            foreach (var item in elements)
            {
                Kernel.PrintDebug("Drawing item in foreach...");
                item.Draw(canvas);
            }
        }

        public void AddUIElement(UIElement element)
        {
            if (!Started)
            {
                throw new InvalidOperationException("The environment has not started yet.");
            }
            elements.Add(element);
        }
        private void AddBottomBar()
        {
            Cosmos.System.Kernel.PrintDebug("BottomBar added");
            AddUIElement(new DelegateUIElement(c =>
            {
                var height = 30;
                c.DrawFilledRectangle(new Pen(Color.Blue), 0, canvas.Mode.Rows - height, canvas.Mode.Columns, height);
            }));
        }
        private void MouseRoutine()
        {
            Kernel.PrintDebug("Drawing mouse....");
            canvas.DrawFilledRectangle(new Pen(Color.Black), mouse.X, mouse.Y, 2, 2);
        }
    }
}
