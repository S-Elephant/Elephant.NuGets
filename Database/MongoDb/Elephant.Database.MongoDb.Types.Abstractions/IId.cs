namespace Elephant.Database.MongoDb.Types.Abstractions
{
	/// <summary>
	/// Interface with an <see cref="MongoId"/> property.
	/// </summary>
	public interface IId
	{
		/// <summary>
		/// Unique identifier.
		/// Internally in MongoDb this is known as "_id" without the double quotes.
		/// Is in string format.
		/// </summary>
		string MongoId { get; set; }
	}
}
