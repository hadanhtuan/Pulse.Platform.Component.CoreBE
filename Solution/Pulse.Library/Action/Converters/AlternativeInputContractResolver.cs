using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace SA.Aodb.Actions.Framework.Converters
{
    public class AlternativeInputContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            // Dropdown attributes always return a list of options to the user. When this is sent back in,
            // a single value is received. This makes sure that value is deserialized as a single value
            // in an array instead.
            // if (member.GetCustomAttributes(typeof(DropdownSpecAttribute)).Any())
            // {
            //     prop.Converter = new DropdownJsonConverter();
            // }

            return prop;
        }
    }
}
