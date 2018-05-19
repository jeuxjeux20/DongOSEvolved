using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DongOSEvolved.UI
{
    /// <summary>
    /// Back, to the drawing board !!!
    /// </summary>
    public class UICanvas : VBEScreen
    {
        public UICanvas()
        {
            ResetPixelArray();
        }

        private void ResetPixelArray()
        {
            PixelsLocation = new OnScreenPixel[mode.Columns][];
            for (int i = 0; i < PixelsLocation.Length; i++)
            {
                PixelsLocation[i] = new OnScreenPixel[mode.Rows];
            }
        }
        private UIElement recorderOwner = null;
        public List<Pixel> ExecuteWhileRecording(Action<UICanvas> drawing, UIElement ui = null)
        {
            if (ui == null)
            {
                recorderOwner = null;
            }
            else
            {
                recorderOwner = ui;
            }

            pixels.Clear();
            IsRecording = true;
            drawing(this);
            IsRecording = false;
            recorderOwner = null;
            return pixels;
        }
        private List<Pixel> pixels = new List<Pixel>();
        public bool IsRecording { get; private set; }
        public OnScreenPixel[][] PixelsLocation { get; private set; }
        public override Mode Mode
        {
            get => base.Mode;
            set
            {
                if (value != base.Mode)
                {
                    Kernel.PrintDebug("Set mode in progresssss");
                    ResetPixelArray();
                    base.Mode = value;
                    foreach (var item in OnModeChanged)
                    {
                        item(Mode);
                    }
                    Kernel.PrintDebug("Set mode donnnnne");
                }
            }
        }
        public override void DrawFilledRectangle(Pen pen, int x_start, int y_start, int width, int height)
        {
            base.DrawFilledRectangle(pen, x_start, y_start, width, height);
            Kernel.PrintDebug("Drawing the rectangool");
            if (IsRecording)
            {
                for (int wi = 0; wi < width; wi++)
                {
                    for (int hi = 0; hi < height; hi++)
                    {
                        Record(pen, wi + x_start, hi + y_start);
                    }
                }
            }
        }
        public void DrawFilledRectangleManual(Pen pen, int x_start, int y_start, int width, int height)
        {
            Kernel.PrintDebug("Drawing the rectangool");
            if (IsRecording)
            {
                for (int wi = 0; wi < width; wi++)
                {
                    for (int hi = 0; hi < height; hi++)
                    {
                        DrawPoint(pen, wi + x_start, hi + x_start);
                    }
                }
            }
        }
        public List<ModeChange> OnModeChanged { get; } = new List<ModeChange>();
        public delegate void ModeChange(Mode mode);
        public override void DrawPoint(Pen pen, int x, int y)
        {
            base.DrawPoint(pen, x, y);
            if (IsRecording)
            {
                Record(pen, x, y);
            }
        }

        private void Record(Pen pen, int x, int y)
        {
            // Kernel.PrintDebug("ah yes recording smells good");
            Point p = new Point(x, y);
            //Kernel.PrintDebug("Point dong");
            var color = pen.Color;
            //Kernel.PrintDebug("Color dong");
            Pixel pix = new Pixel(color, p);
            //Kernel.PrintDebug("createteteded pix");
            pixels.Add(pix);
            //Kernel.PrintDebug("adddedeeeeed");
            
            if (recorderOwner != null)
            {
                OnScreenPixel onScreenPixel = PixelsLocation[x][y];
                if (onScreenPixel == null)
                {
                    onScreenPixel = new OnScreenPixel(color, p, recorderOwner);
                }
                else
                {
                    onScreenPixel.Pixels.Add(new OwnedPixel(color, p, recorderOwner));
                }
            }
            else
            {
                Kernel.PrintDebug("ok the recorder is nool");
            }
        }

        public void DrawPoint(int color, int x, int y)
        {
            this.DrawPoint(new Pen(Color.FromArgb(color)),x,y);
        }

        private void Record(uint color, int x, int y)
        {
            Point p = new Point(x, y);
            pixels.Add(new Pixel(color, p));
            
            if (recorderOwner != null)
            {
                OnScreenPixel onScreenPixel = PixelsLocation[x][y];
                if (onScreenPixel == null)
                {
                    onScreenPixel = new OnScreenPixel(color, p, recorderOwner);
                }
                else
                {
                    onScreenPixel.Pixels.Add(new OwnedPixel(color, p, recorderOwner));
                }
            }
        }

        public new void DrawImage(Image image, int x, int y)
        {
            for (int imageY = 0; imageY < image.Height; imageY++)
            {
                for (int imageX = 0; imageX < image.Width; imageX++)
                {
                    DrawPoint(image.rawData[imageX + imageY * image.Width], x + imageX, y + imageY);
                }
            }
        }
    }
}
