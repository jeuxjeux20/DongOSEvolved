using Cosmos.Debug.Kernel;
using DongOSEvolved.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace DongOSEvolved
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("Test");
            Console.WriteLine("Hi donger, look at some commands lol");
        }
        public override void Start()
        {
            base.Start();
        }
        protected override void Run()
        {

            Console.Write("Dong> ");
            var input = Console.ReadLine();
            bool commandFound = false;
            foreach (var item in commands)
            {
                if (item.Name.Equals(input, StringComparison.CurrentCultureIgnoreCase))
                {
                    item.Execute();
                    commandFound = true;
                    break;
                }
            }
            if (!commandFound)
            {
                Console.WriteLine("Command not found :'(");
            }
        }
        
        private static readonly ICommand[] commands = new ICommand[]
        {
            new AboutCommand(),
            new GraphicsCommand(),
            new HelpCommand()
        };

        public class HelpCommand : ICommand
        {
            public string Name => "help";

            public string Description => "Shows help.";

            public void Execute(object parameter)
            {
                string result = "";
                byte count = 0;
                foreach (var item in commands)
                {
                    count++;
                    result += item.Name + "\t" + item.Description;
                    if (count != commands.Length)
                    {
                        result += Environment.NewLine;
                    }
                }
                Console.WriteLine(result);
            }
        }
    }
}
