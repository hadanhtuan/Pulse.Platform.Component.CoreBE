using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Schema.Exceptions
{
    [Serializable]
    public class UniqueConstraintInternalNameInvalidLengthException : EntityTypeException
    {
        public UniqueConstraintInternalNameInvalidLengthException(string typeName, string constraintName)
            : base("New Schema contains EntityType '{EntityTypeName}' with a UniqueConstraint '{ConstraintName}', " +
                   "which has an InternalName above 62 characters.",
                new Dictionary<string, object> { { "EntityTypeName", typeName }, { "ConstraintName", constraintName } })
        {
        }

        protected UniqueConstraintInternalNameInvalidLengthException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}