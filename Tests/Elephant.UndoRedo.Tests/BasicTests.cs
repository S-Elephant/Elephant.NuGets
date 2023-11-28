using Elephant.Testing.Xunit;

namespace Elephant.UndoRedo.Tests
{
	/// <summary>
	/// Basic <see cref="UndoRedo{TStateType}"/> tests.
	/// </summary>
	public class BasicTests
	{
		/// <summary>
		/// <see cref="UndoRedo{TStateType}.UndoCount"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void NewUndoRedoHasZeroUndoCount()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new();

			// Assert.
			Assert.Equal(0, undoRedo.UndoCount);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.RedoCount"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void NewUndoRedoHasZeroRedoCount()
		{
			// Arrange.
			UndoRedo<int> undoRedo = new();

			// Assert.
			Assert.Equal(0, undoRedo.RedoCount);
		}

		/// <summary>
		/// A new <see cref="UndoRedo{TStateType}"/> should return
		/// false for <see cref="UndoRedo{TStateType}.CanUndo"/>.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void NewUndoRedoCantUndo()
		{
			// Arrange.
			UndoRedo<string> undoRedo = new();

			// Act.
			bool result = undoRedo.CanUndo;

			// Assert.
			Assert.False(result);
		}

		/// <summary>
		/// A new <see cref="UndoRedo{TStateType}"/> should return
		/// false for <see cref="UndoRedo{TStateType}.CanRedo"/>.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void NewUndoRedoCantRedo()
		{
			// Arrange.
			UndoRedo<string> undoRedo = new();

			// Act.
			bool result = undoRedo.CanRedo;

			// Assert.
			Assert.False(result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.Undo"/> throws if can't undo.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void UndoThrowsIfCantUndo()
		{
			// Arrange.
			UndoRedo<string> undoRedo = new();

			// Act and Assert.
			Assert.Throws<InvalidOperationException>(() => undoRedo.Undo());
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.Redo"/> throws if can't undo.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void RedoThrowsIfCantRedo()
		{
			// Arrange.
			UndoRedo<string> undoRedo = new();

			// Act and Assert.
			Assert.Throws<InvalidOperationException>(() => undoRedo.Redo());
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.Undo"/> returns null if can't undo when
		/// the error is suppressed.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void UndoReturnsNullIfCantUndoAndSuppressed()
		{
			// Arrange.
			UndoRedo<string> undoRedo = new();

			// Act.
			string? result = undoRedo.Undo(true);

			// Assert.
			Assert.Null(result);
		}

		/// <summary>
		/// <see cref="UndoRedo{TStateType}.Redo"/> returns null if can't redo when
		/// the error is suppressed.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void RedoReturnsNullIfCantRedoAndSuppressed()
		{
			// Arrange.
			UndoRedo<string> undoRedo = new();

			// Act.
			string? result = undoRedo.Redo(true);

			// Assert.
			Assert.Null(result);
		}
	}
}