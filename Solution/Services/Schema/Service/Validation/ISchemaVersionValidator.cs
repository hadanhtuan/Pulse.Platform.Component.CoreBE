﻿using Database.Models.Schema;
using Database.Models.Schema.Modifications;

namespace Services.Schema.Service.Validation;

public interface ISchemaVersionValidator
{
    ICompositeModification ValidateChangeFrom(SchemaVersion newSchema, SchemaVersion oldSchema);
}