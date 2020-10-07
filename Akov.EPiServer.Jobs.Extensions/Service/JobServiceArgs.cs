using System;
using EPiServer.Logging;

namespace Akov.EPiServer.Jobs.Extensions.Service
{
    public class JobServiceArgs : EventArgs
    {
        public JobServiceArgs(string message, Level level = Level.Information)
        {
            Message = message;
            Level = level;
        }

        public string Message { get; }
        public Level Level { get; }
    }
}