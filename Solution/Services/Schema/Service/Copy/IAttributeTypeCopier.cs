using Database.Models.Schema;

namespace Services.Schema.Service.Copy;

internal interface IAttributeTypeCopier
{
    void CopyInto(AttributeType source, AttributeType target);
}