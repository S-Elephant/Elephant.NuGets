namespace Elephant.Common.Entities
{
    /// <summary>
    /// Base class with <see cref="Id"/>, <see cref="Name"/> and <see cref="Description"/> properties.
    /// </summary>
    public abstract class BaseIdNameDescription : BaseIdName, IIdNameDescription
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
