using Database.Models.Data;

namespace Database.Extensions
{
    public static class JsonModelMappingExtensions
    {
        public static Models.Data.Json.EntityValueAlert ToJsonModel(this EntityValueAlert a)
        {
            return new Models.Data.Json.EntityValueAlert(
                a.AlertType, 
                a.AlertTypeDescription, 
                a.AlertMessage, 
                a.CausedBy, 
                a.EnrichmentContextId, 
                isExistingAlert: true)
            {
                AlertLevel = a.AlertLevel,
                AlertStatus = a.AlertStatus,
                LastChangedState = a.LastChangedState,
                MasterDataLookUpValue = a.LookUpValue,
                MasterDataSourceTable = a.MasterDataSourceTable,
                UserRole = a.UserRole
            };
        }

    }
}
