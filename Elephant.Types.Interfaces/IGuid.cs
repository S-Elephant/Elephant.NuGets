using System;

namespace Elephant.Types.Interfaces
{
    /// <summary>
    /// Interface with a <see cref="System.Guid"/> property.
    /// </summary>
    public interface IGuid
    {
        /// <summary>
        /// Unique identifier (GUID).
        /// </summary>
        Guid Guid { get; set; }
    }
}
