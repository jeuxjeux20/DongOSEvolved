using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;

namespace DongOSEvolved.UI
{
    public class DelegateUIElement : UIElement
    {
        public DelegateUIElement(UIEnvironment environment, DrawDelegate draw) : base(environment)
        {
            ShapeDraw = draw; 
            Draw();
        }
        public DrawDelegate ShapeDraw { get; private set; }
        protected override void DrawShape(UICanvas c)
        {
            Kernel.PrintDebug("Drawing shaaaapppe");
            ShapeDraw(c);
        }
    }
}
