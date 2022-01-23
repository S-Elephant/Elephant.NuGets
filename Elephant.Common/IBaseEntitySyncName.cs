﻿namespace Elephant.Common
{
    /// <summary>
    /// Common entity properties for record synching.
    /// </summary>
    public interface IBaseEntitySyncName : IBaseEntitySync
    {
        /// <summary>
        /// Name.
        /// </summary>
        string Name { get; set; }
    }
}
