using Elephant.ActivityTrackers.Tests.TestDataClasses;

namespace Elephant.ActivityTrackers.Tests
{
	/// <summary>
	/// <see cref="ActivityTracker"/> tests.
	/// </summary>
	public class ActivityTrackerTests
	{
		/// <summary>
		/// Empty tests.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public void EmptyTests()
		{
			// Arrange.
			ActivityTracker systemUnderTest = new();

			// Act.
			systemUnderTest.Add("a");
			systemUnderTest.Remove("a");
			bool hasAny = systemUnderTest.HasAny;
			bool isEmpty = systemUnderTest.IsEmpty;
			int totalActivityCount = systemUnderTest.TotalActivityCount;

			// Assert.
			Assert.True(isEmpty);
			Assert.False(hasAny);
			Assert.Equal(0, systemUnderTest.ActivityCount("a"));
			Assert.Equal(0, totalActivityCount);
		}

		/// <summary>
		/// After adding one activity it should contain one activity.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AddingSingleActivityReturns1Activity()
		{
			// Arrange.
			ActivityTracker systemUnderTest = new();

			// Act.
			systemUnderTest.Add("a");

			// Assert.
			Assert.Equal(1, systemUnderTest.ActivityCount("a"));
		}

		/// <summary>
		/// After adding one activity (with a string key), check if IsBeingProcessed
		/// returns as expected.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("", "", true)]
		[InlineData("", "_", false)]
		[InlineData("a", "a", true)]
		[InlineData("a", "b", false)]
		public void BasicStringKeyActivityInfoClassIsBeingProcessedTests(string activityKey, string activityKeyToCheck, bool expectedIsBeingProcessed)
		{
			// Arrange.
			ActivityTracker systemUnderTest = new();

			// Act.
			systemUnderTest.Add(activityKey);

			// Assert.
			Assert.Equal(expectedIsBeingProcessed, systemUnderTest.IsBeingProcessed(activityKeyToCheck));
		}

		/// <summary>
		/// After adding two activities, then removing one activity, then adding one
		/// activity, it should contain 2 of such activities, not including other activities.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AddingThreeActivitiesMinusOneReturnsTwo()
		{
			// Arrange.
			ActivityTracker systemUnderTest = new();

			// Act.
			systemUnderTest.Add("a");
			systemUnderTest.Add("a");
			systemUnderTest.Add("b");
			systemUnderTest.Remove("a");
			systemUnderTest.Add("a");

			// Assert.
			Assert.Equal(2, systemUnderTest.ActivityCount("a"));
		}

		/// <summary>
		/// After adding 2 activities (both with 2 entries) it should contain 2 counts for
		/// each entry.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void AddSimpleStructTest()
		{
			// Arrange.
			ActivityTrackerGeneric<string, ActivityInfoStruct> systemUnderTest = new();

			// Act.
			systemUnderTest.Add("a", new ActivityInfoStruct("Struct A", 10));
			systemUnderTest.Add("b", new ActivityInfoStruct("Struct B1", 15));
			systemUnderTest.Add("b", new ActivityInfoStruct("Struct B2", 16));
			systemUnderTest.Add("a", new ActivityInfoStruct("Struct C", 20));

			// Assert.
			Assert.Equal(2, systemUnderTest.ActivityCount("a"));
			Assert.Equal(2, systemUnderTest.ActivityCount("b"));
		}

		/// <summary>
		/// Test adding and retrieval and processing of structs.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public void ComplexStructTest()
		{
			// Arrange.
			ActivityTrackerGeneric<string, ActivityInfoStruct> systemUnderTest = new();

			// Act.
			systemUnderTest.Add("a", new ActivityInfoStruct("Struct A", 10));
			systemUnderTest.Add("b", new ActivityInfoStruct("Struct B1", 15));
			systemUnderTest.Add("b", new ActivityInfoStruct("Struct B2", 16));
			systemUnderTest.Add("a", new ActivityInfoStruct("Struct C", 20));
			List<ActivityInfoStruct> resultA = systemUnderTest.GetData("a");
			List<ActivityInfoStruct> resultB = systemUnderTest.GetData("b");
			List<ActivityInfoStruct> resultC = systemUnderTest.GetData("c");

			// Assert.
			Assert.Equal("Struct A", resultA[0].ValueString);

			Assert.Equal(2, resultB.Count);
			Assert.Equal("Struct B1", resultB[0].ValueString);
			Assert.Equal(15, resultB[0].ValueInt);

			Assert.Equal("Struct B2", resultB[1].ValueString);
			Assert.Equal(16, resultB[1].ValueInt);

			Assert.Empty(resultC);

			Assert.False(systemUnderTest.IsBeingProcessed(string.Empty));
			Assert.True(systemUnderTest.IsBeingProcessed("a"));
			Assert.True(systemUnderTest.IsBeingProcessed("b"));
			Assert.False(systemUnderTest.IsBeingProcessed("c"));
			Assert.False(systemUnderTest.IsBeingProcessed("d"));
		}

		/// <summary>
		/// Test adding and retrieval and processing of classes.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public void ComplexClassTest()
		{
			// Arrange.
			ActivityTrackerGeneric<ActivityKey, ActivityInfo> systemUnderTest = new();
			ActivityKey keyA = new(Constants.MockData.Guids.Guids10[0]);
			ActivityKey keyB = new(Constants.MockData.Guids.Guids10[1]);
			ActivityKey keyC = new();

			// Act.
			systemUnderTest.Add(keyA, new ActivityInfo("Class A", 10));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B1", 15));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B2", 16));
			systemUnderTest.Add(keyA, new ActivityInfo("Class C", 20));
			List<ActivityInfo> result1 = systemUnderTest.GetData(keyA);
			List<ActivityInfo> result2 = systemUnderTest.GetData(keyB);
			List<ActivityInfo> result3 = systemUnderTest.GetData(keyC);

			// Assert.
			Assert.Equal("Class A", result1[0].ValueString);

			Assert.Equal(2, result2.Count);
			Assert.Equal("Class B1", result2[0].ValueString);
			Assert.Equal(15, result2[0].ValueInt);

			Assert.Equal("Class B2", result2[1].ValueString);
			Assert.Equal(16, result2[1].ValueInt);

			Assert.Empty(result3);
		}

		/// <summary>
		/// RemoveAll test.
		/// should remove all and also tests HasAny and IsEmpty
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RemoveAllShouldRemovesAll()
		{
			// Arrange.
			ActivityTrackerGeneric<ActivityKey, ActivityInfo> systemUnderTest = new();
			ActivityKey keyA = new(Constants.MockData.Guids.Guids10[0]);
			ActivityKey keyB = new(Constants.MockData.Guids.Guids10[1]);
			systemUnderTest.Add(keyA, new ActivityInfo("Class A", 10));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B1", 15));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B2", 16));
			systemUnderTest.Add(keyA, new ActivityInfo("Class Foo", 20));

			// Act.
			systemUnderTest.RemoveAll();

			// Assert.
			Assert.True(systemUnderTest.IsEmpty);
			Assert.False(systemUnderTest.HasAny);
		}

		/// <summary>
		/// HasAny should return true if any activity is present.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void HasAnyReturnsTrueIfAnyActivityIsPresent()
		{
			// Arrange.
			ActivityTrackerGeneric<ActivityKey, ActivityInfo> systemUnderTest = new();
			ActivityKey keyA = new(Constants.MockData.Guids.Guids10[0]);
			ActivityKey keyB = new(Constants.MockData.Guids.Guids10[1]);
			systemUnderTest.Add(keyA, new ActivityInfo("Class A", 10));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B1", 15));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B2", 16));
			systemUnderTest.Add(keyA, new ActivityInfo("Class Foo", 20));

			// Act.
			bool result = systemUnderTest.HasAny;

			// Assert.
			Assert.True(result);
		}

		/// <summary>
		/// IsEmpty should return true if any activity is present.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsEmptyReturnsFalseIfAnyActivityIsPresent()
		{
			// Arrange.
			ActivityTrackerGeneric<ActivityKey, ActivityInfo> systemUnderTest = new();
			ActivityKey keyA = new(Constants.MockData.Guids.Guids10[0]);
			ActivityKey keyB = new(Constants.MockData.Guids.Guids10[1]);
			systemUnderTest.Add(keyA, new ActivityInfo("Class A", 10));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B1", 15));
			systemUnderTest.Add(keyB, new ActivityInfo("Class B2", 16));
			systemUnderTest.Add(keyA, new ActivityInfo("Class Foo", 20));

			// Act.
			bool result = systemUnderTest.IsEmpty;

			// Assert.
			Assert.False(result);
		}
	}
}