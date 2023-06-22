using Microsoft.EntityFrameworkCore;

namespace Elephant.Database.Tests.InMemory
{
	/// <summary>
	/// In-memory database helper.
	/// </summary>
	internal class DatabaseHelper
	{
		/// <summary>
		/// In-memory context.
		/// </summary>
		internal TestContext TestContext { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		public DatabaseHelper()
		{
			DbContextOptionsBuilder<TestContext> builder = new();
			builder.UseInMemoryDatabase(databaseName: "TestDbInMemory");
			TestContext = new TestContext(builder.Options);

			// Delete existing database before creating a new one.
			TestContext.Database.EnsureDeleted();
			TestContext.Database.EnsureCreated();
		}
	}
}
