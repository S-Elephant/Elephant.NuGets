using Elephant.Types.ResponseWrappers;

namespace Elephant.Types.Tests.ResponseWrappers
{
    /// <summary>
    /// <see cref="PagedResponseWrapper{T}"/> == tests.
    /// </summary>
    public class PagedResponseWrapperTests
    {
        /// <summary>
        /// <see cref="PagedResponseWrapper{T}"/> constructor tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void TestIfAddingReturnsExpectedValues()
        {
            // Arrange.
            PagedResponseWrapper<int> sut = new();

            // Act.
            sut.Data!.Add(5);
            sut.Data!.Add(15);
            sut.Data!.Add(25);

            // Assert.
            Assert.Equal(5, sut.Data[0]);
            Assert.Equal(15, sut.Data[1]);
            Assert.Equal(25, sut.Data[2]);
        }

        /// <summary>
        /// <see cref="PagedResponseWrapper{T}"/> overloaded constructor tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void TestIfAddingReturnsExpectedValuesUsingConstructorOverload()
        {
            // Arrange.
            List<int> data = new() { 5, 15, 25 };

            // Act.
            PagedResponseWrapper<int> sut = new(data, true, 200, "Success.", 1, 2, 2, true, false, data.Count, string.Empty, string.Empty);

            // Assert.
            Assert.Equal(5, sut.Data![0]);
            Assert.Equal(15, sut.Data![1]);
            Assert.Equal(25, sut.Data![2]);
        }

        /// <summary>
        /// <see cref="PagedResponseWrapper{T}"/> overloaded constructor tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void TestIfBadRequestReturns400()
        {
            // Arrange
            List<int> data = new() { 5, 15, 25 };
            PagedResponseWrapper<int> sut = new(data, true, 200, "Success.", 1, 2, 2, true, false, data.Count, string.Empty, string.Empty);

            // Act.
            sut.BadRequest();

            // Assert.
            Assert.Equal(PagedResponseWrapper<int>.Status400BadRequest, sut.StatusCode);
        }

        /// <summary>
        /// <see cref="PagedResponseWrapper{T}.BadRequest(List{string}?, string?)"/> tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast, UnitTest]
        public void TestIfBadRequestReturnsAnyData()
        {
            // Arrange
            List<int> data = new() { 5, 15, 25 };
            PagedResponseWrapper<int> sut = new(data, true, 200, "Success.", 1, 2, 2, true, false, data.Count, string.Empty, string.Empty);

            // Act.
            sut.BadRequest();

            // Assert.
            Assert.NotNull(sut.Data);
        }

        /// <summary>
        /// <see cref="PagedResponseWrapper{T}"/> test switching responses.
        /// </summary>
        [Fact]
        [SpeedVeryFast, IntegrationTest]
        public void TestSwitchingResponsesReturnsLastOne()
        {
            // Arrange
            List<int> data = new() { 5, 15, 25 };
            PagedResponseWrapper<int> sut = new(data, true, 200, "Success.", 1, 2, 2, true, false, data.Count, string.Empty, string.Empty);

            // Act.
            sut.BadRequest();
            sut.Unauthorized();
            sut.Created();

            // Assert.
            Assert.Equal(sut.StatusCode, PagedResponseWrapper<int>.Status201Created);
        }
    }
}
