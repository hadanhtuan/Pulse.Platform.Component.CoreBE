
namespace API.Shared.Core.Interfaces.Rules
{
    /// <summary>
    /// Access to points in time to use (in particular for looking up Masterdata).
    /// </summary>
    public interface IPointInTimes
    {
        /// <summary>
        /// Adds a point in time to the set of points in time to use.
        /// </summary>
        /// <param name="pointInTime"></param>
        public void AddPointInTime(DateTime pointInTime);

        /// <summary>
        /// Returns all the points in time that have been added.
        /// </summary>
        /// <returns></returns>
        public List<DateTime> GetPointInTimes();

        /// <summary>
        /// Clears all points in time that have been added.
        /// </summary>
        public void ClearPointInTimes();
    }
}