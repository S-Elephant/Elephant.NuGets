using Elephant.Constants.SqlServer;
using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elephant.CodeFirst.SqlServer
{
	/// <summary>
	/// Configuration helper.
	/// </summary>
	public static class ConfigurationHelper
	{
		/// <summary>
		/// Creates a table with an auto-incrementing Id column of type int as the primary key.
		/// </summary>
		public static string ToIdTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null)
			where TEntity : class, IId
		{
			tableName = ToTableName<TEntity>(tableName);
			builder.ToTable(tableName, schema);

			builder.HasKey(p => p.Id)
				.HasName($"PK_{tableName}");

			builder.Property(p => p.Id)
				.ValueGeneratedOnAdd()
				.IsRequired();

			return tableName;
		}

		/// <summary>
		/// Creates a table with an auto-incrementing Id column of type int as the primary key and a Name column.
		/// </summary>
		public static void ToIdNameTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null)
			where TEntity : class, IId, IName
		{
			ToIdTableWithPrimaryKey(ref builder, schema, tableName);

			builder.Property(p => p.Name)
				.HasColumnType(DbTypes.Name)
				.IsRequired();
		}

		/// <summary>
		/// Creates a table with an automatic GUID column of type int as the primary key.
		/// </summary>
		public static void ToGuidTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null)
			where TEntity : class, IGuid
		{
			tableName = ToTableName<TEntity>(tableName);
			builder.ToTable(tableName, schema);

			builder.HasKey(p => p.Guid)
				.HasName($"PK_{tableName}");

			builder.Property(p => p.Guid)
				.HasColumnType(DbTypes.Guid)
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("NEWID()")
				.IsRequired();
		}

		/// <summary>
		/// Creates a table with an automatic GUID column of type int as the primary key and a Name column.
		/// </summary>
		public static void ToGuidNameTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null)
			where TEntity : class, IGuid, IName
		{
			ToGuidTableWithPrimaryKey(ref builder, schema, tableName);

			builder.Property(p => p.Name)
			 .HasColumnType(DbTypes.Name)
			 .IsRequired();
		}

		/// <summary>
		/// Adds a <see cref="IName.Name"/> column to the table.
		/// </summary>
		public static void AddName<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdName
		{
			builder.Property(p => p.Name)
				.HasColumnType(DbTypes.Name)
				.IsRequired();
		}

		/// <summary>
		/// Adds a <see cref="IIdNameDescription.Description"/> column to the table.
		/// </summary>
		public static void AddDescription<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdNameDescription
		{
			builder.Property(p => p.Description)
				.HasColumnType(DbTypes.NVarCharMax)
				.IsRequired();
		}

		/// <summary>
		/// Adds both a <see cref="IName.Name"/> and a <see cref="IIdNameDescription.Description"/> columns to the table.
		/// </summary>
		public static void AddNameAndDescription<T>(ref EntityTypeBuilder<T> builder)
			where T : class, IIdNameDescription
		{
			AddName(ref builder);
			AddDescription(ref builder);
		}

		/// <summary>
		/// Adds an <see cref="IIsEnabled.IsEnabled"/> column to the table.
		/// </summary>
		public static void AddIsEnabled<T>(ref EntityTypeBuilder<T> builder, bool defaultValue = true)
			where T : class, IIsEnabled
		{
			builder.Property(p => p.IsEnabled)
				.HasColumnType(DbTypes.Bool)
				.HasDefaultValue(defaultValue)
				.IsRequired();
		}

		private static string ToTableName<T>(string? tableName) => tableName ?? typeof(T).Name;
	}
}
