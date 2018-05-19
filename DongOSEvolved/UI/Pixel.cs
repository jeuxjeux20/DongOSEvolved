using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.UI
{
    public class Pixel
    {
        public Pixel(Color c, Point p)
        {
            color = c;
            Coords = p;
        }
        public Pixel(uint color, Point p)
        {
            argbColor = color;
            Coords = p;
        }
        protected Color color;

        public virtual Color Color
        {
            get {
                if (Color == null)
                {
                    // Since you won't always need to get the color, better do the conversion later.
                    color = Color.FromArgb((int)argbColor);
                }
                return color;
            }
            set { color = value; }
        }

        public Point Coords { get; private set; }
        private readonly uint argbColor;
        public static bool operator ==(Pixel p1, Pixel p2) // null == new Pixel();
        {
            if (ReferenceEquals(null, p1)) // null is null ?
            {
                return ReferenceEquals(null, p2); // new Pixel() is null
            }
            if (ReferenceEquals(null, p2)) {
                return false;
            }
            return p1.color == p2.color && p1.Coords == p2.Coords;
        }
        public static bool operator !=(Pixel p1, Pixel p2)
        {
            if (ReferenceEquals(null, p1))
            {
                return !ReferenceEquals(null, p2);
            }
            if (ReferenceEquals(null, p2))
            {
                return true;
            }
            return p1.color != p2.color && p1.Coords != p2.Coords;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + color.GetHashCode();
                hash = hash * 23 + Coords.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            var pixel = obj as Pixel;
            return pixel != null &&
                   color == pixel.color &&
                   Coords == pixel.Coords;
        }
    }
}
