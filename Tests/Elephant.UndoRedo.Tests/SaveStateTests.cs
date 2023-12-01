namespace Elephant.UndoRedo.Tests
{
	/// <summary>
	/// Basic <see cref="UndoRedo{TStateType}.SaveState"/> and <see cref="UndoRedo{TStateType}.SaveStates"/> tests.
	/// </summary>
	public class SaveStateTests
	{
		/// <summary>
		/// <see cref="UndoRedo{TStateType}.SaveState"/> returns expected.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SaveStateReturnsExpected()
		{
			// Arrange.
			UndoRedo<string> undoRedo = new();
			const string newState = "Pikachu state!";

			// Act.
			undoRedo.SaveState(newState);

			// Assert.
			Assert.Equal(newState, undoRedo.CurrentState);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.SaveState"/> should return last saved state.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SaveStateReturnsLastState()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(new List<int> { 1, 2, 5, 7, 10 });

			// Act.
			int result = undoRedo.CurrentState;

			// Assert.
			Assert.Equal(10, result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.SaveState"/> should return last saved state.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SaveStatesSavesCorrectAmount()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new(1);

			// Act.
			undoRedo.SaveStates(new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10 });

			// Assert.
			Assert.Equal(10, undoRedo.UndoCount);
		}
	}
}
