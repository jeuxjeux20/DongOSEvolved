using Cosmos.System.Graphics;
using DongOSEvolved.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DongOSEvolved.Commands
{
    class TestCommand : ICommand
    {
        public string Name => "test";

        public string Description => "Mmmmmm testy !";

        public void Execute(string parameter)
        {
            var canvas = new VBEScreen(new Mode(320, 240, ColorDepth.ColorDepth32));
            canvas.Clear(Color.Blue);
            var color = Color.Green;
            canvas.DrawCircle(new Pen(color), 160, 160, 70);
            canvas.DrawImage(Images.TV, 190, 190);
            canvas.DrawFilledRectangle(new Pen(Color.DarkBlue), 0, 30, canvas.Mode.Columns, 30);
        }
    }
}
