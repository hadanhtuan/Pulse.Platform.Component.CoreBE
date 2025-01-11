using System;
using System.Text.Json.Serialization;

namespace Database.Models.Data.Json
{
    
    /// Represents a reference to a row of data in Master Data used to create an enriched value.        
    
    public class MasterDataReference
    {
        public MasterDataReference(string tableName, Guid rowId)
        {
            TableName = tableName;
            RowId = rowId;
        }

        
        /// Public, parameter-free default constructor required by the <see cref="System.Text.Json.JsonSerializer"/>
        /// in order to use <see cref="System.Text.Json.Serialization.ReferenceHandler.Preserve"/>.
        
        [JsonConstructor]
        [Obsolete("This constructor is only intended for the System.Text.Json.JsonSerializer.")]
        public MasterDataReference() : this(string.Empty, Guid.Empty) { }

        
        /// The table name MasterData that the referenced data resides in.
        
        [JsonInclude]
        public string TableName { get; private set; }

        
        /// The unique row id of the data the reference points to.
        
        /// <remarks>
        /// Using row id rather than entity id means the reference points to the exact bi-temporal version in MasterData.
        /// </remarks>
        [JsonInclude]
        public Guid RowId { get; private set; }

        public bool IsEquivalentTo(MasterDataReference other) =>
            TableName == other.TableName &&
            RowId == other.RowId;
    }
}
