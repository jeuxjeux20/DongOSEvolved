using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static Cosmos.System.Kernel;

namespace DongOSEvolved.UI
{
    public abstract class UIElement : IComparable
    {
        // The problem with this technique is that if it's the same color,
        // it doesn't recognize the shape.
        public virtual List<Pixel> Draw(Canvas c)
        {
            Kernel.PrintDebug("Drawing started.");
            var before = GetAllPixels(c);
            DrawShape(c);
            var after = GetAllPixels(c);
            var pixelsChanged = new List<Pixel>();
            // Normally i won't have to check the length of both.
            for (int i = 0; i < before.Length; i++)
            {
                if (before[i] != after[i])
                {
                    pixelsChanged.Add(after[i]);
                }
            }
            lastDrawing = pixelsChanged;
            return pixelsChanged;
        }
        private List<Pixel> lastDrawing = new List<Pixel>();
        public ReadOnlyCollection<Pixel> LastDrawing => lastDrawing.AsReadOnly();
        private static Pixel[] GetAllPixels(Canvas c)
        {
            PrintDebug("Getting all pixels...");
            var totalPixels = c.Mode.Columns * c.Mode.Rows;
            PrintDebug("Creating array");
            Pixel[] allPoints = new Pixel[totalPixels];
            int x = 0, y = 0;
            for (int i = 0; i < totalPixels; i++)
            {
                PrintDebug("Iteration : " + i + " - x : " + x + " ; y : " + y);
                allPoints[i] = new Pixel(c.GetPointColor(x, y), new Point(x, y));
                y++;
                if (y >= c.Mode.Columns - 1) // Go to the line under it
                {
                    y = 0;
                    ++x; // it's better than x++ for some reason.
                }
            }
            return allPoints;
        }

        protected abstract void DrawShape(Canvas c);

        public int CompareTo(object obj)
        {
            return Layer.CompareTo(obj);
        }

        public int Layer { get; set; } = 0;
    }
    public delegate void DrawDelegate(Canvas c);
}
