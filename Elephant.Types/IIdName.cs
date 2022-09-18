namespace Elephant.Types
{
	/// <summary>
	/// Interface with <see cref="Id"/> and <see cref="Name"/> propreties.
	/// </summary>
	public interface IIdName : IId
	{
		/// <summary>
		/// Name.
		/// </summary>
		string Name { get; set; }
	}
}
