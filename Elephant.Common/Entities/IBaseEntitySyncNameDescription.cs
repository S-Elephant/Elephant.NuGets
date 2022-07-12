namespace Elephant.Common.Entities
{
    /// <summary>
    /// Common entity properties for record synching.
    /// </summary>
    public interface IBaseEntitySyncNameDescription : IBaseEntitySyncName
    {
        /// <summary>
        /// Description.
        /// </summary>
        string Description { get; set; }
    }
}
