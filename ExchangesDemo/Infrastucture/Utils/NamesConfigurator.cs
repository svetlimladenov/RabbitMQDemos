using System;

namespace Infrastucture.Utils
{
    public class NamesConfigurator
    {
        public string GenerateExchangeName(Type message)
        {
            //TODO: Use some convention, or add configuration, etc
            return message.ToString();
        }
    }
}