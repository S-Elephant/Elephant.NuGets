namespace Elephant.Types
{
	/// <summary>
	/// Interface with <see cref="Id"/>, <see cref="Name"/> and <see cref="Description"/> propreties.
	/// </summary>
	public interface IIdNameDescription : IIdName
	{
		/// <summary>
		/// Description.
		/// </summary>
		string Description { get; set; }
	}
}
