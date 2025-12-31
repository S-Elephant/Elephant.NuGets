using Elephant.Constants.Sqlite;
using Elephant.Types.Interfaces;
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
		/// Note that SQLite does not support schemas.
		/// </summary>
		public static string ToIdTableWithPrimaryKey<T>(ref EntityTypeBuilder<T> builder, string? tableName = null)
			where T : class, IId
		{
			tableName = ToTableName<T>(tableName);
			_ = builder.ToTable(tableName);

			_ = builder.HasKey(p => p.Id)
				.HasName($"PK_{ToTableName<T>(tableName)}");

			_ = builder.Property(p => p.Id)
				.ValueGeneratedOnAdd()
				.IsRequired();

			return tableName;
		}

		/// <summary>
		/// Adds a <see cref="IIdName"/>.Name field to the table.
		/// </summary>
		public static void AddName<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdName
		{
			_ = builder.Property(p => p.Name)
				.HasColumnType(DbType.Name)
				.IsRequired();
		}

		/// <summary>
		/// Adds a <see cref="IIdNameDescription"/>.Description field to the table.
		/// </summary>
		public static void AddDescription<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdNameDescription
		{
			_ = builder.Property(p => p.Description)
				.HasColumnType(DbType.TextMax)
				.IsRequired();
		}

		/// <summary>
		/// Adds both a <see cref="IIdName"/>.Name and <see cref="IIdNameDescription"/>.Description fields to the table.
		/// </summary>
		public static void AddNameAndDescription<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdNameDescription
		{
			AddName(ref builder);
			AddDescription(ref builder);
		}

		/// <summary>
		/// Adds an <see cref="IIsEnabled"/>.IsEnabled field to the table.
		/// </summary>
		public static void AddIsEnabled<T>(ref EntityTypeBuilder<T> builder, bool defaultValue = true)
			where T : class, IIsEnabled
		{
			_ = builder.Property(p => p.IsEnabled)
				.HasColumnType(DbType.Bool)
				.HasDefaultValue(defaultValue)
				.IsRequired();
		}

		/// <summary>
		/// Returns the specified table name or, if null, the name of type <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">Entity type whose name will be used as the table name if <paramref name="tableName"/> is null.</typeparam>
		/// <param name="tableName">Optional table name. If null, the name of type <typeparamref name="T"/> is used.</param>
		/// <returns>
		/// Table name to use which is either the provided <paramref name="tableName"/> or the name of type <typeparamref name="T"/>.
		/// </returns>
		private static string ToTableName<T>(string? tableName)
		{
			return tableName ?? typeof(T).Name;
		}
	}
}
