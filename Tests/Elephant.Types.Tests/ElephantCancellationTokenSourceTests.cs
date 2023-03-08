namespace Elephant.Types.Tests
{
	/// <summary>
	/// <see cref="ElephantCancellationTokenSource"/> tests.
	/// </summary>
	public class ElephantCancellationTokenSourceTests : IDisposable
	{
		private ElephantCancellationTokenSource _systemUnderTest = new();

		/// <summary>
		/// <see cref="ElephantCancellationTokenSource"/> cancellation test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NewTokenIsNotDisposedAndNotCancelled()
		{
			// Assert.
			Assert.True(_systemUnderTest.IsNotDisposedAndNotCancellationRequested);
		}

		/// <summary>
		/// <see cref="ElephantCancellationTokenSource"/> cancellation test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CancelDoesntDispose()
		{
			// Act.
			_systemUnderTest.Cancel();

			// Assert.
			Assert.True(_systemUnderTest.IsCancellationRequested);
			Assert.True(_systemUnderTest.IsNotDisposed);
		}

		/// <summary>
		/// <see cref="ElephantCancellationTokenSource"/> cancellation test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CancelWorks()
		{
			// Act.
			_systemUnderTest.Cancel();

			// Assert.
			Assert.True(_systemUnderTest.IsCancellationRequested);
			Assert.True(_systemUnderTest.IsDisposedOrCancellationRequested);
		}

		/// <summary>
		/// <see cref="ElephantCancellationTokenSource"/> cancellation test after it was already disposed.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void CancelWorksAfterAlreadyDisposed()
		{
			// Arrange.
			_systemUnderTest.Dispose();

			// Act.
			_systemUnderTest.CancelAndDispose();

			// Assert.
			Assert.True(_systemUnderTest.IsDisposed);
			Assert.True(_systemUnderTest.IsDisposedOrCancellationRequested);
		}

		/// <inheritdoc cref="IDisposable.Dispose"/>
		public void Dispose()
		{
			_systemUnderTest.CancelAndDispose();
			GC.SuppressFinalize(this);
		}
	}
}
