using Elephant.Database.MongoDb.Contexts;

namespace Elephant.Database.MongoDb.Attributes
{
	/// <summary>
	/// Put this attribute on your configuration classes to automatically load
	/// them by calling either <see cref="MongoContext.AutoLoadConfigurations"/> or <see cref="MongoContext.AutoLoadConfigurationsByAssemblyNames"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class AutoLoadConfigurationAttribute : Attribute
	{
	}
}
