using Elephant.DataAnnotations.Services;
using Elephant.DataAnnotations.Services.Interfaces;

namespace Elephant.DataAnnotations.Tests
{
	/// <summary>
	/// <see cref="DataAnnotationService"/> tests.
	/// </summary>
	public class DataAnnotationServiceTests
	{
		[IfEmptyMakeItNull]
		private class TestClassAnnotation
		{
			public string? String { get; set; } = string.Empty;

			public int? Int { get; set; } = 0;

			public int IntNonNullable { get; set; } = 0;

			public float? Float { get; set; } = null;
		}

		private class TestWithAnnotations
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
		private class TestClassAnnotationWithValues
		{
			public string? String { get; set; } = "Foo";

			public int? Int { get; set; } = 2;

			[IfEmptyMakeItNull]
			public float? Float { get; set; } = -32.53f;

			public string? StringThatIsEmpty { get; set; } = string.Empty;
		}

		private readonly IDataAnnotationService _systemUnderTest = new DataAnnotationService();

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
			object dummy = new TestWithAnnotations();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			TestWithAnnotations dummyCasted = (TestWithAnnotations)dummy;
			Assert.Null(dummyCasted.String);
		}

		/// <summary>
		/// Replace <see cref="int"/> with null succeeds.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReplaceIntWithNullWorks()
		{
			// Arrange.
			object dummy = new TestWithAnnotations();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			TestWithAnnotations dummyCasted = (TestWithAnnotations)dummy;
			Assert.Null(dummyCasted.Int);
		}

		/// <summary>
		/// Replace <see cref="float"/> with null succeeds.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReplaceFloatWithNullWorks()
		{
			// Arrange.
			object dummy = new TestWithAnnotations();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			TestWithAnnotations dummyCasted = (TestWithAnnotations)dummy;
			Assert.Null(dummyCasted.Float);
		}

		/// <summary>
		/// Replace <see cref="char"/> with null succeeds.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReplaceCharWithNullWorks()
		{
			// Arrange.
			object dummy = new TestWithAnnotations();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			TestWithAnnotations dummyCasted = (TestWithAnnotations)dummy;
			Assert.Null(dummyCasted.Char);
		}

		/// <summary>
		/// Non-marked property should not be nulled.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void DoesntReplaceNonMarkedProperty()
		{
			// Arrange.
			object dummy = new TestWithAnnotations();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			TestWithAnnotations dummyCasted = (TestWithAnnotations)dummy;
			Assert.NotNull(dummyCasted.IntDontNull);
		}

		/// <summary>
		/// Marked class must replace empty nullable properties.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MarkedClassReplacesNullables()
		{
			// Arrange.
			object dummy = new TestClassAnnotation();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			TestClassAnnotation dummyCasted = (TestClassAnnotation)dummy;
			Assert.Null(dummyCasted.Float);
		}

		/// <summary>
		/// Marked class must not replace empty non-nullable properties.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void MarkedClassDoesntReplaceNonNullable()
		{
			// Arrange.
			object dummy = new TestClassAnnotation();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			// Assert.
			Assert.Equal(0, ((TestClassAnnotation)dummy).IntNonNullable);
		}

		/// <summary>
		/// Marked class must not replace empty non-nullable properties.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AnnotationWithValuesRemainsUnchanged()
		{
			// Arrange.
			object dummy = new TestClassAnnotationWithValues();

			// Act.
			_systemUnderTest.ReplaceEmptyStringsWithNulls(ref dummy);

			TestClassAnnotationWithValues dummyCasted = (TestClassAnnotationWithValues)dummy;

			// Assert.
			Assert.NotNull(dummyCasted.Float);
			Assert.NotNull(dummyCasted.Int);
			Assert.NotNull(dummyCasted.String);
		}
	}
}