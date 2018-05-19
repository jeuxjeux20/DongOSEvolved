using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.UI
{
    public class BackgroundUIElement : UIElement
    {
        public BackgroundUIElement(UIEnvironment environment) : base(environment)
        {
            parentEnvironment.Canvas.OnModeChanged.Add(Canvas_OnModeChanged);
            SetScreenSize();
            Draw();
        }
        private void SetScreenSize()
        {
            columns = parentEnvironment.Canvas.Mode.Columns;
            rows = parentEnvironment.Canvas.Mode.Rows;
            Kernel.PrintDebug("Wow : " + columns + "x" + rows);
        }
        // I put these comments because i'm dumb :p
        // It's the horizontal : ---> [800]x600
        private int columns;
        // It's the vertical   : ---> 800x[600]
        private int rows;
        private void Canvas_OnModeChanged(Mode mode)
        {
            SetScreenSize();
            Draw();
        }
        protected override void DrawShape(UICanvas c)
        {
            c.DrawFilledRectangleManual(new Pen(Color.LightGreen), 0, 0, columns, rows);
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            parentEnvironment.Canvas.OnModeChanged.Remove(Canvas_OnModeChanged);
        }
    }
}
