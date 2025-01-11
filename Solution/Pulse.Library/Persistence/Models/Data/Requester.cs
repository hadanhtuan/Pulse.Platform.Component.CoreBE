using GlobalIdentity.Models;

namespace Database.Models.Data
{
    
    /// Model for the requester that published a raw message
    
    public class Requester 
    {
        public Requester(IRequester? requester)
        {
            RequesterId = requester?.Id;
            RequesterRole = requester?.Role;
            RequestType = requester?.RequestType;
            RequestContext = requester?.RequestContext;
        }

        public Requester() { }

        public string? RequesterId { get; private set; }

        public string? RequesterRole { get; private set; }

        public string? RequestType { get; private set; }

        public string? RequestContext { get; private set; }
    }
}
