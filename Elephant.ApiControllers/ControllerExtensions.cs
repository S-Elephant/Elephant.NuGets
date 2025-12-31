using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Elephant.ApiControllers
{
	/// <summary>
	/// Extensions for ASP.Net Core Controllers.
	/// </summary>
	public static class ControllerExtensions
	{
		/// <summary>
		/// Convert the <paramref name="formFile"/> into a <see cref="byte"/> array.
		/// </summary>
		/// <param name="formFile"><see cref="IFormFile"/> to convert into a byte array.</param>
		/// <returns><paramref name="formFile"/> as a byte array. Returns an empty byte array if <paramref name="formFile"/> has a length smaller than 0.</returns>
		public static byte[] ToByteArray(this IFormFile formFile)
		{
			long length = formFile.Length;

			if (length < 0)
				return []; // Something is wrong.

			byte[] bytes = new byte[length];
			using (Stream stream = formFile.OpenReadStream())
			{
				int _ = stream.Read(bytes, 0, (int)formFile.Length);
			}

			return bytes;
		}

		/// <summary>
		/// Convert the <see cref="byte"/> array into an <see cref="IFormFile"/> array.
		/// </summary>
		/// <param name="byteArray"><see cref="byte"/> array to convert into an <see cref="IFormFile"/>.</param>
		/// <param name="stream">Opened <see cref="MemoryStream"/>. Please close this stream manually.</param>
		/// <param name="name">Name</param>
		/// <param name="filename">Filename.</param>
		/// <param name="contentType">MIME type. Some common types can be found here: https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types.</param>
		/// <returns><paramref name="byteArray"/> as an <see cref="IFormFile"/>.</returns>
		/// <remarks>
		/// Caller is responsible for disposing the <paramref name="stream"/> parameter after the returned <see cref="IFormFile"/> is no longer needed.
		/// Failure to dispose the stream will result in a memory leak.
		/// </remarks>
		public static IFormFile ToIFormFile(this byte[] byteArray, out MemoryStream stream, string name = "", string filename = "", string contentType = "")
		{
			stream = new(byteArray); // DON'T dispose this stream because we return the FormFile at the end of this method.
			FormFile result = new(stream, 0, byteArray.Length, name, filename)
			{
				Headers = new HeaderDictionary(),
			};

			if (contentType != string.Empty)
				result.ContentType = contentType;

			return result;
		}
	}
}
