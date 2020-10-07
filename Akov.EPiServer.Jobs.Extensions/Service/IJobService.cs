using System;
using System.Collections.Generic;

namespace Akov.EPiServer.Jobs.Extensions.Service
{
    public interface IJobService
    {
        string Execute(Dictionary<string, object> parameters);
        event EventHandler<JobServiceArgs> OnStatusChanged;
    }
}
