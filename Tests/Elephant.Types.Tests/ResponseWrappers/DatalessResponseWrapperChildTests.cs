using Elephant.Types.ResponseWrappers;

namespace Elephant.Types.Tests.ResponseWrappers
{
    /// <summary>
    /// <see cref="ResponseWrapper{T}"/> child tests for types without data.
    /// </summary>
    public class DatalessResponseWrapperChildTests
	{
        /// <summary>
        /// <see cref="ResponseWrapperOk"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void OkIsSuccess()
        {
            // Arrange and act.
            ResponseWrapperOk sut = new("Foo");

            // Assert.
            Assert.True(sut.IsSuccess);
        }

        /// <summary>
        /// <see cref="ResponseWrapperCreated"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void CreatedIsSuccess()
        {
            // Arrange and act.
            ResponseWrapperCreated sut = new("Foo");

            // Assert.
            Assert.True(sut.IsSuccess);
        }

        /// <summary>
        /// <see cref="ResponseWrapperBadRequest"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void BadRequestIsError()
        {
            // Arrange and act.
            ResponseWrapperBadRequest sut = new("Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperInternalServerError"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void InternalServerErrorIsError()
        {
            // Arrange and act.
            ResponseWrapperInternalServerError sut = new("Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperNotFound"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void NotFoundIsError()
        {
            // Arrange and act.
            ResponseWrapperNotFound sut = new("Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperUnprocessableEntity"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void UnprocessableEntityIsError()
        {
            // Arrange and act.
            ResponseWrapperUnprocessableEntity sut = new("Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperUnauthorized"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void UnauthorizedIsError()
        {
            // Arrange and act.
            ResponseWrapperUnauthorized sut = new("Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperNoRecordsAffected"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void NoRecordsAffectedIsError()
        {
            // Arrange and act.
            ResponseWrapperUnauthorized sut = new("Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperNoContent"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void NoContentIsSuccess()
        {
            // Arrange and act.
            ResponseWrapperNoContent sut = new();

            // Assert.
            Assert.True(sut.IsSuccess);
        }
    }
}
