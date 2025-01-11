using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Database.Models.Schema
{
    
    /// Defines standard elements in the AODB schema that are required for the
    /// AODB to function.
    
    public static class StandardElements
    {
        // Entities
        public static readonly string FlightLeg = "flight_leg";
        public static readonly string GroundLeg = "ground_leg";
        public static readonly string Visit = "visit";

        // Visit attributes
        public static readonly string LinkedDueTo = "linked_due_to";
        public static readonly string Priority = "priority";

        
        /// Returns a dictionary containing an entry for each standard entity type that must
        /// exist in the schema with the entity type internal name as key and a list of the
        /// internal names of the standard attribute types that are required in the schema.
        
        public readonly static IDictionary<string, IEnumerable<string>> EntityAttributeTypes =
            new ReadOnlyDictionary<string, IEnumerable<string>>(new Dictionary<string, IEnumerable<string>>
            {
                { FlightLeg, StringValues.Empty },
                { GroundLeg, StringValues.Empty },
                // Enable as standard elements when updated to a compliant schema
                //{ Visit, new [] { LinkedDueTo, Priority } }
                { Visit, StringValues.Empty }
            });
    }
}