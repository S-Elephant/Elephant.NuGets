using Elephant.Types.ResponseWrappers;

namespace Elephant.Types.Tests.ResponseWrappers
{
    /// <summary>
    /// <see cref="ResponseWrapper{T}"/> HTTP status code tests.
    /// </summary>
    public class ResponseWrapperStatusCodeTests
    {
        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes equal success.
        /// </summary>
        [Theory]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(299)]
        [SpeedVeryFast, UnitTest]
        public void IsSuccessCodeSuccess(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.True(sut.IsSuccess);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes don't equal an error.
        /// </summary>
        [Theory]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(299)]
        [SpeedVeryFast, UnitTest]
        public void IsSuccessCodeNotAnError(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.False(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes don't equal an informative, redirection or custom.
        /// </summary>
        [Theory]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(299)]
        [SpeedVeryFast, UnitTest]
        public void IsSuccessCodeNotInformativeRedirectionOrCustom(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.False(sut.IsInformativeRedirectionOrCustom);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes equal error.
        /// </summary>
        [Theory]
        [InlineData(400)]
        [InlineData(401)]
        [InlineData(499)]
        [InlineData(500)]
        [InlineData(599)]
        [SpeedVeryFast, UnitTest]
        public void IsErrorCodeError(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes don't equal an error.
        /// </summary>
        [Theory]
        [InlineData(400)]
        [InlineData(401)]
        [InlineData(499)]
        [InlineData(500)]
        [InlineData(599)]
        [SpeedVeryFast, UnitTest]
        public void IsErrorCodeIsNotSuccess(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.False(sut.IsSuccess);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes don't equal an informative, redirection or custom.
        /// </summary>
        [Theory]
        [InlineData(400)]
        [InlineData(401)]
        [InlineData(499)]
        [InlineData(500)]
        [InlineData(599)]
        [SpeedVeryFast, UnitTest]
        public void IsErrorCodeNotInformativeRedirectionOrCustom(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.False(sut.IsInformativeRedirectionOrCustom);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes equal InformativeRedirectionOrCustom.
        /// </summary>
        [Theory]
        [InlineData(100)]
        [InlineData(199)]
        [InlineData(300)]
        [InlineData(399)]
        [SpeedVeryFast, UnitTest]
        public void IsInformativeRedirectionOrCustomCodeInformativeRedirectionOrCustom(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.True(sut.IsInformativeRedirectionOrCustom);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes don't equal an error.
        /// </summary>
        [Theory]
        [InlineData(100)]
        [InlineData(199)]
        [InlineData(300)]
        [InlineData(399)]
        [SpeedVeryFast, UnitTest]
        public void IsInformativeRedirectionOrCustomCodeNotAnError(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.False(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes don't equal an informative, redirection or custom.
        /// </summary>
        [Theory]
        [InlineData(100)]
        [InlineData(199)]
        [InlineData(300)]
        [InlineData(399)]
        [SpeedVeryFast, UnitTest]
        public void IsInformativeRedirectionOrCustomCodeNotSuccess(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.False(sut.IsSuccess);
        }

        /// <summary>
        /// <see cref="ResponseWrapper{T}"/> constructor test if HTTP response codes are properly preserved.
        /// </summary>
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(400)]
        [InlineData(500)]
        [SpeedVeryFast, UnitTest]
        public void IsStatusCodePreserved(int httpResponseCode)
        {
            // Arrange and act.
            ResponseWrapper<int> sut = new(5, httpResponseCode);

            // Assert.
            Assert.Equal(sut.StatusCode, httpResponseCode);
        }
    }
}
