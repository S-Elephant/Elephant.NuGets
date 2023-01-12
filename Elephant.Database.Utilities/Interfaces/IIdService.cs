using System;

namespace Elephant.Database.Utilities.Interfaces
{
    /// <summary>
    /// Auto increment id helper for determining if an id is an insert or update id.
    /// </summary>
    /// <remarks>You may want to use or DI this class as a singleton.</remarks>
    public interface IIdService
    {
        /// <summary>
        /// Set this to the value that equals the first id value of the auto-incrementer for the first inserted id.
        /// For MsSql this starts at 1 and for SqlLite this starts at 0. Default value is 1.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified value is equal or less than <see cref="InsertId"/>.</exception>
        int FirstInsertId { get; set; }

        /// <summary>
        /// Set this to the value that you consider to be an insert id.
        /// For MsSql this should be 0. Default value is 0.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified value is equal or greater than <see cref="FirstInsertId"/>.</exception>
        int InsertId { get; set; }

        /// <summary>
        /// Return true if <paramref name="id"/> is an insert id.
        /// </summary>
        bool IsIdInsert(int id);

        /// <summary>
        /// Return true if <paramref name="id"/> is an update id.
        /// </summary>
        bool IsIdUpdate(int id);

        /// Return true if <paramref name="id"/> is not an insert id.
        bool IsIdNotInsert(int id);

        /// <summary>
        /// Return true if <paramref name="id"/> is not an update id.
        /// </summary>
        bool IsIdNotUpdate(int id);

        /// <summary>
        /// Return true if the <paramref name="id"/> is smaller than the <see cref="FirstInsertId"/>.
        /// </summary>
        bool IsInvalid(int id);

        /// <summary>
        /// Return true if the <paramref name="id"/> is equal or greater than the <see cref="FirstInsertId"/>.
        /// </summary>
        bool IsValid(int id);
    }
}