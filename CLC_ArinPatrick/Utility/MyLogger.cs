using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper_ArinPatrick.Utility
{
    public class MyLogger : ILogger
    {
        //singlton is a class that can only have one instance 

        private static MyLogger instance;
        private static Logger logger;

        public static MyLogger GetInstance()
        {
            //if there is no logger, create a new one. We can only have ONE
            if(instance == null)
            {
                instance = new MyLogger();
            }
            return instance;
        }


        private Logger GetLogger()
        {
            if (MyLogger.logger == null)
                MyLogger.logger = LogManager.GetLogger("MinesweeperRule");
            return MyLogger.logger;
            

            
        }
        public void Debug(string message)
        {
             GetLogger().Debug(message);
        }

        public void Error(string message)
        {
            GetLogger().Error(message);

        }

        public void info(string message)
        {
            GetLogger().Info(message);

        }

        public void Warning(string message)
        {
            GetLogger().Warn(message);

        }
    }
}
