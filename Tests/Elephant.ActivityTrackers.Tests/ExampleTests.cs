namespace Elephant.ActivityTrackers.Tests
{
	/// <summary>
	/// Example tests.
	/// </summary>
	public class ExampleTests
	{
		/// <summary>
		/// Example.
		/// </summary>
		internal class ExampleClass
		{
			private readonly ActivityTracker _activityTracker = new();

			/// <summary>
			/// Example.
			/// </summary>
			internal void Foo()
			{
				_activityTracker.Add("processing-image");
				_activityTracker.Remove("processing-image");
				_activityTracker.Add("saving-image");
				_activityTracker.Add("send-image-to-backend");
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
			ExampleClass example = new();

			// Act.
			Exception? exception = Record.Exception(() => example.Foo());
			Exception? exception2 = Record.Exception(() => example.UpdateGui());

			// Assert.
			Assert.Null(exception);
			Assert.Null(exception2);
		}
	}
}
