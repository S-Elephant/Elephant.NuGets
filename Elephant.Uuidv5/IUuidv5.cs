using System;

namespace Elephant.Uuidv5Utilities
{
	/// <inheritdoc cref="Uuidv5Utils"/>
	public interface IUuidv5
	{
		/// <inheritdoc cref="Uuidv5Utils.GenerateGuid"/>
		Guid GenerateGuid(Guid namespaceId, string name);
	}
}