using Elephant.Types;

namespace Elephant.Database.Tests.InMemory
{
	/// <summary>
	/// Test entity.
	/// </summary>
	internal class TestEntity : BaseIdName
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public TestEntity()
		{
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public TestEntity(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}
