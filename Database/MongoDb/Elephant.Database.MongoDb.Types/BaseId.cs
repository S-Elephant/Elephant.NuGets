using System.Diagnostics.CodeAnalysis;
using Elephant.Database.MongoDb.Types.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Elephant.Database.MongoDb.Types
{
	/// <inheritdoc cref="IId"/>
	public abstract class BaseId : IId
	{
		/// <inheritdoc/>
		[BsonElement("_id")]
		[BsonRepresentation(BsonType.ObjectId)]
		public string MongoId { get; set; } = ObjectId.GenerateNewId().ToString();
	}

	/// <summary>
	/// Comparer for <see cref="BaseId"/>.
	/// </summary>
	/// <example>Distinct example on a table called "BaseIds".
	/// var result = await Context.BaseIds.Distinct(new IdComparer()).ToListAsync(cancellationToken);</example>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Group related items for clarity.")]
	public class IdComparer : IEqualityComparer<BaseId>
	{
		/// <inheritdoc cref="IEqualityComparer{T}.Equals(T, T)"/>
		public bool Equals(BaseId? x, BaseId? y)
		{
			if (x == null || y == null)
				return x == null && y == null;

			return x.MongoId.Equals(y.MongoId);
		}

		/// <inheritdoc cref="IEqualityComparer{T}.GetHashCode(T)"/>
		public int GetHashCode(BaseId obj)
		{
			return obj.MongoId.GetHashCode();
		}
	}
}