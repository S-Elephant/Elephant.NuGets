using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
	/// <summary>
	/// Very slow test.
	/// </summary>
	[TraitDiscoverer("Elephant.Testing.Xunit.SpeedVerySlowDiscoverer", "Elephant.Testing.Xunit")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class SpeedVerySlow : Attribute, ITraitAttribute
	{
	}

	/// <summary>
	/// <see cref="SpeedVerySlow"/> <see cref="ITraitDiscoverer"/>.
	/// </summary>
#if DEBUG
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Group related items for clarity.")]
#endif
	public class SpeedVerySlowDiscoverer : ITraitDiscoverer
	{
		private const string Key = "Speed";
		private const string Value = "VerySlow";

		/// <inheritdoc cref="ITraitDiscoverer.GetTraits(IAttributeInfo)"/>
		public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
		{
			yield return new KeyValuePair<string, string>(Key, Value);
		}
	}
}