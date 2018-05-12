using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.Commands
{
    class AboutCommand : ICommand
    {
        public string Name => "about";

        public string Description => "Shows the version of the OS.";

        public void Execute(string parameter)
        {
            Console.WriteLine("DongOS 0.0.1 | GL HF :) !");
        }
    }
}
