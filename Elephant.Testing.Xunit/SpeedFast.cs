using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
    /// <summary>
    /// Fast test.
    /// </summary>
    [TraitDiscoverer("Elephant.Testing.Xunit.SpeedFastDiscoverer", "Elephant.Testing.Xunit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SpeedFast : Attribute, ITraitAttribute
    {
    }

    /// <summary>
    /// <see cref="SpeedFast"/> <see cref="ITraitDiscoverer"/>.
    /// </summary>
    public class SpeedFastDiscoverer : ITraitDiscoverer
    {
        private const string Key = "Speed";
        private const string Value = "Fast";

        /// <inheritdoc cref="ITraitDiscoverer.GetTraits(IAttributeInfo)"/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>(Key, Value);
        }
    }
}