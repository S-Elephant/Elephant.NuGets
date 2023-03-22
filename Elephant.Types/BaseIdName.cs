using Elephant.Types.Interfaces;

namespace Elephant.Types
{
    /// <summary>
    /// Base class with <see cref="BaseId.Id"/> and <see cref="Name"/> properties.
    /// </summary>
    public abstract class BaseIdName : BaseId, IIdName
    {
        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;
    }
}
