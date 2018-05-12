using System;
using System.Collections.Generic;
using System.Text;

namespace DongOSEvolved.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        void Execute(object parameter = null);
    }
}
