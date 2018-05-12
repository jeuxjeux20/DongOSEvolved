using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.Commands
{
    class PerfTestCommand : ICommand
    {
        public string Name => "perftest";

        public string Description => "Does a performance test.";

        public void Execute(string parameter)
        {
            parameter = parameter.Trim();
            int result = 0;
            bool parsed = false;
            if (parameter != null)
            {
                parsed = int.TryParse(parameter, out result);
            }
            for (int i = 0; i < (parsed ? result : 10); i++)
            {
                Console.WriteLine("HEYYYEAAYEAAAYEAAA : " + i);
            }
        }
    }
}
