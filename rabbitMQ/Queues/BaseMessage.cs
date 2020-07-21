using System;
using System.Collections.Generic;
using System.Text;

namespace Queues
{
    public abstract class BaseMessage
    {
        public DateTime SentOn { get; set; } = DateTime.Now;

        public string Sender { get; set; } = "No one";
    }
}
