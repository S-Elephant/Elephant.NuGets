namespace Elephant.Types.Interfaces
{
    /// <summary>
    /// Interface with a <see cref="Guid"/> property.
    /// </summary>
    public interface IGuid
    {
        /// <summary>
        /// Unique identifier (GUID).
        /// </summary>
        IGuid Guid { get; set; }
    }
}
