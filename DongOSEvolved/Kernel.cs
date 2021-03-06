﻿using Cosmos.Debug.Kernel;
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
            Console.Clear();
            Console.WriteLine("Hi donger, type 'help' for all dem commands !");
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
                if (input.StartsWith(item.Name, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var parameter = input.Substring(item.Name.Length);
                        item.Execute(parameter);
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("FAWK, ERROR OCCURED : ");
                        Console.WriteLine(e.ToString().ToUpper());
                        Console.ResetColor();
                    }
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
            new HelpCommand(),
            new PerfTestCommand(),
            new TestCommand()
        };

        public class HelpCommand : ICommand
        {
            public string Name => "help";

            public string Description => "Shows help.";

            public void Execute(string parameter)
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
