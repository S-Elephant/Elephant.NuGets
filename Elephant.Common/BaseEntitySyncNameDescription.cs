namespace Elephant.Common
{
    /// <summary>
    /// Common entity properties for record synching, with name and description.
    /// </summary>
    public abstract class BaseEntitySyncNameDescription : BaseEntitySyncName, IBaseEntitySyncNameDescription, IIdNameDescription
    {
        /// <summary>
        /// <inheritdoc cref="IIdNameDescription.Description" />
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
