using System;
using System.Runtime.CompilerServices;

namespace Akov.EPiServer.Jobs.Extensions.Progress
{
    public interface IProgressManager
    {
        event EventHandler<Progress> OnProgressChanged;
        int CreateNew(int count, [CallerMemberName] string method = "");
        void Increment(int progressId);
        void Complete(int progressId);
    }
}