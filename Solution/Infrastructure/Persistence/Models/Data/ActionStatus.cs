namespace Database.Models.Data
{
    public enum ActionStatus
    {
        /// <summary>
        /// Default status when assigned and an action has neither been executed, nor reverted
        /// </summary>
        Undefined,

        /// <summary>
        /// Status when an action has been executed normally
        /// </summary>
        Executed,

        /// <summary>
        /// Status when an action has been reverted.
        /// 
        /// If action A is run, its status is represented by "Executed" until it has been reverted
        /// which is indicated by updating the ActionStatus.
        /// </summary>
        Reverted
    }
}
