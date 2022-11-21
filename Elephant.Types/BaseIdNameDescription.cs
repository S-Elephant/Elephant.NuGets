namespace Elephant.Types
{
    /// <summary>
    /// Base class with <see cref="BaseId.Id"/>, <see cref="BaseIdName.Name"/> and <see cref="Description"/> properties.
    /// </summary>
    public abstract class BaseIdNameDescription : BaseIdName, IIdNameDescription
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
