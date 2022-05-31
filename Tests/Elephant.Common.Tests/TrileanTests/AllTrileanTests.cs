namespace Elephant.Common.Tests.TrileanTests
{
    /// <summary>
    /// <see cref="Trilean"/> tests.
    /// </summary>
    public class AllTrileanTests
    {
        #region Data

        /// <summary>
        /// <see cref="TrileanEquals"/> data.
        /// </summary>
        public static IEnumerable<object[]> EqualData =>
            new List<object[]>
            {
                new object[] { Trilean.True, Trilean.True },
                new object[] { Trilean.True, new Trilean(true) },
                new object[] { Trilean.True, new Trilean(Trilean.Value.True) },
                new object[] { Trilean.Unknown, Trilean.Unknown },
            };

        /// <summary>
        /// <see cref="TrileanUnequals"/> data.
        /// </summary>
        public static IEnumerable<object[]> UnequalData =>
            new List<object[]>
            {
                new object[] { Trilean.True, Trilean.False },
                new object[] { Trilean.True, new Trilean(false) },
                new object[] { Trilean.True, new Trilean(Trilean.Value.False) },
                new object[] { Trilean.Unknown, Trilean.True },
                new object[] { Trilean.Unknown, Trilean.False },
                new object[] { Trilean.True, Trilean.False },
            };

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

        #endregion

        /// <summary>
        /// <see cref="Trilean"/> == tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [MemberData(nameof(EqualData))]
        public void TrileanEquals(Trilean a, Trilean b)
        {
            Assert.True(a == b);
            Assert.Equal(a, b);
        }

        /// <summary>
        /// <see cref="Trilean"/> == tests.
        /// </summary>
        [Theory]
        [SpeedVeryFast]
        [MemberData(nameof(UnequalData))]
        public void TrileanUnequals(Trilean a, Trilean b)
        {
            Assert.False(a == b);
            Assert.NotEqual(a, b);
        }

        /// <summary>
        /// <see cref="Trilean"/> != tests.
        /// </summary>
        /// <remarks>The use of an if-else inside a test is not best-practice.</remarks>
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
        /// <see cref="Trilean"/> assign unknown test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIfTrileanIsUnknownIfNull()
        {
            Trilean trilean = new(null);
            
            Assert.True(trilean.IsUnknown);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign unknown test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIfTrileanIsUnknownIfUnknown()
        {
            Trilean trilean = new(Trilean.Value.Unknown);
            
            Assert.True(trilean.IsUnknown);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign unknown test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIfTrileanIsUnknownIfUnknownThroughMethod()
        {
            Trilean trilean = new();
            trilean.AssignUnknown();
            
            Assert.True(trilean.IsUnknown);
        }

        /// <summary>
        /// <see cref="Trilean.GetValue"/> assign unknown test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIfTrileanGetValueIsUnknownIfUnknown()
        {
            Trilean trilean = new();
            trilean.AssignUnknown();

            Assert.Equal(Trilean.Value.Unknown, trilean.GetValue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign unknown test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIfTrileanIsNeitherTrueNorFalseIfUnknown()
        {
            Trilean trilean = new();
            trilean.AssignUnknown();

            Assert.False(trilean.IsFalse || trilean.IsTrue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign false test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsFalseIfAssignFalse()
        {
            Trilean trilean = new(false);
        
            Assert.True(trilean.IsFalse);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign false test.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsFalseIfAssignFalseEnum()
        {
            Trilean trilean = new(Trilean.Value.False);
            
            Assert.True(trilean.IsFalse);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign false tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsFalseIfAssignFalseByMethod()
        {
            Trilean trilean = new();
            trilean.AssignFalse();
            
            Assert.True(trilean.IsFalse);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign false tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsFalseEnumIfAssignFalseByMethod()
        {
            Trilean trilean = new();
            trilean.AssignFalse();
            
            Assert.Equal(Trilean.Value.False, trilean.GetValue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign false tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsNotUnknownAndNotTrueIfAssignFalse()
        {
            Trilean trilean = new();
            trilean.AssignFalse();
            
            Assert.False(trilean.IsUnknown || trilean.IsTrue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign true tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsTrueIfAssignTrue()
        {
            Trilean trilean = new(true);
            
            Assert.True(trilean.IsTrue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign true tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsTrueIfAssignTrueEnum()
        {
            Trilean trilean = new(Trilean.Value.True);

            Assert.True(trilean.IsTrue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign true tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsTrueIfAssignTrueByMethod()
        {
            Trilean trilean = new();
            trilean.AssignTrue();
            
            Assert.True(trilean.IsTrue);
        }

        /// <summary>
        /// <see cref="Trilean"/> assign true tests.
        /// </summary>
        [Fact]
        [SpeedVeryFast]
        public void TestIsNotUnknownAndNotFalseIfAssignTrue()
        {
            Trilean trilean = new(true);

            Assert.False(trilean.IsUnknown || trilean.IsFalse);
        }
    }
}
