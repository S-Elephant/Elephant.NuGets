using Microsoft.EntityFrameworkCore;

namespace Elephant.Database.Extensions
{
	/// <summary>
	/// Provides extension methods for <see cref="DbSet{TEntity}"/>.
	/// </summary>
	public static class DbSetExtensions
	{
		/// <summary>
		/// Clear the specified <see cref="DbSet{TEntity}"/>.
		/// </summary>
		/// <param name="dbSet">Entity to perform it on.</param>
		/// <returns><paramref name="dbSet"/>.</returns>
		/// <remarks>Does not reset the auto increment seed and does not
		/// perform very well for large datasets but is compatible with
		/// in-memory databases.</remarks>
		public static DbSet<TEntity> Clear<TEntity>(this DbSet<TEntity> dbSet)
			where TEntity : class
		{
			foreach (TEntity entity in dbSet)
			{
				dbSet.Remove(entity);
			}

			return dbSet;
		}
	}
}
