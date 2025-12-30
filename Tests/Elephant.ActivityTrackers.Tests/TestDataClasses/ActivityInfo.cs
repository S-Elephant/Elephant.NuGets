// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Elephant.ActivityTrackers.Tests.TestDataClasses
{
	/// <summary>
	/// Activity info test data class for testing classes.
	/// </summary>
	internal sealed class ActivityInfo
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
		/// Constructor.
		/// </summary>
		public ActivityInfo()
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ActivityInfo(string valueString, int valueInt)
		{
			ValueString = valueString;
			ValueInt = valueInt;
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			ActivityInfo other = (ActivityInfo)obj;
			return ValueString == other.ValueString && ValueInt == other.ValueInt && SomethingElse == other.SomethingElse;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return HashCode.Combine(ValueString, ValueInt, SomethingElse);
		}
	}
}
