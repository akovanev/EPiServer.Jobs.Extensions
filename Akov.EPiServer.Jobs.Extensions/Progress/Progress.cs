namespace Akov.EPiServer.Jobs.Extensions.Progress
{
    public class Progress
    {
        public Progress(string method, int count)
        {
            Method = method;
            Count = count;
        }

        public string Method { get; }
        public int Count { get; }
        public int Current { get; private set; }

        public void Increment() => Current++;
    }
}
