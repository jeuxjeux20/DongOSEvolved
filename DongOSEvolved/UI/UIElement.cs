using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static Cosmos.System.Kernel;

namespace DongOSEvolved.UI
{
    public abstract class UIElement : IDisposable
    {
        // 100% clean :)
        public List<Pixel> Draw() 
        {
            // I could've done c.ExecuteWhileRecording(DrawShape) but that Ldvirtftn is angry 
            return parentEnvironment.Canvas.ExecuteWhileRecording((canvas) =>
            {
                DrawShape(canvas);
            }, this);
        }
        protected readonly UIEnvironment parentEnvironment;
        private List<Pixel> lastDrawing = new List<Pixel>();
        public ReadOnlyCollection<Pixel> PixelsDrawn => lastDrawing.AsReadOnly();
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
        protected abstract void DrawShape(UICanvas c);

        //public int CompareTo(object obj)
        //{
        //    return Layer.CompareTo(obj);
        //}
        public UIElement(UIEnvironment environment)
        {
            parentEnvironment = environment;
            // Draw(environment.Canvas);
        }
        
        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    parentEnvironment.Elements.Remove(this);
                    foreach (var item in lastDrawing)
                    {
                        OnScreenPixel loc = parentEnvironment.Canvas.PixelsLocation[item.Coords.X][item.Coords.Y];
                        for (int i = 0; 
                             i < loc.Pixels.Count; 
                             i++)
                        {
                            if (loc.Pixels[i].Owner == this)
                            {
                                loc.Pixels.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous. no ty
                lastDrawing = null;

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~UIElement() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }
        // no managed things

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
        //public int Layer { get; set; } = 0;
        //No more need i guess.
    }
    public delegate void DrawDelegate(UICanvas c);
}
