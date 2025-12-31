using Microsoft.AspNetCore.Http;

namespace Elephant.ApiControllers.Tests
{
	/// <summary>
	/// Byte array and IForm tests.
	/// </summary>
	public class ByteArrayAndIFormFileTests
	{
		/// <summary>
		/// <see cref="ControllerExtensions.ToIFormFile(byte[], out MemoryStream, string, string, string)"/> and <see cref="ControllerExtensions.ToByteArray(IFormFile)"/> tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void FromBytesToFormFileAndBack()
		{
			// Arrange.
			byte[] bytes = [0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E,];
			MemoryStream memoryStream = new(bytes);
			try
			{
				// Act.
				// Two act lines because I don't want to manually construct an IFormFile.
				IFormFile formFile = bytes.ToIFormFile(out memoryStream);
				byte[] convertedBytes = formFile.ToByteArray();

				// Assert.
				Assert.Equal(bytes, convertedBytes); // We expect the start and end result to be the same.
			}
			finally
			{
				memoryStream.Dispose();
			}
		}

		/// <summary>
		/// <see cref="ControllerExtensions.ToIFormFile(byte[], out MemoryStream, string, string, string)"/> and <see cref="ControllerExtensions.ToByteArray(IFormFile)"/> conversions test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void FromEmptyBytesToFormFileAndBack()
		{
			// Arrange.
			byte[] bytes = [];

			MemoryStream memoryStream = new(bytes);
			try
			{
				// Act.
				// Two act lines because I don't want to manually construct an IFormFile.
				IFormFile formFile = bytes.ToIFormFile(out memoryStream);
				byte[] convertedBytes = formFile.ToByteArray();

				// Assert.
				Assert.Equal(bytes, convertedBytes); // We expect the start and end result to be the same.
			}
			finally
			{
				memoryStream.Dispose();
			}
		}
	}
}