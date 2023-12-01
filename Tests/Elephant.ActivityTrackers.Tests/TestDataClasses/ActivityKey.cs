namespace Elephant.ActivityTrackers.Tests.TestDataClasses
{
	/// <summary>
	/// Activity key test data class for testing classes being used as keys.
	/// </summary>
	internal class ActivityKey
	{
		/// <summary>
		/// Test string.
		/// </summary>
		public Guid Key { get; set; } = Guid.Empty;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ActivityKey()
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ActivityKey(Guid key)
		{
			Key = key;
		}
	}
}
