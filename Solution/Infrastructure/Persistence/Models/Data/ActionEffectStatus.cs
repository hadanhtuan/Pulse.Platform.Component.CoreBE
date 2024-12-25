namespace Database.Models.Data
{
    public enum ActionEffectStatus
    {
        /// <summary>
        /// Default value when its neither Added, nor Deleted
        /// </summary>
        Undefined,

        /// <summary>
        /// Reflects that the owner of the enum has been added by an action
        /// </summary>
        Added,

        /// <summary>
        /// Reflects that the towner of the enum has been deleted by an action
        /// </summary>
        Deleted
    }
}
