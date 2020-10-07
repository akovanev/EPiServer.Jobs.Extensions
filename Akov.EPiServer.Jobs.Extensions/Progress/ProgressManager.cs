using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Akov.EPiServer.Jobs.Extensions.Progress
{
    public class ProgressManager : IProgressManager
    {
        private readonly Dictionary<int, Progress> _dictionary = new Dictionary<int, Progress>();
        private readonly int _mod;

        public ProgressManager(int mod)
        {
            _mod = mod;
        }

        public event EventHandler<Progress> OnProgressChanged;

        public int CreateNew(
            int count,
            [CallerMemberName] string method = "")
        {
            int id = _dictionary.Count + 1;
            _dictionary.Add(id, new Progress(method, count));
            return id;
        }

        public void Increment(int progressId)
        {
            if (!_dictionary.ContainsKey(progressId)) return;

            Progress p = _dictionary[progressId];

            p.Increment();

            if (p.Current % _mod == 0)
                OnProgressChanged?.Invoke(this, p);
        }

        public void Complete(int progressId)
        {
            if (!_dictionary.ContainsKey(progressId)) return;

            Progress p = _dictionary[progressId];
            _dictionary.Remove(progressId);
            OnProgressChanged?.Invoke(this, p);
        }
    }
}