using System;
using System.Collections.Generic;
using System.Text;
using Ninja.Application.Common.Interfaces;
using NLog;

namespace Ninja.Application.Services
{
    public class Loggin : ILoggin
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}
