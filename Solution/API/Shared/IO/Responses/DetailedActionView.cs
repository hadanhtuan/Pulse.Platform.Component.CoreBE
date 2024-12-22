using Newtonsoft.Json;
using System.Collections.Generic;

namespace SA.Actions.Framework.IO.Responses
{
    /// <summary>
    /// Presention model an action including information on the action's defined attributes
    /// </summary>
    public class DetailedActionView : MinimalActionView
    {
        public IEnumerable<DetailedAttributeView>? Attributes { get; set; }
        public string? SubmitButtonLabel { get; set; }
        public string? CancelButtonLabel { get; set; }
        public bool? ConfirmCancel { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool HideAction { get; set; }

        public bool? ConfirmSubmit { get; set; }
    }
}