// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Elephant.DataAnnotations.Tests
{
	/// <summary>
	/// <see cref="GuidNotEmptyAttribute"/> tests.
	/// </summary>
	public class GuidNotEmptyTests
	{
		/// <summary>
		/// <see cref="GuidNotEmptyAttribute"/> returns valid if both GUID's are valid.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ValidGuidsReturnValid()
		{
			// Arrange.
			ValidationTarget target = new();

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GuidNotEmptyAttribute"/> returns valid if one GUID is valid and the other one is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ValidGuidAndNullReturnValid()
		{
			// Arrange.
			ValidationTarget target = new(guidNullableStr: null);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GuidNotEmptyAttribute"/> returns valid if one GUID is valid and the other one is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyGuidsReturnsInvalid()
		{
			// Arrange.
			ValidationTarget target = new(Guid.Empty.ToString(), Guid.Empty.ToString());

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// <see cref="GuidNotEmptyAttribute"/> returns valid if one GUID is valid and the other one is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void EmptyGuidReturnsInvalid()
		{
			// Arrange.
			ValidationTarget target = new(guidNullableStr: Guid.Empty.ToString());

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// <see cref="GuidNotEmptyAttribute"/> returns valid if one GUID is valid and the other one is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullGuidReturnsInvalid()
		{
			// Arrange.
			NoNullAllowedValidationTarget target = new(guidNullableStr: null);

			// Act.
			bool isValid = Validator.TryValidateObject(target, new ValidationContext(target), new List<ValidationResult>(), true);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private sealed class ValidationTarget
		{
			/// <summary>
			/// Items to validate.
			/// </summary>
			[GuidNotEmpty]
			public Guid Guid { get; set; }

			[GuidNotEmpty]
			public Guid? GuidNullable { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public ValidationTarget(string guidStr = "526b8186-1378-4409-8d99-b2e2dc54e5a3", string? guidNullableStr = "d8f1dfca-e45a-49e9-a1e9-70abb4459632")
			{
				Guid = Guid.Parse(guidStr);
				GuidNullable = guidNullableStr == null ? null : Guid.Parse(guidNullableStr);
			}
		}

		/// <summary>
		/// Test class.
		/// </summary>
		private sealed class NoNullAllowedValidationTarget
		{
			[GuidNotEmpty(false)]
			public Guid? GuidNullable { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public NoNullAllowedValidationTarget(string? guidNullableStr = "d8f1dfca-e45a-49e9-a1e9-70abb4459632")
			{
				GuidNullable = guidNullableStr == null ? null : Guid.Parse(guidNullableStr);
			}
		}
	}
}