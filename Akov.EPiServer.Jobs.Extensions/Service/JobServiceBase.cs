using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Akov.EPiServer.Jobs.Extensions.Progress;

namespace Akov.EPiServer.Jobs.Extensions.Service
{
    public abstract class JobServiceBase : IJobService
    {
        protected JobServiceBase(IProgressManager progressManager)
        {
            ProgressManager = progressManager;
            InitProgressManager(ProgressManager);
        }

        protected IProgressManager ProgressManager { get; }

        public event EventHandler<JobServiceArgs> OnStatusChanged;

        public abstract string Execute(Dictionary<string, object> parameters);

        protected virtual void LogMessage(string message) => OnStatusChanged.Fire(this, message);

        private void InitProgressManager(IProgressManager progressManager)
        {
            var sequence = Observable.FromEventPattern<Progress.Progress>(
                handler => progressManager.OnProgressChanged += handler,
                handler => progressManager.OnProgressChanged -= handler);
            sequence.Subscribe(
                data => LogMessage(data.EventArgs.Report()),
                ex => LogMessage(ex.Message));
        }
    }
}