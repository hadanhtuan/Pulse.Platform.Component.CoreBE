using Newtonsoft.Json;
// using SA.Aodb.Actions.Interfaces.Meta;
using System.Collections.Generic;

namespace API.Shared.IO.Responses
{
    /// <summary>
    /// Base information on a single action. Contains enough info for being able to identify the actions, 
    /// but also support for the UI to display, and trigger the action.
    /// </summary>
    public class ActionDescription
    {
        public string EntityType { get; set; } = null!;

        public string InternalName { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Group { get; set; } = null!;
        public bool? HasPermission { get; set; }
        public bool? FulfilledPreconditions { get; set; }
        // Keys that need to be pressed simultaneously to trigger the action
        public List<string>? Shortcut { get; set; }
        public bool IsSuggested { get; set; }
        public int Weight { get; set; }
        // [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        // public ModalSize ModalSize { get; set; }
    }
}
