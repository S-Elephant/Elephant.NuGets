﻿namespace Elephant.Extensions.Tests
{
    /// <summary>
    /// <see cref="BetweenExtensions"/> tests.
    /// </summary>
    public class BetweenExtensionsTests
    {
        /// <summary>
        /// <see cref="BetweenExtensions.IsBetweenII{T}(T, T, T)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(5.0f, 5.0f, 5.0f, true)]
        [InlineData(5.0f, 4.9f, 5.0f, true)]
        [InlineData(5.0f, 4.9f, 5.1f, true)]
        [InlineData(5.0f, 5.1f, 4.9f, false)]
        // ReSharper disable once InconsistentNaming
        public void IsBetweenIITests(float value, float min, float max, bool expectedAreEqual)
        {
            Assert.Equal(expectedAreEqual, value.IsBetweenII(min, max));
        }

        /// <summary>
        /// <see cref="BetweenExtensions.IsBetweenIE{T}(T, T, T)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(5.0f, 5.0f, 5.0f, false)]
        [InlineData(5.0f, 4.9f, 5.0f, false)]
        [InlineData(5.0f, 4.9f, 5.1f, true)]
        [InlineData(5.0f, 5.1f, 4.9f, false)]
        // ReSharper disable once InconsistentNaming
        public void IsBetweenIETests(float value, float min, float max, bool expectedAreEqual)
        {
            Assert.Equal(expectedAreEqual, value.IsBetweenIE(min, max));
        }

        /// <summary>
        /// <see cref="BetweenExtensions.IsBetweenEI{T}(T, T, T)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(5.0f, 5.0f, 5.0f, false)]
        [InlineData(5.0f, 4.9f, 5.0f, true)]
        [InlineData(5.0f, 4.9f, 5.1f, true)]
        [InlineData(5.0f, 5.1f, 4.9f, false)]
        // ReSharper disable once InconsistentNaming
        public void IsBetweenEITests(float value, float min, float max, bool expectedAreEqual)
        {
            Assert.Equal(expectedAreEqual, value.IsBetweenEI(min, max));
        }

        /// <summary>
        /// <see cref="BetweenExtensions.IsBetweenEE{T}(T, T, T)"/> tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [InlineData(5.0f, 5.0f, 5.0f, false)]
        [InlineData(5.0f, 4.9f, 5.0f, false)]
        [InlineData(5.0f, 4.9f, 5.1f, true)]
        [InlineData(5.0f, 5.1f, 4.9f, false)]
        // ReSharper disable once InconsistentNaming
        public void IsBetweenEETests(float value, float min, float max, bool expectedAreEqual)
        {
            Assert.Equal(expectedAreEqual, value.IsBetweenEE(min, max));
        }
    }
}
