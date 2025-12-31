namespace Elephant.Extensions
{
	/// <summary>
	/// <see cref="Stream"/> extensions.
	/// </summary>
	public static class StreamExtensions
	{
		/// <summary>
		/// Convert <paramref name="stream"/> into a Byte array.
		/// </summary>
		/// <param name="stream">Stream to convert. Returns an empty array if null.</param>
		/// <param name="bufferSize">Buffer size for reading the stream.</param>
		public static byte[] ToByteArray(this Stream? stream, int bufferSize)
		{
			if (stream == null)
				return [];

			byte[] buffer = new byte[bufferSize];
			using MemoryStream memoryStream = new();
			int read;
			while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
				memoryStream.Write(buffer, 0, read);

			return memoryStream.ToArray();
		}
	}
}
