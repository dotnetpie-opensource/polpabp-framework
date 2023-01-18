using MessageBird;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Extensions.MessageBird
{
    [RemoteService(false)]
    public class MessageBirdClientWrapper : ISingletonDependency
    {
        private readonly MessageBirdConfiguration _messageBirdCfg;
        private readonly List<Tuple<string, string>> _phoneNumbers;
        private readonly Client _client;

        public Client Client => _client;
        public string Accesskey => _messageBirdCfg.LiveMode ? _messageBirdCfg.LiveAccesskey : _messageBirdCfg.DeveloperAccesskey;

        public MessageBirdClientWrapper(IOptions<MessageBirdConfiguration> configurationProvider)
        {
            _messageBirdCfg = configurationProvider.Value;
            _phoneNumbers = new List<Tuple<string, string>>();

            var originators = _messageBirdCfg.Originators;

            // Parse 
            // An example of the configuration is:
            // US:12056062323,CA:12265465476
            var pairList = originators.Split(',');
            foreach (var pair in pairList)
            {
                var pieces = pair.Split(':');
                if (pieces.Length == 2)
                {
                    _phoneNumbers.Add(new Tuple<string, string>(pieces[0], pieces[1].Trim()));
                }
            }

            _client = Client.CreateDefault(Accesskey);
        }

        public string GetOrginatorByCountry(string alpha)
        {
            var entry = _phoneNumbers.Find(a => a.Item1 == alpha);
            return entry?.Item2;
        }
    }
}
