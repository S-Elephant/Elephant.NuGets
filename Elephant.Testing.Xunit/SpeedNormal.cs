using Xunit.Abstractions;
using Xunit.Sdk;

namespace Elephant.Testing.Xunit
{
    [TraitDiscoverer("Elephant.Testing.Xunit.SpeedNormalDiscoverer", "Elephant.Testing.Xunit")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SpeedNormal : Attribute, ITraitAttribute
    {
    }
 
    public class SpeedNormalDiscoverer : ITraitDiscoverer
    {
        private const string Key = "Speed";
        private const string Value = "Normal";
        
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>(Key, Value);
        }
    }
}