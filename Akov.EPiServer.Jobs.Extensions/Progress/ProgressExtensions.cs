namespace Akov.EPiServer.Jobs.Extensions.Progress
{
    public static class ProgressExtensions
    {
        public static string Report(this Progress progress)
            => $"{progress.Method}: {progress.Current}/{progress.Count}";
    }
}