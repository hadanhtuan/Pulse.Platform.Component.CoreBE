using System;

namespace Database.Models.Data
{
    /// <summary>
    /// Reasons for processing an EntityValue. 
    /// 
    /// The reasons are ordered by how important the update is, 
    /// starting with updates triggered by rules. The special Priority
    /// reason has the highest importance, so that it is possible to query 
    /// ordered by priority.
    /// </summary>
    [Flags]
    public enum ProcessingReasons
    {
        None                = 0b_0000_0000_0000, // 0
        BusinessRulesUpdate = 0b_0000_0000_0010, // 2
        MasterdataUpdate    = 0b_0000_0000_1000, // 8 
        RuleTrigger         = 0b_0000_0010_0000, // 32
        Scheduled           = 0b_0000_0100_0000, // 64

        /// <summary>
        /// The update to the entity is of priority (e.g., inside operational window)
        /// </summary>
        Priority       = 0b_0001_0000_0000_0000, // 4096
    }
}