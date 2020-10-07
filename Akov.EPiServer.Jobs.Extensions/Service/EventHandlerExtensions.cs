using System;

namespace Akov.EPiServer.Jobs.Extensions.Service
{
    public static class EventHandlerExtensions
    {
        public static void Fire(this EventHandler<JobServiceArgs> handler, object @this, string message)
        {
            if (handler != null && !String.IsNullOrWhiteSpace(message))
            {
                var args = new JobServiceArgs(message);
                handler(@this, args);
            }
        }
    }
}