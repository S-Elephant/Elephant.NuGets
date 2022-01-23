namespace Elephant.Common
{
    /// <summary>
    /// Common entity properties for record synching.
    /// </summary>
    public interface IBaseEntitySync : IId
    {
        /// <summary>
        /// The timestamp of when this entity was last created or modified.
        /// </summary>
        DateTime LastModifiedOn { get; set; }

        /// <summary>
        /// The unique identifier of the user that was the last user that created or modified this entity or null if nobody.
        /// </summary>
        int? LastModifiedBy { get; set; }

        /// <summary>
        /// The unique identifier of the user that created this entity or null if no user created it.
        /// </summary>
        int? CreatedBy { get; set; }
    }
}
