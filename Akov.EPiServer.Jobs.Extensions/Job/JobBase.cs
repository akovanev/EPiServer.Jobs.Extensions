using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Akov.EPiServer.Jobs.Extensions.Service;
using EPiServer.Logging;
using EPiServer.Scheduler;

namespace Akov.EPiServer.Jobs.Extensions.Job
{
    public class JobBase<TService> : ScheduledJobBase
        where TService : IJobService
    {
        private readonly IJobService _service;
        private readonly ILogger _logger;

        public JobBase(TService service, ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        public override string Execute()
        {
            SubscribeToStatusChanged(_service);
            return _service.Execute(GetExecutionParameters());
        }

        protected virtual Dictionary<string, object> GetExecutionParameters() => null;

        protected virtual void SubscribeToStatusChanged(IJobService service)
        {
            var sequence = Observable.FromEventPattern<JobServiceArgs>(
                handler => service.OnStatusChanged += handler,
                handler => service.OnStatusChanged -= handler);
            sequence.Subscribe(
                data => UpdateStatus(data.EventArgs),
                ex => UpdateStatus(new JobServiceArgs(ex.Message, Level.Critical)));
        }

        protected virtual void UpdateStatus(JobServiceArgs args)
        {
            OnStatusChanged(args.Message);
            _logger.Log(args.Level, args.Message);
        }
    }
}
