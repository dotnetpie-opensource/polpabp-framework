namespace PolpAbp.Framework.Mock
{
    public class SharedMemory
    {
        public static SharedMemory Data = new SharedMemory();

        public string BackgroundJobName = string.Empty;

        public string EmailReceiver = string.Empty;
    }
}
