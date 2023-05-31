namespace Elephant.DataAnnotations
{
	/// <summary>
	/// Attribute that marks a property to be replaced with a null value if it's empty.
	/// This attribute itself does nothing, it only marks it.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
#pragma warning disable SA1649
	public class IfEmptyMakeItNullAttribute : Attribute
#pragma warning restore SA1649
	{
		// Attribute has no logic, you can add that yourself.
	}
}
