// using SA.Actions.Meta;

using System.Linq;

namespace API.Shared.IO.Responses
{
    /// <summary>
    /// Minimal presentation model of an action which includes all action related information except for
    /// information on its attributes.
    /// </summary>
    public class MinimalActionView : ActionDescription
    {
        public new bool FulfilledPreconditions { get; set; }

        public new bool HasPermission { get; set; }

        public static MinimalActionView FromActionMetadata(
            ActionAttribute actionMeta,
            bool? hasPermission = null,
            bool? fulfilledPreconditions = null)
        {
            return new MinimalActionView
            {
                InternalName = actionMeta.InternalName,
                EntityType = actionMeta.EntityInternalName,
                Description = actionMeta.Description ?? "",
                Label = actionMeta.Label ?? "",
                Group = actionMeta.Group ?? "",
                Shortcut = actionMeta.ShortcutButton?.Split(new char[] { ',' }).ToList(),
                FulfilledPreconditions = fulfilledPreconditions ?? false,
                HasPermission = hasPermission ?? false,
                IsSuggested = actionMeta.IsSuggested,
                Weight = actionMeta.Weight,
                ModalSize = actionMeta.ModalSize,
            };
        }
    }
}