using System;

namespace Elephant.Uuidv5Utilities
{
	/// <inheritdoc cref="IUuidv5"/>
	public class Uuidv5 : IUuidv5
	{
		/// <inheritdoc cref="Uuidv5Utils.GenerateGuid"/>
		public Guid GenerateGuid(Guid namespaceId, string name)
		{
			return Uuidv5Utils.GenerateGuid(namespaceId, name);
		}
	}
}
