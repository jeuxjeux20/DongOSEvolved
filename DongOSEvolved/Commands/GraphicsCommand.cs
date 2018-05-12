using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DongOSEvolved.Commands
{
    class GraphicsCommand : ICommand
    {
        public string Name => "graphics";

        public string Description => "See the nice graphics";

        public void Execute(object parameter)
        {
            Canvas c = FullScreenCanvas.GetFullScreenCanvas();
            //Mode littlestMode = null;
            //foreach (var item in c.AvailableModes)
            //{
            //    if (littlestMode == null)
            //    {
            //        littlestMode = item;
            //        continue;
            //    }
            //    if (littlestMode > item)
            //    {
            //        littlestMode = item;
            //    }
            //}
            c.Mode = new Mode(800, 600, ColorDepth.ColorDepth32);
            var environment = new UI.UIEnvironment(c);
            environment.Start();
        }
    }
}
