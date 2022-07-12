namespace Elephant.Common.Entities
{
    /// <summary>
    /// Base class with <see cref="Id"/> and <see cref="Name"/> propreties.
    /// </summary>
    public abstract class BaseIdName : BaseId, IIdName
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
