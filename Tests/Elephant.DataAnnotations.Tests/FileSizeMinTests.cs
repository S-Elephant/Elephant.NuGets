using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Elephant.DataAnnotations.Tests
{
	/// <summary>
	/// <see cref="FileSizeMinAttribute"/> tests.
	/// </summary>
	public class FileSizeMinTests
	{
		/// <summary>
		/// Create an <see cref="IFormFile"/> mock.
		/// </summary>
		/// <param name="content">String contents to write.</param>
		/// <returns>Mocked <see cref="IFormFile"/> with the specified contents.</returns>
		private static FormFile CreateIFormFileMock(string content)
		{
			using MemoryStream stream = new();
			using StreamWriter writer = new(stream);
			writer.Write(content);
			writer.Flush();
			stream.Position = 0;
			return new FormFile(stream, 0, stream.Length, "name", "Mock.txt");
		}

		/// <summary>
		/// <see cref="FileSizeMinAttribute"/> non-required test without data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ShouldReturnSuccessIfDataIsNull()
		{
			// Arrange.
			ValidationTarget target = new(null);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="FileSizeMinAttribute"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(null, false)]
		[InlineData("", false)]
		[InlineData("A", false)]
		[InlineData("Some random string in here as the data.", true)]
		public void Validate(string? content, bool expectedIsValid)
		{
			// Arrange.
			ValidationTarget target = new(CreateIFormFileMock(content!));

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.Equal(expectedIsValid, isValid);
		}

		/// <summary>
		/// Test class for <see cref="Validate(string, bool)"/> tests.
		/// </summary>
		private sealed class ValidationTarget
		{
			/// <summary>
			/// <see cref="IFormFile"/> to validate.
			/// </summary>
			[FileSizeMin(5)]
			public IFormFile? FormFile { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTarget(IFormFile? formFile)
			{
				FormFile = formFile;
			}
		}

		/// <summary>
		/// <see cref="FileSizeMinAttribute"/> should be invalid if the base type is wrong.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsInvalidIfWrongType()
		{
			// Arrange.
			WrongType target = new();

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// Class that is always the wrong type for validation testing.
		/// </summary>
		private sealed class WrongType
		{
			/// <summary>
			/// Item to validate.
			/// </summary>
			[GreaterThanZeroRequired]
			// ReSharper disable once UnusedMember.Local
			public AlwaysWrong Item { get; set; } = new();
		}

		/// <summary>
		/// Class that is always the wrong type for validation testing.
		/// Used as a property in <see cref="WrongType"/>.
		/// </summary>
		private sealed class AlwaysWrong
		{
		}
	}
}