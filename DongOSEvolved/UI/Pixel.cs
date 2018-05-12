using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.UI
{
    public struct Pixel
    {
        public Pixel(Color c, Point p)
        {
            color = c;
            coords = p;
        }
        public Color color;
        public Point coords;
        public static bool operator ==(Pixel p1, Pixel p2)
        {
            if (p1.color == p2.color && p1.coords == p2.coords)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool operator !=(Pixel p1, Pixel p2)
        {
            if (p1.color == p2.color && p1.coords == p2.coords)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + color.GetHashCode();
                hash = hash * 23 + coords.GetHashCode();
                return hash;
            }
        }
    }
}
