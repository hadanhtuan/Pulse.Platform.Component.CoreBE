using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    [Serializable]
    public class IndexUnsupportedForEntityTypeException : EntityTypeException
    {
        public IndexUnsupportedForEntityTypeException(string entityTypeName)
            : base("New Schema contains Indexes in unsupported EntityType: {EntityTypeName}" +
                   "of Type {Type}",
                new Dictionary<string, object> { { "EntityTypeName", entityTypeName } })
        {
        }

        protected IndexUnsupportedForEntityTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}