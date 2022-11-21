using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
    /// <summary>
    /// Slow test.
    /// </summary>
    [TraitDiscoverer("Elephant.Testing.Xunit.SpeedSlowDiscoverer", "Elephant.Testing.Xunit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SpeedSlow : Attribute, ITraitAttribute
    {
    }

    /// <summary>
    /// <see cref="SpeedSlow"/> <see cref="ITraitDiscoverer"/>.
    /// </summary>
    public class SpeedSlowDiscoverer : ITraitDiscoverer
    {
        private const string Key = "Speed";
        private const string Value = "Slow";

        /// <inheritdoc cref="ITraitDiscoverer.GetTraits(IAttributeInfo)"/>
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>(Key, Value);
        }
    }
}