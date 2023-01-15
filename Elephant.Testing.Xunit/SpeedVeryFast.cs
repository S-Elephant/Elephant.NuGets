using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
    /// <summary>
    /// Very fast test.
    /// </summary>
    [TraitDiscoverer("Elephant.Testing.Xunit.SpeedVeryFastDiscoverer", "Elephant.Testing.Xunit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SpeedVeryFast : Attribute, ITraitAttribute
    {
    }

	/// <summary>
	/// <see cref="SpeedVeryFast"/> <see cref="ITraitDiscoverer"/>.
	/// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Group related items for clarity.")]
    public class SpeedVeryFastDiscoverer : ITraitDiscoverer
    {
        private const string Key = "Speed";
        private const string Value = "VeryFast";

        /// <inheritdoc cref="ITraitDiscoverer.GetTraits(IAttributeInfo)"/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>(Key, Value);
        }
    }
}