namespace Database.Models.Data
{
    public enum ActionStatus
    {
        
        /// Default status when assigned and an action has neither been executed, nor reverted
        
        Undefined,

        
        /// Status when an action has been executed normally
        
        Executed,

        
        /// Status when an action has been reverted.
        /// 
        /// If action A is run, its status is represented by "Executed" until it has been reverted
        /// which is indicated by updating the ActionStatus.
        
        Reverted
    }
}
