using Elephant.Testing.Xunit;

namespace Elephant.Common.Tests
{
    /// <summary>
    /// <see cref="Trilean"/> tests.
    /// </summary>
    public class TrileanTests
    {
        /// <summary>
        /// <see cref="Equals"/> data.
        /// </summary>
        public static IEnumerable<object[]> EqualData =>
            new List<object[]>
            {
                new object[] { Trilean.True, Trilean.True, true },
                new object[] { Trilean.True, new Trilean(true), true },
                new object[] { Trilean.True, new Trilean(Trilean.Value.True), true },
                new object[] { Trilean.True, Trilean.False, false },
                new object[] { Trilean.True, new Trilean(false), false },
                new object[] { Trilean.True, new Trilean(Trilean.Value.False), false },
                new object[] { Trilean.Unknown, Trilean.Unknown, true },
                new object[] { Trilean.Unknown, Trilean.True, false },
                new object[] { Trilean.Unknown, Trilean.False, false },
                new object[] { Trilean.True, Trilean.False, false },
            };

        /// <summary>
        /// <see cref="Trilean"/> == tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [MemberData(nameof(EqualData))]
        public void TrileanEquals(Trilean a, Trilean b, bool expectedAreEqual)
        {
            if (expectedAreEqual)
                Assert.True(a == b);
            else
                Assert.False(a == b);
        }

        /// <summary>
        /// <see cref="NotEqual"/> data.
        /// </summary>
        public static IEnumerable<object[]> NotEqualData =>
            new List<object[]>
            {
                new object[] { Trilean.True, Trilean.True, true },
                new object[] { Trilean.True, new Trilean(true), true },
                new object[] { Trilean.True, new Trilean(Trilean.Value.True), true },
                new object[] { Trilean.True, Trilean.False, false },
                new object[] { Trilean.True, new Trilean(false), false },
                new object[] { Trilean.True, new Trilean(Trilean.Value.False), false },
                new object[] { Trilean.Unknown, Trilean.Unknown, true },
                new object[] { Trilean.Unknown, Trilean.True, false },
                new object[] { Trilean.Unknown, Trilean.False, false },
                new object[] { Trilean.True, Trilean.False, false },
            };

        /// <summary>
        /// <see cref="Trilean"/> != tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [MemberData(nameof(NotEqualData))]
        public void NotEqual(Trilean a, Trilean b, bool expectedAreEqual)
        {
            if (expectedAreEqual)
                Assert.False(a != b);
            else
                Assert.True(a != b);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign unknown tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void AssignUnknown()
        {
            Trilean trilean = new(null);
            Assert.True(trilean.IsUnknown);
            trilean = new(Trilean.Value.Unknown);
            Assert.True(trilean.IsUnknown);
            trilean = new();
            trilean.AssignUnknown();
            Assert.True(trilean.IsUnknown);
            Assert.Equal(Trilean.Value.Unknown, trilean.GetValue);
            Assert.False(trilean.IsFalse);
            Assert.False(trilean.IsTrue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign false tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void AssignFalse()
        {
            Trilean trilean = new(false);
            Assert.True(trilean.IsFalse);
            trilean = new(Trilean.Value.False);
            Assert.True(trilean.IsFalse);
            trilean = new();
            trilean.AssignFalse();
            Assert.True(trilean.IsFalse);
            Assert.Equal(Trilean.Value.False, trilean.GetValue);
            Assert.False(trilean.IsUnknown);
            Assert.False(trilean.IsTrue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign true tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void AssignTrue()
        {
            Trilean trilean = new(true);
            Assert.True(trilean.IsTrue);
            trilean = new(Trilean.Value.True);
            Assert.True(trilean.IsTrue);
            trilean = new();
            trilean.AssignTrue();
            Assert.True(trilean.IsTrue);
            Assert.Equal(Trilean.Value.True, trilean.GetValue);
            Assert.False(trilean.IsFalse);
            Assert.False(trilean.IsUnknown);
        }
    }
}
