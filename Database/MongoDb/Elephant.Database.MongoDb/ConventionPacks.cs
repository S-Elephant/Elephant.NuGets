using MongoDB.Bson.Serialization.Conventions;

namespace Elephant.Database.MongoDb
{
	/// <summary>
	/// MongoDB convention packs.
	/// </summary>
	/// <remarks>A list of all convention packs can be found here: https://mongodb.github.io/mongo-csharp-driver/2.19/apidocs/html/N_MongoDB_Bson_Serialization_Conventions.htm</remarks>
	public static class ConventionPacks
	{
		/// <summary>
		/// Enforce global camelCase for MongoDB elements to eliminate the need to require
		/// a [BsonElement("yourCamelCaseProperty")] on every entity property anymore.
		/// </summary>
		/// <param name="namespaces">
		/// Case sensitive array of all namespaces that this convention should apply to.
		/// If empty, it will apply to everything.
		/// Default is empty.
		/// </param>
		/// <remarks>
		/// More info: https://mongodb.github.io/mongo-csharp-driver/2.19/apidocs/html/T_MongoDB_Bson_Serialization_Conventions_CamelCaseElementNameConvention.htm
		/// </remarks>
		public static void EnforceGlobalCamelCase(params string[] namespaces)
		{
			ConventionPack conventionPack = new() { new CamelCaseElementNameConvention() };
			const string conventionName = "camelCase";

			// When to use.
			if (namespaces.Any())
			{
				ConventionRegistry.Register(conventionName, conventionPack, type => namespaces.Any(namespaceName => type.FullName != null && namespaceName.StartsWith(type.FullName)));
			}
			else
				ConventionRegistry.Register(conventionName, conventionPack, _ => true);
		}
	}
}
