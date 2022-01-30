using Microsoft.Extensions.Configuration;

namespace Elephant.Common
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.Configuration"/> helper class.
    /// </summary>
    public class ConfigurationHelper
    {
        /// <summary>
        /// Retrieves an <see cref="IConfigurationSection"/> and throws if not found.
        /// </summary>
        public static IConfigurationSection GetSection(IConfiguration configuration, string sectionKey)
        {
            IConfigurationSection section = configuration.GetSection(sectionKey);
            if (!section.Exists())
                throw new Exception($"Configuration section with key \"{sectionKey}\" not found. If you're sure that the section exists, then please also ensure that the configuration itself exists (CopyIfNewer and such).");
            return section;
        }
    }
}
