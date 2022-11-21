using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
    /// <summary>
    /// Normal (as in how long it takes to run) test.
    /// </summary>
    [TraitDiscoverer("Elephant.Testing.Xunit.SpeedNormalDiscoverer", "Elephant.Testing.Xunit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SpeedNormal : Attribute, ITraitAttribute
    {
    }

    /// <summary>
    /// <see cref="SpeedNormal"/> <see cref="ITraitDiscoverer"/>.
    /// </summary>
    public class SpeedNormalDiscoverer : ITraitDiscoverer
    {
        private const string Key = "Speed";
        private const string Value = "Normal";

        /// <inheritdoc cref="ITraitDiscoverer.GetTraits(IAttributeInfo)"/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>(Key, Value);
        }
    }
}