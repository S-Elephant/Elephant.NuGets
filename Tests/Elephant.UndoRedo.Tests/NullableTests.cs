namespace Elephant.UndoRedo.Tests
{
	/// <summary>
	/// Basic <see cref="UndoRedo{TStateType}"/> tests using nullables.
	/// </summary>
	public class NullableTests
	{
		/// <summary>
		/// Data test class.
		/// </summary>
		private class DataTest
		{
			/// <summary>
			/// Test value.
			/// </summary>
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			private int Value { get; set; }

			/// <summary>
			/// Constructor.
			/// </summary>
			public DataTest(int value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}"/> nullable class test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CurrentStateReturnsExpected()
		{
			// Arrange.
			DataTest lastItem = new(100);
			UndoRedo<DataTest?> undoRedo = new(new List<DataTest?> { new(1), new(3), new(5), new(6), new(9), lastItem });

			DataTest? result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(lastItem, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}"/> reference type null test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CurrentReferenceTypeStateReturnsNull()
		{
			// Arrange.
			UndoRedo<DataTest?> undoRedo = new(new List<DataTest?> { null });

			DataTest? result = undoRedo.CurrentState;

			// Assert.
			Assert.Null(result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}"/> value type null test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CurrentValueTypeStateReturnsNull()
		{
			// Arrange.
			UndoRedo<bool?> undoRedo = new(new List<bool?> { null });

			bool? result = undoRedo.CurrentState;

			// Assert.
			Assert.Null(result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}"/> reference type nullable test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CurrentReferenceTypeStateReturnsCorrect()
		{
			// Arrange.
			DataTest lastItem = new(-100);
			UndoRedo<DataTest?> undoRedo = new(new List<DataTest?> { null, lastItem });

			DataTest? result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(lastItem, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}"/> value type nullable test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CurrentValueTypeStateReturns10()
		{
			// Arrange.
			UndoRedo<bool?> undoRedo = new(new List<bool?> { null, true });

			bool? result = undoRedo.CurrentState;

			// Assert.
			Assert.True(result);
		}
	}
}