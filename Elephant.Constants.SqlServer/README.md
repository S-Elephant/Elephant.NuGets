# Example usage:
```C#
builder.Property(p => p.TodoStatus)
	.HasColumnType(DbType.Enum)
	.HasConversion<int>()
	.IsRequired();
```