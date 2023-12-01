namespace Elephant.UndoRedo.Tests
{
	/// <summary>
	/// <see cref="UndoRedo{TStateType}"/> redo tests.
	/// </summary>
	public class RedoTests
	{
		/// <summary>
		/// <see cref="UndoRedo{TStateType}.CanRedo"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RedoReturns7()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });
			undoRedo.UndoXTimes(2);
			undoRedo.Redo();

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(7, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.CanRedo"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void RedoReturnsNegative10()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, -10 });
			undoRedo.UndoXTimes(4);
			undoRedo.RedoXTimes(4);

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(-10, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.RedoXTimes"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(1, 2)]
		[InlineData(2, 5)]
		[InlineData(3, 7)]
		[InlineData(4, 10)]
		public void RedoXTimesReturnsExpected(int redoAmount, int expected)
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });
			undoRedo.UndoXTimes(4);
			undoRedo.RedoXTimes(redoAmount);

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(expected, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.RedoXTimes"/> with ignoring errors will redo
		/// until the end, if possible.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void UndoMoreThanPossibleIgnoringErrorsReturnsFirstState()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });
			undoRedo.UndoXTimes(4);
			undoRedo.RedoXTimes(9, true);

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(10, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.RedoAll"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void UndoUntilFirstReturnsFirstState()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { -100, 1, 2, 5, 7, 10 });

			// Act.
			int result = undoRedo.RedoAll();

			// Assert.
			Assert.Equal(10, result);
		}
	}
}