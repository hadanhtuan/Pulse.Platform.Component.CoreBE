using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Schema.Exceptions
{
    [Serializable]
    public class UniqueConstraintUnsupportedForEntityTypeException : EntityTypeException
    {
        public UniqueConstraintUnsupportedForEntityTypeException(string entityTypeName)
            : base("New Schema contains Unique Constraints in unsupported EntityType: {EntityTypeName}" +
                   "of Type {Type}",
                new Dictionary<string, object> { { "EntityTypeName", entityTypeName } })
        {
        }

        protected UniqueConstraintUnsupportedForEntityTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}