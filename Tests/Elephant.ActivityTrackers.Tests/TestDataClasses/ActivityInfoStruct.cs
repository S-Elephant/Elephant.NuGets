namespace Elephant.ActivityTrackers.Tests.TestDataClasses
{
	/// <summary>
	/// ?
	/// </summary>
	internal struct ActivityInfoStruct
	{
		/// <summary>
		/// Test string.
		/// </summary>
		public string ValueString { get; set; } = string.Empty;

		/// <summary>
		/// Test int.
		/// </summary>
		public int ValueInt { get; set; }

		/// <summary>
		/// Random unused property.
		/// </summary>
		public int SomethingElse { get; set; }

		/// <summary>
		/// ?
		/// </summary>
		public ActivityInfoStruct()
		{
			ValueString = string.Empty;
			ValueInt = 0;
			SomethingElse = 0;
		}

		/// <summary>
		/// ?
		/// </summary>
		public ActivityInfoStruct(string valueString, int valueInt)
		{
			ValueString = valueString;
			ValueInt = valueInt;
			SomethingElse = 0;
		}
	}
}
