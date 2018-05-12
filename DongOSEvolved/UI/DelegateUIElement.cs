using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;

namespace DongOSEvolved.UI
{
    public class DelegateUIElement : UIElement
    {
        public DelegateUIElement(DrawDelegate draw)
        {
            ShapeDraw = draw;
        }
        public DrawDelegate ShapeDraw { get; }
        protected override void DrawShape(Canvas c)
        {
            Kernel.PrintDebug("Drawing shaaaapppe");
            ShapeDraw(c);
        }
    }
}
