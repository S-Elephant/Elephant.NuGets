namespace Elephant.ActivityTrackers.Tests
{
	/// <summary>
	/// Generic example tests.
	/// </summary>
	public class ExampleGenericTests
	{
		/// <summary>
		/// Example custom data (can also be a struct).
		/// </summary>
		internal sealed class ActivityInfo
		{
			/// <summary>
			/// Example data.
			/// </summary>
			public string ExampleData { get; set; } = string.Empty;

			/// <summary>
			/// Constructor.
			/// </summary>
			public ActivityInfo()
			{
			}

			/// <summary>
			/// Constructor with initializer.
			/// </summary>
			public ActivityInfo(string exampleData)
			{
				ExampleData = exampleData;
			}

			/// <inheritdoc/>
			public override bool Equals(object? obj)
			{
				if (obj == null || GetType() != obj.GetType())
					return false;

				ActivityInfo other = (ActivityInfo)obj;
				return ExampleData == other.ExampleData;
			}

			/// <inheritdoc/>
			public override int GetHashCode()
			{
				return ExampleData.GetHashCode();
			}
		}

		/// <summary>
		/// Example.
		/// </summary>
		internal sealed class ExampleGenericClass
		{
			// In this example we use a string key to distinguish between different activities.
			private readonly ActivityTrackerGeneric<string, ActivityInfo> _activityTracker = new();

			/// <summary>
			/// Example.
			/// </summary>
			internal void Foo()
			{
				ActivityInfo processingImage1 = new("My custom data here.");
				ActivityInfo processingImage2 = new("My custom data here2.");
				ActivityInfo processingImage3 = new("My custom data here3.");

				_activityTracker.Add("processing-image", processingImage1);
				_activityTracker.Add("processing-image", processingImage2);

				// Will remove 1 "processing-image" key with the ActivityInfo that contains string "My custom data here" only.
				_ = _activityTracker.Remove("processing-image", processingImage1);

				// Will remove nothing because the combination of key and value doesn't exist.
				_ = _activityTracker.Remove("processing-image", processingImage3);

				// Will remove one entry from key "processing-image", regardless of it's value.
				_activityTracker.Remove("processing-image");
			}

			/// <summary>
			/// Example.
			/// </summary>
			internal void UpdateGui()
			{
				if (_activityTracker.IsBeingProcessed("saving-image"))
				{
					// Enable some UI elements or such.
				}
			}
		}

		/// <summary>
		/// Example test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public void ExampleTestWorks()
		{
			// Arrange.
			ExampleGenericClass example = new();

			// Act.
			Exception? exception = Record.Exception(example.Foo);
			Exception? exception2 = Record.Exception(example.UpdateGui);

			// Assert.
			Assert.Null(exception);
			Assert.Null(exception2);
		}
	}
}
