using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DongOSEvolved.UI
{
    public class UICanvas : SVGAIIScreen
    {
        public List<Pixel> ExecuteWhileRecording(Action<Canvas> drawing)
        {
            pixels.Clear();
            IsRecording = true;
            drawing(this);
            IsRecording = false;
            return pixels;
        }
        private List<Pixel> pixels = new List<Pixel>();
        public bool IsRecording { get; private set; }

        public override void DrawPoint(Pen pen, int x, int y)
        {
            base.DrawPoint(pen, x, y);
            if (IsRecording)
            {
                pixels.Add(new Pixel(pen.Color, new Point(x, y)));
            }
        }
    }
}
