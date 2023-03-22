namespace Elephant.Types.Interfaces
{
    /// <summary>
    /// Interface with an <see cref="IsEnabled"/> property.
    /// </summary>
    public interface IIsEnabled
    {
        /// <summary>
        /// Determines whether this object or entity is enabled or not.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
