using API.Shared.Core.Interfaces.Context.Matching;
using API.Shared.Core.Interfaces.Rules.Matching;
using System.Reflection;
using System.Text.Json;

namespace API.Shared.Core.Context.Matching
{
    /// <inheritdoc/>
    public class AdapterHandlerEntityResult : IAdapterHandlerEntityResult
    {
        /// <inheritdoc/>
        public Guid? Identifier { get; }
        /// <inheritdoc/>
        public string EntityType { get; }

        /// <inheritdoc/>
        public IDictionary<string, object?> AttributeValuePairs { get; }
            = new Dictionary<string, object?>();
        
        /// <inheritdoc/>
        public IDictionary<string, IAdapterHandlerEntityResult> AttributeEntityValuePairs { get; }
            = new Dictionary<string, IAdapterHandlerEntityResult>();

        /// <inheritdoc/>
        public IDictionary<string, IEnumerable<IAdapterHandlerEntityResult>> 
            AttributeEntityValueCollectionPairs { get; }
            = new Dictionary<string, IEnumerable<IAdapterHandlerEntityResult>>();

        /// <inheritdoc/>
        public IEnumerable<string> ResetAttributeValues { get; }
            = new List<string>();


        internal static AdapterHandlerEntityResult Create<TEntityInputInterface>(
            Guid? identifier,
            BaseEntityInput entityInput)
            where TEntityInputInterface : IEntityInput
        {
            return new AdapterHandlerEntityResult(identifier, entityInput, typeof(TEntityInputInterface));
        }

        /// <inheritdoc/>
        [Obsolete("For backwards compatibility only - use static Create factory method instead.")]
        protected AdapterHandlerEntityResult(
            IDictionary<string, object> attributeValuePairs,
            Guid? identifier,
            string entityType)
        {
            Identifier = identifier;
            EntityType = entityType;
        }

        /// <summary>
        /// Public constructor as it is used by Aodb Component (PostMapping invoker)
        /// </summary>
        /// <param name="identifier">Id of the matched entity</param>
        /// <param name="entityInput">The mapped entity input</param>
        /// <param name="entityInputInterfaceType">Interface type of the mapped entity input</param>
        public AdapterHandlerEntityResult(
            Guid? identifier,
            BaseEntityInput entityInput,
            Type entityInputInterfaceType)
        {
            Identifier = identifier;
            EntityType = entityInput.EntityType;

            foreach (PropertyInfo prop in entityInputInterfaceType.GetProperties())
            {
                AddValueToRelevantAttributeDictionary(
                    prop, prop.GetValue(entityInput), entityInput.IncludedAttributes);
            }
        }

        private void AddValueToRelevantAttributeDictionary(
            PropertyInfo prop, object? value, IEnumerable<string> includedAttributes)
        {
            if (value != null || includedAttributes.Contains(prop.Name))
            {
                var propName = prop.Name.ToSnakeCase();

                // If the value is a complex attribute value, then serialize it to JSON
                if (value is IComplexAttributeInput<IComplexAttribute> valueAsComplexAttributeInput)
                {
                    AddComplexAttributeValue(propName, valueAsComplexAttributeInput);
                    return;
                }

                // If the value is a contained sub entity, extract it and map it to a 
                // IAdapterHandlerEntityResult that can be put into AttributeEntityValuePairs.
                if (TryExtractAdapterHandlerEntityResultFromValue(value,
                    out IAdapterHandlerEntityResult? adapterHandlerEntityResult))
                {
                    if (adapterHandlerEntityResult != null)
                    {
                        AttributeEntityValuePairs.Add(propName, adapterHandlerEntityResult);
                    }
                    return;
                }

                // If the value is a list of contained sub entities, extract it and map it to a list of 
                // IAdapterHandlerEntityResults that can be put into AttributeEntityValueCollectionPairs.
                if (TryExtractListOfAdapterHandlerEntityResultFromValue(value,
                    out IList<IAdapterHandlerEntityResult> listOfAdapterHandlerEntityResult))
                {
                    if (listOfAdapterHandlerEntityResult.Count > 0)
                    {
                        AttributeEntityValueCollectionPairs.Add(propName, listOfAdapterHandlerEntityResult);
                    }
                    return;
                }

                // Finally, if it is just a normal value, add it to AttributeValuePairs
                AttributeValuePairs.Add(propName, value);
            }
        }

        private void AddComplexAttributeValue(string propName, IComplexAttributeInput<IComplexAttribute> valueAsComplexAttributeInput)
        {
            if (valueAsComplexAttributeInput.IgnoreWhenEmpty && valueAsComplexAttributeInput.Item.ValueIsEmpty())
            {
                // Do not serialize and empty JSON structure in this case.
                return;
            }
            // The cast to (object) ensures that we do not serialize the value as an (empty) interface but using the specific object type
            AttributeValuePairs.Add(propName, JsonSerializer.Serialize((object)valueAsComplexAttributeInput.Item));
        }

        private bool TryExtractAdapterHandlerEntityResultFromValue(
            object? value,
            out IAdapterHandlerEntityResult? result)
        {
            if (value is ICreateAdapterHandlerEntityResult
                valueAsICreateAdapterHandlerEntityResult)
            {
                result = valueAsICreateAdapterHandlerEntityResult.CreateAdapterHandlerEntityResult();
                return true;
            }
            result = null;
            return false;
        }

        private bool TryExtractListOfAdapterHandlerEntityResultFromValue(
            object? value,
            out IList<IAdapterHandlerEntityResult> result)
        {
            result = new List<IAdapterHandlerEntityResult>();

            if (value is IEnumerable<object>
                valueAsIEnumerable)
            {
                foreach (var entry in valueAsIEnumerable.OfType<ICreateAdapterHandlerEntityResult>())
                {
                    var adapterHandlerEntityResult =
                        entry.CreateAdapterHandlerEntityResult();
                    if (adapterHandlerEntityResult != null)
                    {
                        result.Add(adapterHandlerEntityResult);
                    }
                }
                return true;
            }

            return false;
        }
    }
}