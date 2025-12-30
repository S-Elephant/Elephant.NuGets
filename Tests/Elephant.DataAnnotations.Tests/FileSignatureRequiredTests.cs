using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Elephant.DataAnnotations.Tests
{
	/// <summary>
	/// <see cref="FileSignatureRequiredAttribute"/> tests.
	/// </summary>
	public class FileSignatureRequiredTests
	{
		/// <summary>
		/// Test class for <see cref="ShouldReturnFalseIfDataIsNull()"/> tests with .txt and .png extensions.
		/// </summary>
		private sealed class ValidationTargetTxtAndPng
		{
			/// <summary>
			/// <see cref="IFormFile"/> to validate.
			/// </summary>
			[FileSignature(AllowedFileExtensionType.Txt, AllowedFileExtensionType.Png)]
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public FormFile? FormFile { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTargetTxtAndPng(FormFile? formFile)
			{
				FormFile = formFile;
			}
		}

		/// <summary>
		/// <see cref="FileSignatureAttribute"/> non-required test without data.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ShouldReturnFalseIfDataIsNull()
		{
			// Arrange.
			ValidationTargetTxtAndPng target = new(null);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}
	}
}