using Microsoft.EntityFrameworkCore;

namespace Elephant.Database
{
	/// <summary>
	/// Provides extension methods for <see cref="DbSet{TEntity}"/>.
	/// </summary>
	public static class DbSetExtensions
    {
		/// <summary>
		/// Clear the specified <see cref="DbSet{TEntity}"/>.
		/// </summary>
		/// <returns>The <paramref name="dbSet"/>.</returns>
		/// <remarks>Does not reset the auto increment seed.</remarks>
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
