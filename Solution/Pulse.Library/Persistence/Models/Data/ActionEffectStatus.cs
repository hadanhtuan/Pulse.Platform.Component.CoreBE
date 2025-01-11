namespace Database.Models.Data
{
    public enum ActionEffectStatus
    {
        
        /// Default value when its neither Added, nor Deleted
        
        Undefined,

        
        /// Reflects that the owner of the enum has been added by an action
        
        Added,

        
        /// Reflects that the towner of the enum has been deleted by an action
        
        Deleted
    }
}
