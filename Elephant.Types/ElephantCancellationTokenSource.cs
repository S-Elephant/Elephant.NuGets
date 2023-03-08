using System.Threading;

namespace Elephant.Types
{
	/// <summary>
	/// <see cref="CancellationTokenSource"/> wrapper to prevent exceptions.
	/// </summary>
	public class ElephantCancellationTokenSource : CancellationTokenSource
	{
		/// <summary>
		/// Returns true if this token was disposed.
		/// </summary>
		public bool IsDisposed { get; private set; } = false;

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

		/// <inheritdoc cref="System.Threading.CancellationTokenSource.Dispose(bool)"/>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			IsDisposed = true;
		}

		/// <summary>
		/// Cancel and dispose. Will do nothing if it was already disposed.
		/// Will not cancel if cancellation was already requested.
		/// </summary>
		public void CancelAndDispose()
		{
			if (!IsDisposed)
			{
				if (!IsCancellationRequested)
					Cancel();

				Dispose();
			}
		}
	}
}
