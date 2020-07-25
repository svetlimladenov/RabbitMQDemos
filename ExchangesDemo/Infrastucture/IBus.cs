using System;

namespace Infrastucture
{
    public interface IBus
    {
        void Publish(string exchange, string message);

        void Subscribe(string exchange, Action<string> action);
    }
}