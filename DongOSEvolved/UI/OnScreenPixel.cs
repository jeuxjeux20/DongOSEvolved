using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;

namespace DongOSEvolved.UI
{
    public class OnScreenPixel : Pixel
    {
        public OnScreenPixel(Color c, Point p, UIElement owner) : base(c, p)
        {
            Pixels.Add(new OwnedPixel(c, p, owner));
        }
        public OnScreenPixel(uint color, Point p, UIElement owner) : base(color, p)
        {
            Pixels.Add(new OwnedPixel(color, p, owner));
        }
        public override Color Color
        {
            get
            {
                if (Pixels.Count == 0)
                {
                    return null;
                }
                else
                {
                    return Pixels[Pixels.Count - 1].Color;
                }
            }
            set
            {
                if (Pixels.Count == 0)
                {
                    return;
                }
                else
                {
                    Pixels[Pixels.Count - 1].Color = value;
                }
            }
        }
        public List<OwnedPixel> Pixels { get; } = new List<OwnedPixel>();
    }
}
