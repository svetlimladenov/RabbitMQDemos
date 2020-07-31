using System;

namespace Infrastucture
{
    public interface IBus : IDisposable
    {
        void Publish(string exchange, string message);

        void Publish<T>(T message);

        void Subscribe(string exchange, Action<string> action);

        void Subscribe<T>(Action<T> action);

    }
}