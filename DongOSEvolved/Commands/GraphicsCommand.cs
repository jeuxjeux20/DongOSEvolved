using Cosmos.System.Graphics;
using DongOSEvolved.UI;
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

        public void Execute(string parameter)
        {
            var c = new UICanvas
            {
                Mode = new Mode(320, 200, ColorDepth.ColorDepth32)
            };
            var environment = new UIEnvironment(c);
            environment.Start();
        }
    }
}
