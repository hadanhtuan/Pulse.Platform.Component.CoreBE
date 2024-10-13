using System;

namespace API.Shared.Core.Interfaces.Context.Matching
{
    public interface IAdapterMessage
    {
        public DateTime Received { get; set; }
        public Guid? InternalId { get; set; }
    }
}
