using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    [Serializable]
    public class UniqueConstraintChangedException : EntityTypeException
    {
        public UniqueConstraintChangedException(string internalName)
            : base("New Schema contains a change to the existing unique constraint '{UniqueConstraintInternalName}'",
                new Dictionary<string, object> { { "UniqueConstraintInternalName", internalName }, })
        {
        }

        protected UniqueConstraintChangedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}