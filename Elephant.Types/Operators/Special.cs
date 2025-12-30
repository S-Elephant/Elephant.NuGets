namespace Elephant.Types.Operators
{
	/// <summary>
	/// Special operators.
	/// </summary>
	public enum Special
	{
		/// <summary>
		/// No operator.
		/// </summary>
		None,

		/// <summary>
		/// sizeof(). Size of a data type.
		/// </summary>
		SizeOf,

		/// <summary>
		/// typeof(). Type of a class.
		/// </summary>
		TypeOf,

		/// <summary>
		/// <![CDATA[&]]>. Address of a variable.
		/// </summary>
		Address,

		/// <summary>
		/// *. Pointer to a variable.
		/// </summary>
		Pointer,

		/// <summary>
		/// ? :. Conditional Expression. Example: x == 4 ? 1 : 2
		/// </summary>
		ConditionalExpression,

		/// <summary>
		/// is. Determines whether an object is of a certain type.
		/// </summary>
		Is,

		/// <summary>
		/// as. Cast without raising an exception if the cast fails.
		/// </summary>
		As,
	}
}
