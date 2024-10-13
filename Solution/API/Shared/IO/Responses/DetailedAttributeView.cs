using SA.Aodb.Actions.Framework.IO.Schema;

namespace SA.Aodb.Actions.Framework.IO.Responses
{
    /// <summary>
    /// Presents information on one attribute contained in an action where the internal name is used for
    /// providing input data for the attribute and the requirements, default values, etc are defined in
    /// the schema.
    /// </summary>
    public class DetailedAttributeView
    {
        public string InternalName { get; set; } = null!;
        public ActionAttributeSchema Schema { get; set; } = null!;
    }
}
