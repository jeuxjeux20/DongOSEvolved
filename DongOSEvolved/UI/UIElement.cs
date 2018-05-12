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
        // 100% clean :)
        public virtual List<Pixel> Draw(UICanvas c) 
        {
            // I could've done c.ExecuteWhileRecording(DrawShape) but that Ldvirtftn is angry 
            return c.ExecuteWhileRecording((canvas) =>
            {
                DrawShape(canvas);
            });
        }
        private List<Pixel> lastDrawing = new List<Pixel>();
        public ReadOnlyCollection<Pixel> HitPixels => lastDrawing.AsReadOnly();
        #region Old and slow methods
        //private static Pixel[] GetAllPixels(Canvas c)
        //{
        //    PrintDebug("Getting all pixels...");
        //    var totalPixels = c.Mode.Columns * c.Mode.Rows;
        //    PrintDebug("Creating array");
        //    Pixel[] allPoints = new Pixel[totalPixels];
        //    int x = 0, y = 0;
        //    for (int i = 0; i < totalPixels; i++)
        //    {
        //        PrintDebug("Iteration : " + i + " - x : " + x + " ; y : " + y);
        //        allPoints[i] = new Pixel(c.GetPointColor(x, y), new Point(x, y));
        //        y++;
        //        if (y >= c.Mode.Columns - 1) // Go to the line under it
        //        {
        //            y = 0;
        //            ++x; // it's better than x++ for some reason.
        //        }
        //    }
        //    return allPoints;
        //}
        #endregion
        protected abstract void DrawShape(Canvas c);

        public int CompareTo(object obj)
        {
            return Layer.CompareTo(obj);
        }

        public int Layer { get; set; } = 0;
    }
    public delegate void DrawDelegate(Canvas c);
}
