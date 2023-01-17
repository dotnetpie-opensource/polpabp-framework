using MessageBird;
using Microsoft.Extensions.Options;
using PolpAbp.Framework.Globalization;
using PolpAbp.Framework.Net;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace PolpAbp.Extensions.MessageBird
{
    [RemoteService(false)]
    public class MessageBirdClient : ISingletonDependency
    {
        private readonly MessageBirdConfiguration _messageBirdCfg;
        private readonly List<Tuple<CountryAlphaEnum, string>> _phoneNumbers;
        private readonly Client _client;

        public Client Client => _client;
        public string Accesskey => _messageBirdCfg.LiveMode ? _messageBirdCfg.LiveAccesskey : _messageBirdCfg.DeveloperAccesskey;

        public MessageBirdClient(IOptions<MessageBirdConfiguration> configurationProvider)
        {
            _messageBirdCfg = configurationProvider.Value;
            _phoneNumbers = new List<Tuple<CountryAlphaEnum, string>>();

            var originators = _messageBirdCfg.Originators;

            // Parse 
            var pairList = originators.Split(',');
            foreach (var pair in pairList)
            {
                var pieces = pair.Split(':');
                if (pieces.Length == 2)
                {
                    int.TryParse(pieces[0], out int code);
                    if (code > 0)
                    {
                        _phoneNumbers.Add(new Tuple<CountryAlphaEnum, string>((CountryAlphaEnum)code, pieces[1].Trim()));
                    }
                }
            }

            _client = Client.CreateDefault(Accesskey);
        }

        public string GetOrginatorByCountry(CountryAlphaEnum code)
        {
            var entry = _phoneNumbers.Find(a => a.Item1 == code);
            return entry?.Item2;
        }
    }
}
