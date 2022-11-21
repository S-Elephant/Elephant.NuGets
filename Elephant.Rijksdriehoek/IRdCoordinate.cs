#if !NETSTANDARD2_0 && !NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif

namespace Elephant.Rijksdriehoek
{
    /// <summary>
    /// A struct for Rijksdriehoekscoördinaten using <typeparamref name="TType"/>.
    /// For more info see: https://nl.wikipedia.org/wiki/Rijksdriehoeksco%C3%B6rdinaten.
    /// </summary>
    public interface IRdCoordinate<TType>
    {
        /// <summary>
        /// Return true if this Rijksdriehoek coordinate is valid.
        /// </summary>
        bool IsValid { get; }
        
        /// <summary>
        /// X Rd coordinate.
        /// </summary>
        TType X { get; set; }

        /// <summary>
        /// Y Rd coordinate.
        /// </summary>
        TType Y { get; set; }

        /// <summary>
        /// Return the distance between 2 Rd coordinates.
        /// </summary>
        /// <param name="otherRdCoordinate">The other coordinate.</param>
        TType Distance(IRdCoordinate<TType> otherRdCoordinate);

        /// <summary>
        /// Return the distance between 2 Rd coordinates. If <paramref name="otherRdCoordinate"/> is null then it'll return 0f.
        /// </summary>
        /// <param name="otherRdCoordinate">The other coordinate.</param>
        TType Distance(RdCoordinate? otherRdCoordinate);
        
        /// <summary>
        /// Indicates whether this instance and the specific <paramref name="obj"/> are equal.
        /// </summary>
        /// <param name="obj">The other object.</param>
#if NETSTANDARD2_0 || NETSTANDARD2_1
        bool Equals(object? obj);
#else
        bool Equals([NotNullWhen(true)] object? obj);
#endif

        /// <summary>
        /// Return the hash code for this instance.
        /// </summary>
        int GetHashCode();
    }
}