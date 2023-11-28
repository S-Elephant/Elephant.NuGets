using System;
using System.Collections.Generic;
using System.Linq;

namespace Elephant.UndoRedo
{
	/// <summary>
	/// Undo and redo.
	/// </summary>
	/// <typeparam name="TStateType">State type to be managed.</typeparam>
	public class UndoRedo<TStateType>
	{
		private readonly Stack<TStateType?> _undoStack = new();
		private readonly Stack<TStateType?> _redoStack = new();
		private TStateType? _currentState = default(TStateType);

		/// <summary>
		/// Stored undo count.
		/// </summary>
		public int UndoCount => _undoStack.Count;

		/// <summary>
		/// Stored redo count.
		/// </summary>
		public int RedoCount => _redoStack.Count;

		/// <summary>
		/// Returns the current state or null if there's no state.
		/// </summary>
		public TStateType? CurrentState => _currentState;

		/// <summary>
		/// Returns true if calling <see cref="Undo(bool)"/> is possible.
		/// </summary>
		public bool CanUndo => _undoStack.Count > 0;

		/// <summary>
		/// Returns true if calling <see cref="Redo"/> is possible.
		/// </summary>
		public bool CanRedo => _redoStack.Count > 0;

		/// <summary>
		/// Constructor.
		/// </summary>
		public UndoRedo()
		{
		}

		/// <summary>
		/// Constructor with initial state.
		/// </summary>
		public UndoRedo(TStateType initialState)
		{
			_currentState = initialState;
			_undoStack.Push(initialState);
		}

		/// <summary>
		/// Constructor with initial states.
		/// </summary>
		public UndoRedo(IEnumerable<TStateType> initialStates)
		{
			List<TStateType> initialStatesAsList = initialStates.ToList();
			if (!initialStatesAsList.Any())
				return;

			_currentState = initialStatesAsList[0];
			_undoStack.Push(initialStatesAsList[0]);

			for (int i = 1; i < initialStatesAsList.Count; i++)
				SaveState(initialStatesAsList[i]);
		}

		/// <summary>
		/// Saves the current state and updates the undo and redo stacks.
		/// </summary>
		/// <remarks>
		/// Pushes the current state onto the undo stack and updates the
		/// current state reference. It also clears the redo stack, as any new state
		/// invalidates the redo history.
		/// </remarks>
		/// <param name="newState">New state to save. This will be the new current state of the system.</param>
		public void SaveState(TStateType newState)
		{
			_undoStack.Push(_currentState); // Move current state onto the undo stack.
			_currentState = newState;
			_redoStack.Clear(); // Clear redo stack on new state.
		}

		/// <summary>
		/// Saves the current state and updates the undo and redo stacks.
		/// </summary>
		/// <remarks>
		/// Pushes the current state onto the undo stack and updates the
		/// current state reference. It also clears the redo stack, as any new state
		/// invalidates the redo history.
		/// </remarks>
		/// <param name="newStates">New states to save. The last <paramref name="newStates"/> value will be the new current state of the system.</param>
		public void SaveStates(IEnumerable<TStateType> newStates)
		{
			foreach (TStateType state in newStates)
				SaveState(state);
		}

		/// <summary>
		/// Reverts to the previous state.
		/// </summary>
		/// <param name="ignoreError">If true, will not throw an error if undo isn't possible.</param>
		/// <returns>The state after the undo operation.</returns>
		/// <remarks>
		/// This method pops the top state from the undo stack and pushes the current state onto the redo stack.
		/// It updates the current state to the state popped from the undo stack. If there is no state to undo,
		/// it throws an InvalidOperationException.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when there are no states to undo.</exception>
		public TStateType? Undo(bool ignoreError = false)
		{
			if (!CanUndo)
			{
				if (ignoreError)
					return _currentState;

				throw new InvalidOperationException("No states to undo.");
			}

			_redoStack.Push(_currentState);
			_currentState = _undoStack.Pop();
			return _currentState;
		}

		/// <summary>
		/// Reverts to the first state.
		/// </summary>
		/// <returns>The state after the undo operation.</returns>
		public TStateType? UndoAll()
		{
			int count = _undoStack.Count;
			for (int i = 0; i < count; i++)
				Undo();

			return CurrentState;
		}

		/// <summary>
		/// Returns true if calling <see cref="UndoXTimes"/> is possible with
		/// the specified <paramref name="undoAmount"/>.
		/// </summary>
		/// <param name="undoAmount">Amount of times to undo.</param>
		public bool CanUndoUsingAmount(int undoAmount)
		{
			if (undoAmount <= 0)
				return false;

			return _undoStack.Count >= undoAmount;
		}

		/// <summary>
		/// Returns true if calling <see cref="RedoXTimes"/> is possible with
		/// the specified <paramref name="redoAmount"/>.
		/// </summary>
		/// <param name="redoAmount">Amount of times to undo.</param>
		public bool CanRedoUsingAmount(int redoAmount)
		{
			if (redoAmount <= 0)
				return false;

			return _redoStack.Count >= redoAmount;
		}

		/// <summary>
		/// Reverts to the previous state.
		/// </summary>
		/// <param name="undoAmount">Amount of times to undo.</param>
		/// <param name="ignoreError">If true, will not throw an error if undo isn't possible and will then
		/// instead undo as much as possible.</param>
		/// <returns>The state after the undo operations.</returns>
		/// <remarks>
		/// This method pops the top state from the undo stack and pushes the current state onto the redo stack.
		/// It updates the current state to the state popped from the undo stack. If there is no state to undo,
		/// it throws an InvalidOperationException.
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when there are no states to undo.</exception>
		public TStateType? UndoXTimes(int undoAmount, bool ignoreError = false)
		{
			if (undoAmount <= 0)
				return _currentState;

			if (!CanUndoUsingAmount(undoAmount) && !ignoreError)
				throw new InvalidOperationException($"No or not enough states to undo {undoAmount} times.");

			for (int i = 0; i < undoAmount; i++)
				Undo(ignoreError);

			return _currentState;
		}

		/// <summary>
		/// Reapplies a state that was previously undone.
		/// </summary>
		/// <param name="ignoreError">If true, will not throw an error if undo isn't possible.</param>
		/// <remarks>
		/// This method pops the top state from the redo stack and pushes the current state onto the undo stack.
		/// It updates the current state to the state popped from the redo stack. If there is no state to redo,
		/// it throws an InvalidOperationException.
		/// </remarks>
		/// <returns>The state after the redo operation.</returns>
		/// <exception cref="InvalidOperationException">Thrown when there are no states to redo.</exception>
		public TStateType? Redo(bool ignoreError = false)
		{
			if (!CanRedo)
			{
				if (ignoreError)
					return _currentState;

				throw new InvalidOperationException("No states to redo.");
			}

			_undoStack.Push(_currentState);
			_currentState = _redoStack.Pop();
			return _currentState;
		}

		/// <summary>
		/// Reapplies all states.
		/// </summary>
		/// <returns>The state after the redo operations.</returns>
		public TStateType? RedoAll()
		{
			int count = _redoStack.Count;
			for (int i = 0; i < count; i++)
				Redo();

			return CurrentState;
		}

		/// <summary>
		/// Reapplies a state that was previously undone.
		/// </summary>
		/// <param name="redoAmount">Amount of times to redo.</param>
		/// <param name="ignoreError">If true, will not throw an error if undo isn't possible and will then
		/// instead redo as much as possible.</param>
		/// <remarks>
		/// This method pops the top state from the redo stack and pushes the current state onto the undo stack.
		/// It updates the current state to the state popped from the redo stack. If there is no state to redo,
		/// it throws an InvalidOperationException.
		/// </remarks>
		/// <returns>The state after the redo operations.</returns>
		/// <exception cref="InvalidOperationException">Thrown when there are no states to redo.</exception>
		public TStateType? RedoXTimes(int redoAmount, bool ignoreError = false)
		{
			if (redoAmount <= 0)
				return _currentState;

			if (!CanRedoUsingAmount(redoAmount) && !ignoreError)
				throw new InvalidOperationException($"No or not enough states to undo {redoAmount} times.");

			for (int i = 0; i < redoAmount; i++)
				Redo(ignoreError);

			return _currentState;
		}
	}
}
