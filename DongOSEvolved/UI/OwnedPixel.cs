using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;

namespace DongOSEvolved.UI
{
    public class OwnedPixel : Pixel
    {
        public OwnedPixel(Color c, Point p, UIElement owner) : base(c, p)
        {
            Owner = owner;
        }

        public OwnedPixel(uint color, Point p, UIElement owner) : base(color, p)
        {
            Owner = owner;
        }
        public UIElement Owner { get; }
    }
}
