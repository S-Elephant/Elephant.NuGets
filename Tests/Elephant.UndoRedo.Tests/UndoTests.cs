using Elephant.Testing.Xunit;

namespace Elephant.UndoRedo.Tests
{
	/// <summary>
	/// <see cref="UndoRedo{TStateType}"/> undo tests.
	/// </summary>
	public class UndoTests
	{
		/// <summary>
		/// <see cref="UndoRedo{TStateType}.CanUndo"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void UndoReturns7()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });
			undoRedo.Undo();

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(7, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.UndoXTimes"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0, 10)]
		[InlineData(1, 7)]
		[InlineData(2, 5)]
		[InlineData(3, 2)]
		[InlineData(4, 1)]
		public void UndoXTimesReturnsExpected(int undoAmount, int expected)
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });
			undoRedo.UndoXTimes(undoAmount);

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.UndoXTimes"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0, 10)]
		[InlineData(1, 7)]
		[InlineData(2, 5)]
		[InlineData(3, 2)]
		public void UndoXTimesWithoutInitialReturnsExpected(int undoAmount, int expected)
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 2, 5, 7, 10 });
			undoRedo.UndoXTimes(undoAmount);

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.UndoXTimes"/> with redo tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast]
		[InlineData(0, 10)]
		[InlineData(1, 7)]
		[InlineData(2, 5)]
		[InlineData(3, 2)]
		[InlineData(4, 1)]
		public void UndoXTimesWithRedoReturnsExpected(int undoAmount, int expected)
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });
			undoRedo.Undo();
			undoRedo.Redo();
			undoRedo.UndoXTimes(undoAmount);

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.UndoXTimes"/> with ignoring errors will undo
		/// until the beginning, if possible.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void UndoMoreThanPossibleIgnoringErrorsReturnsFirstState()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });
			undoRedo.UndoXTimes(9, true);

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(1, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.UndoAll"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void UndoUntilFirstReturnsFirstState()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { -100, 1, 2, 5, 7, 10 });

			// Act.
			int result = undoRedo.UndoAll();

			// Assert.
			Assert.Equal(-100, result);
		}
	}
}