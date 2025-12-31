namespace Elephant.Types.Interfaces
{
	/// <summary>
	/// Interface with <see cref="IId.Id"/>, <see cref="IName.Name"/> and <see cref="Description"/> properties.
	/// </summary>
	public interface IIdNameDescription : IIdName
	{
		/// <summary>
		/// Description.
		/// </summary>
		string Description { get; set; }
	}
}
