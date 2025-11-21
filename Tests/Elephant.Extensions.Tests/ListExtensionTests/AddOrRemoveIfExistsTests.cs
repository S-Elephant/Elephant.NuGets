namespace Elephant.Extensions.Tests.ListExtensionTests
{
    /// <summary>
    /// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> tests.
    /// </summary>
    public class AddOrRemoveIfExistsTests
    {
        /// <summary>
        /// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should remove a number.
        /// </summary>
        [Fact]
        [SpeedFast, UnitTest]
        public void ValueIsRemoved1()
        {
            List<int> list = new () { -10, 1, 2, 3 };

            list.AddOrRemoveIfExists(1);

            Assert.Equal(new List<int>() { -10, 2, 3 }, list);
        }

        /// <summary>
        /// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should remove a number.
        /// </summary>
        [Fact]
        [SpeedFast, UnitTest]
        public void ValueIsRemovedminus10()
        {
            List<int> list = new () { -10, 1, 2, 3 };

            list.AddOrRemoveIfExists(-10);

            Assert.Equal(new List<int>() { 1, 2, 3 }, list);
        }

        /// <summary>
        /// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should add a number.
        /// </summary>
        [Fact]
        [SpeedFast, UnitTest]
        public void ValueIsAdded4()
        {
            List<int> list = new () { -10, 1, 2, 3 };

            list.AddOrRemoveIfExists(4);

            Assert.Equal(new List<int>() { -10, 1, 2, 3, 4 }, list);
        }

        /// <summary>
        /// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should add a number.
        /// </summary>
        [Fact]
        [SpeedFast, UnitTest]
        public void ValueIsAddedMinus2()
        {
            List<int> list = new () { -10, 1, 2, 3 };

            list.AddOrRemoveIfExists(-2);

            Assert.Equal(new List<int>() { -10, 1, 2, 3, -2 }, list);
        }

        /// <summary>
        /// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should add a number.
        /// </summary>
        [Fact]
        [SpeedFast, UnitTest]
        public void ValueIsAddedToEmptyList()
        {
            List<int> list = new ();

            list.AddOrRemoveIfExists(0);

            Assert.Equal(new List<int>() { 0 }, list);
        }
    }
}
