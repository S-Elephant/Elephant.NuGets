using System;
using System.Security.Cryptography;
using System.Text;

namespace Elephant.Uuidv5Utilities
{
	/// <summary>
	/// UUID v5 helper functions.
	/// </summary>
	public static class Uuidv5Utils
	{
		/// <summary>
		/// Generates a version 5 UUID based on a namespace ID and a name.
		/// </summary>
		/// <param name="namespaceId">The namespace identifier represented as a <see cref="Guid"/>.</param>
		/// <param name="name">The unique name within the specified namespace. An empty string as well as special characters are also allowed.</param>
		/// <returns>A version 5 UUID as a <see cref="Guid"/>.</returns>
		public static Guid GenerateGuid(Guid namespaceId, string name)
		{
			// Convert the name to its UTF-8 representation.
			byte[] nameBytes = Encoding.UTF8.GetBytes(name);

			// Convert the namespace GUID to a byte array.
			byte[] namespaceBytes = namespaceId.ToByteArray();

			// Convert the namespace byte array to the byte order specified in RFC 4122. For more info see: https://datatracker.ietf.org/doc/html/rfc4122.
			SwapByteOrder(namespaceBytes);

			// For storing the SHA-1 hash.
			byte[] hash;

			using (SHA1 algorithm = SHA1.Create())
			{
				// Compute the SHA-1 hash for the namespace.
				algorithm.TransformBlock(namespaceBytes, 0, namespaceBytes.Length, null, 0);

				// Compute the SHA-1 hash for the name.
				algorithm.TransformFinalBlock(nameBytes, 0, nameBytes.Length);

				// Retrieve the computed hash.
				hash = algorithm.Hash;
			}

			// Initialize the result array to store the first 16 bytes of the hash.
			byte[] result = new byte[16];

			// Copy the first 16 bytes of the hash to the result array.
			Array.Copy(hash, 0, result, 0, 16);

			// Set the 4 bits of 7th byte to indicate that this is a version 5 UUID.
			result[6] = (byte)((result[6] & 0x0F) | (5 << 4));

			// Set the bits of the 9th byte to the RFC 4122 variant. For more info see: https://datatracker.ietf.org/doc/html/rfc4122.
			result[8] = (byte)((result[8] & 0x3F) | 0x80);

			// Convert the result byte array to the byte order used by .NET.
			SwapByteOrder(result);

			// Create and return a GUID object from the result byte array.
			return new Guid(result);
		}

		/// <summary>
		/// Swaps the byte order of a GUID to conform to RFC 4122.
		/// </summary>
		/// <param name="guidBytes">Byte array representing the GUID to have its byte order swapped.</param>
		private static void SwapByteOrder(byte[] guidBytes)
		{
			// Reverse the first 4 bytes.
			Array.Reverse(guidBytes, 0, 4);

			// Reverse the 5th and 6th bytes.
			Array.Reverse(guidBytes, 4, 2);

			// Reverse the 7th and 8th bytes.
			Array.Reverse(guidBytes, 6, 2);
		}
	}
}
