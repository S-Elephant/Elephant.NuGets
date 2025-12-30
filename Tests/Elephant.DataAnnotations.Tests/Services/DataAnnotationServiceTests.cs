using Elephant.DataAnnotations.Services;
// ReSharper disable UnusedMember.Local

namespace Elephant.DataAnnotations.Tests.Services
{
	/// <summary>
	/// <see cref="DataAnnotationService"/> tests.
	/// </summary>
	public class DataAnnotationServiceTests
	{
		[IfEmptyMakeItNull]
		private sealed class TestClassAnnotation
		{
			public string? String { get; set; } = string.Empty;

			public int? Int { get; set; } = 0;

			public int IntNonNullable { get; set; } = 0;

			public float? Float { get; set; } = null;
		}

		private sealed class TestWithAnnotations
		{
			[IfEmptyMakeItNull]
			public string? String { get; set; } = string.Empty;

			[IfEmptyMakeItNull]
			public int? Int { get; set; } = 0;

			public int? IntDontNull { get; set; } = 0;

			[IfEmptyMakeItNull]
			public float? Float { get; set; } = null;

			[IfEmptyMakeItNull]
			public char? Char { get; set; } = null;
		}

		[IfEmptyMakeItNull]
		private sealed class TestClassAnnotationWithValues
		{
			public string? String { get; set; } = "Foo";

			public int? Int { get; set; } = 2;

			[IfEmptyMakeItNull]
			public float? Float { get; set; } = -32.53f;

			public string? StringThatIsEmpty { get; set; } = string.Empty;
		}

		private readonly DataAnnotationService _systemUnderTest = new();

		/// <summary>
		/// Setup.
		/// </summary>
		public DataAnnotationServiceTests()
		{
		}

		/// <summary>
		/// Replace <see cref="string"/> with null succeeds.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReplaceStringWithNullWorks()
		{
			// Arrange.
			TestWithAnnotations dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			Assert.Null(dummy.String);
		}

		/// <summary>
		/// Replace <see cref="int"/> with null succeeds.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReplaceIntWithNullWorks()
		{
			// Arrange.
			TestWithAnnotations dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			Assert.Null(dummy.Int);
		}

		/// <summary>
		/// Replace <see cref="float"/> with null succeeds.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReplaceFloatWithNullWorks()
		{
			// Arrange.
			TestWithAnnotations dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			Assert.Null(dummy.Float);
		}

		/// <summary>
		/// Replace <see cref="char"/> with null succeeds.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReplaceCharWithNullWorks()
		{
			// Arrange.
			TestWithAnnotations dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			Assert.Null(dummy.Char);
		}

		/// <summary>
		/// Non-marked property should not be nulled.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void DoesntReplaceNonMarkedProperty()
		{
			// Arrange.
			TestWithAnnotations dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			_ = Assert.NotNull(dummy.IntDontNull);
		}

		/// <summary>
		/// Marked class must replace empty nullable properties.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MarkedClassReplacesNullables()
		{
			// Arrange.
			TestClassAnnotation dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			Assert.Null(dummy.Float);
		}

		/// <summary>
		/// Marked class must not replace empty non-nullable properties.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MarkedClassDoesntReplaceNonNullable()
		{
			// Arrange.
			TestClassAnnotation dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			Assert.Equal(0, dummy.IntNonNullable);
		}

		/// <summary>
		/// Marked class must not replace empty non-nullable properties.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AnnotationWithValuesRemainsUnchanged()
		{
			// Arrange.
			TestClassAnnotationWithValues dummy = new();

			// Act.
			_ = _systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			_ = Assert.NotNull(dummy.Float);
			_ = Assert.NotNull(dummy.Int);
			Assert.NotNull(dummy.String);
		}
	}
}