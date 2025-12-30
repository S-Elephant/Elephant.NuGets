using System.Threading;

namespace Elephant.Types
{
	/// <summary>
	/// <see cref="CancellationTokenSource"/> wrapper to prevent exceptions.
	/// </summary>
	public class ElephantCancellationTokenSource : CancellationTokenSource
	{
		/// <summary>
		/// Indicates whether Dispose has been called. 0 = not disposed, 1 = disposed.
		/// </summary>
		private int _disposed = 0;

		/// <summary>
		/// Returns true if this token was disposed.
		/// </summary>
		public bool IsDisposed => _disposed == 1;

		/// <summary>
		/// Returns true if this token is NOT yet disposed.
		/// </summary>
		public bool IsNotDisposed => !IsDisposed;

		/// <summary>
		/// Returns true if either this token was disposed or if cancellation was requested.
		/// </summary>
		public bool IsDisposedOrCancellationRequested => IsDisposed || IsCancellationRequested;

		/// <summary>
		/// Returns true if this token is not yet disposed and if no cancellation was requested.
		/// </summary>
		public bool IsNotDisposedAndNotCancellationRequested => !IsDisposed && !IsCancellationRequested;

		/// <inheritdoc cref="CancellationTokenSource.Dispose(bool)"/>
		protected override void Dispose(bool disposing)
		{
			if (Interlocked.Exchange(ref _disposed, 1) == 0)
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>
		/// Cancel and dispose. Will do nothing if it was already disposed.
		/// Will not cancel if cancellation was already requested.
		/// Is thread-safe.
		/// </summary>
		public void CancelAndDispose()
		{
			if (Interlocked.Exchange(ref _disposed, 1) == 0)
			{
				try
				{
					if (!IsCancellationRequested)
						Cancel();
				}
				catch (System.ObjectDisposedException)
				{
					// Ignore because that means this is already disposed.
				}

				base.Dispose(true); // Call base directly since we already set _disposed = 1.
			}
		}
	}
}
