using Elephant.Types.ResponseWrappers;

namespace Elephant.Types.Tests.ResponseWrappers
{
    /// <summary>
    /// <see cref="ResponseWrapper{T}"/> child tests.
    /// </summary>
    public class ResponseWrapperChildTests
    {
        /// <summary>
        /// <see cref="ResponseWrapperOk{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void OkIsSuccess()
        {
            // Arrange and act.
            ResponseWrapperOk<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsSuccess);
        }

        /// <summary>
        /// <see cref="ResponseWrapperCreated{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void CreatedIsSuccess()
        {
            // Arrange and act.
            ResponseWrapperCreated<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsSuccess);
        }

        /// <summary>
        /// <see cref="ResponseWrapperBadRequest{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void BadRequestIsError()
        {
            // Arrange and act.
            ResponseWrapperBadRequest<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperInternalServerError{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void InternalServerErrorIsError()
        {
            // Arrange and act.
            ResponseWrapperInternalServerError<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperNotFound{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void NotFoundIsError()
        {
            // Arrange and act.
            ResponseWrapperNotFound<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperUnprocessableEntity{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void UnprocessableEntityIsError()
        {
            // Arrange and act.
            ResponseWrapperUnprocessableEntity<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperUnauthorized{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void UnauthorizedIsError()
        {
            // Arrange and act.
            ResponseWrapperUnauthorized<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperNoRecordsAffected{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void NoRecordsAffectedIsError()
        {
            // Arrange and act.
            ResponseWrapperUnauthorized<int> sut = new (-1, "Foo");

            // Assert.
            Assert.True(sut.IsError);
        }

        /// <summary>
        /// <see cref="ResponseWrapperNoContent{TData}"/> test.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void NoContentIsSuccess()
        {
            // Arrange and act.
            ResponseWrapperNoContent<int> sut = new ();

            // Assert.
            Assert.True(sut.IsSuccess);
        }
    }
}
