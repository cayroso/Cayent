using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Infrastructure.Data
{
    public class EventData
    {
        public string Id { get; set; }
        public string CorrelationId { get; set; }
        //public string TenantId { get; set; }
        public string Type { get; set; }
        public string Event { get; set; }

        public int RetryCount { get; set; }
        public DateTimeOffset DateSent { get; set; }
        public DateTimeOffset DateFailed { get; set; }
        public DateTimeOffset DateSuccess { get; set; }

        public EventData()
        {
            Id = string.Empty;
            CorrelationId = string.Empty;
            //TenantId = string.Empty;
            Type = string.Empty;
            Event = string.Empty;

            RetryCount = 0;
            DateSent = DateTimeOffset.MinValue;
            DateFailed = DateTimeOffset.MaxValue;
            DateSuccess = DateTimeOffset.MaxValue;
        }
    }
}
