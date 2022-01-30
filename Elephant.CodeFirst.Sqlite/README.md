# Example usage
builder.Property(p => p.TodoStatus)
	.HasColumnType(DbType.Enum)
	.HasConversion<int>()
	.IsRequired();