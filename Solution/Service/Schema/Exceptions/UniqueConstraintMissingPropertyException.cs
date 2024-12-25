using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Schema.Exceptions
{
    [Serializable]
    public class UniqueConstraintMissingPropertyException : EntityTypeException
    {
        public UniqueConstraintMissingPropertyException(string typeName, string constraintName, string property)
            : base("New Schema contains EntityType '{EntityTypeName}' with a UniqueConstraint '{ConstraintName}', " +
                   "which is missing a {Property}.",
                new Dictionary<string, object>
                {
                    { "EntityTypeName", typeName }, { "ConstraintName", constraintName }, { "Property", property }
                })
        {
        }

        protected UniqueConstraintMissingPropertyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}