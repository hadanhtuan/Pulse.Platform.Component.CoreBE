namespace API.Shared.Core.Interfaces.Context.Models
{
    /// <summary>
    /// Interface used for AODB attributes defined as ComplexAttribute in the schema.
    /// </summary>
    /// <remarks>
    /// These all have the ability to determine if the value of the complex attribute as
    /// a whole is considered as empty (if all properties are null and all collections are empty).
    /// This is primarly used when serializing complex attributes to JSON.
    /// </remarks>
    public interface IComplexAttribute
    {
        /// <summary>
        /// Determines if the value of the complex attribute as a whole is considered as empty 
        /// (if all properties are null and all collections are empty).
        /// </summary>
        /// <returns></returns>
        public bool ValueIsEmpty();
    }
}
