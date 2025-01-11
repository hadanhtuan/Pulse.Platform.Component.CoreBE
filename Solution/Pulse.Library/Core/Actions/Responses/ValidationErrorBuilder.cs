using System.Collections.Generic;

namespace Pulse.Library.Core.Actions.Responses
{
    public class ValidationErrorBuilder
    {
        private readonly IDictionary<string, List<string>> validationErrors = new Dictionary<string, List<string>>();

        
        /// Appends a validation error for the specific attribute. If the error is inside a complex object, 
        /// the attribute should contain all internal name separated by '.'.
        ///
        /// Example: An error on Attribute 2, should result in "attribute" = "attr1.attr2"
        /// public class ActionConfiguration 
        /// {
        ///     [ActionAttr(InternalName = "attr1")]
        ///     public ComplexObject Attribute1
        ///     {
        ///         [ActionAttr(InternalName = "attr2")]
        ///         public string Attribute2 = "Invalid value"
        ///     }
        /// }
        
        /// <param name="attribute">{attribute_internal_name}.{attribute_internal_name}</param>
        /// <param name="error">The validation error that occured.</param>
        public ValidationErrorBuilder AddValidationError(string attribute, string error)
        {
            var key = attribute.ToLower();
            if (!validationErrors.TryGetValue(key, out var list))
            {
                list = new List<string>();
                validationErrors.Add(key, list);
            }

            list.Add(error);
            return this;
        }

        
        /// Builds the validation error response.
        
        /// <returns></returns>
        public IDictionary<string, List<string>> Build()
        {
            return validationErrors;
        }
    }
}
