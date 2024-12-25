using Database.Models.Schema;

namespace Service.Schema.Service.Copy;

internal interface IAttributeTypeCopier
{
    void CopyInto(AttributeType source, AttributeType target);
}