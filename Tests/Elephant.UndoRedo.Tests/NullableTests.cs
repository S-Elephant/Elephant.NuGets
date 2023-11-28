using Elephant.Testing.Xunit;

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
		[SpeedVeryFast]
		public void CurrentStateReturnsExpected()
		{
			// Arrange.
			DataTest? LastItem = new(100);
			UndoRedo<DataTest?> undoRedo = new(new List<DataTest?> { new(1), new(3), new(5), new(6), new(9), LastItem });

			DataTest? result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(LastItem, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}"/> reference type null test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
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
		[SpeedVeryFast]
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
		[SpeedVeryFast]
		public void CurrentReferenceTypeStateReturnsCorrect()
		{
			// Arrange.
			DataTest? LastItem = new(-100);
			UndoRedo<DataTest?> undoRedo = new(new List<DataTest?> { null, LastItem });

			DataTest? result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(LastItem, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}"/> value type nullable test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
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