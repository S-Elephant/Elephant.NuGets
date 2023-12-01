namespace Elephant.ActivityTrackers
{
	/// <summary>
	/// Tracker for different activities, counting the number of times each
	/// activity occurs.
	/// </summary>
	public class ActivityTracker : ActivityTrackerGeneric<string, bool>
	{
		/// <summary>
		/// Adds a count to the specified activity.
		/// </summary>
		/// <param name="activityKey">The name of the activity to add a count to.</param>
		public void Add(string activityKey)
		{
			Add(activityKey, false);
		}
	}
}
