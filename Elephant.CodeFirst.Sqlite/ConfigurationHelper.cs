using Elephant.Common;
using Elephant.Constants.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elephant.CodeFirst.Sqlite
{
	/// <summary>
	/// Configuration helper.
	/// </summary>
	public static class ConfigurationHelper
	{
		/// <summary>
		/// Creates a table with an auto-incrementing Id field of type int as the primary key.
		/// Note that Sqlite does not support schemas.
		/// </summary>
		public static string ToIdTableWithPrimaryKey<T>(ref EntityTypeBuilder<T> builder, string? tableName = null)
			where T : class, IId
		{
			tableName = ToTableName<T>(tableName);
			builder.ToTable(tableName);

			builder.HasKey(p => p.Id)
				.HasName($"PK_{ToTableName<T>(tableName)}");

			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd()
				.IsRequired();

			return tableName;
		}

		/// <summary>
		/// Adds a <see cref="IIdName.Name"/> field to the table.
		/// </summary>
		public static void AddName<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdName
		{
			builder.Property(p => p.Name)
				.HasColumnType(DbType.Name)
				.IsRequired();
		}

		/// <summary>
		/// Adds a <see cref="IIdNameDescription.Description"/> field to the table.
		/// </summary>
		public static void AddDescription<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdNameDescription
		{
			builder.Property(p => p.Description)
				.HasColumnType(DbType.TextMax)
				.IsRequired();
		}

		/// <summary>
		/// Adds both a <see cref="IIdName.Name"/> and <see cref="IIdNameDescription.Description"/> fields to the table.
		/// </summary>
		public static void AddNameAndDescription<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdNameDescription
		{
			AddName(ref builder);
			AddDescription(ref builder);
		}

        /// <summary>
        /// Adds an <see cref="IIsEnabled.IsEnabled"/> field to the table.
        /// </summary>
        public static void AddIsEnabled<T>(ref EntityTypeBuilder<T> builder, bool defaultValue = true)
            where T : class, IIsEnabled
        {
            builder.Property(p => p.IsEnabled)
                .HasColumnType(DbType.Bool)
                .HasDefaultValue(defaultValue)
                .IsRequired();
        }

        private static string ToTableName<T>(string? tableName) => tableName ?? typeof(T).Name;
	}
}
