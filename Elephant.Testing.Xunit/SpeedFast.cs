using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
    [TraitDiscoverer("Elephant.Testing.Xunit.SpeedFastDiscoverer", "Elephant.Testing.Xunit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SpeedFast : Attribute, ITraitAttribute
    {
    }
 
    public class SpeedFastDiscoverer : ITraitDiscoverer
    {
        private const string Key = "Speed";
        private const string Value = "Fast";
            
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>(Key, Value);
        }
    }
}