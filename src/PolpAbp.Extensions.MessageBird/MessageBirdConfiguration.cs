namespace PolpAbp.Extensions.MessageBird
{
    public class MessageBirdConfiguration
    {
        public string DeveloperAccesskey { get; set; }
        public string LiveAccesskey { get; set; }
        public bool LiveMode { get; set; }  
        public string Originators { get; set; }
    }
}
