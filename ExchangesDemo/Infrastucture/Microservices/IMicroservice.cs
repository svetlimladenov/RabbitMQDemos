using System;
using Microsoft.Extensions.Hosting;

namespace Infrastucture.Microservices
{
    public interface IMicroservice : IHostedService, IDisposable
    {
        void Start();

        void Stop();
    }
}