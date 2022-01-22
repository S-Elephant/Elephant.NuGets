namespace Elephant.Common
{
    /// <summary>
    /// Base class with an <see cref="Id"/> property.
    /// </summary>
    public abstract class BaseId : IId
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Id { get; set; }
    }
}
