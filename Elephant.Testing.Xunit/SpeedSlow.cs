using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
    [TraitDiscoverer("Elephant.Testing.Xunit.SpeedSlowDiscoverer", "Elephant.Testing.Xunit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SpeedSlow : Attribute, ITraitAttribute
    {
    }
 
    public class SpeedSlowDiscoverer : ITraitDiscoverer
    {
        private const string Key = "Speed";
        private const string Value = "Slow";
        
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>(Key, Value);
        }
    }
}