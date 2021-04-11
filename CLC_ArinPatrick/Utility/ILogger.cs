using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Utility
{
    public interface ILogger
    {
        void Debug(string message);
        void info(string message);
        void Warning(string message);
        void Error(string message);


    }
}
