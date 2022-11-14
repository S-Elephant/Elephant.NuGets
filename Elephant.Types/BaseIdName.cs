namespace Elephant.Types
{
    /// <summary>
    /// Base class with <see cref="Id"/> and <see cref="Name"/> propreties.
    /// </summary>
    public abstract class BaseIdName : BaseId, IIdName
    {
        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;
    }
}
