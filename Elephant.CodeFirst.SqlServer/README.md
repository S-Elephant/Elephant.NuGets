# About

Contains Code First configuration helper methods for MS SQL Server.

# ConfigurationHelper

```
string ToIdTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IId

void ToIdNameTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IId, IName

void ToGuidTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IGuid

void ToGuidNameTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IGuid, IName

void AddName<T>(ref EntityTypeBuilder<T> builder) where T : class, IIdName

void AddDescription<T>(ref EntityTypeBuilder<T> builder) where T : class, IIdNameDescription

void AddNameAndDescription<T>(ref EntityTypeBuilder<T> builder) where T : class, IIdNameDescription

void AddIsEnabled<T>(ref EntityTypeBuilder<T> builder, bool defaultValue = true) where T : class, IIsEnabled
```



