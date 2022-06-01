namespace Elephant.Common.Tests.TrileanTests
{
    /// <summary>
    /// Trilean test data.
    /// </summary>
    public class TrileanTestData
    {
        /// <summary>
        /// <see cref="AllTrileanTests.TrileansAreEqual(Trilean, Trilean)"/> and
        /// <see cref="AllTrileanTests.TrileansAreEqualByOperator(Trilean, Trilean)"/> test data.
        /// </summary>
        public static TheoryData<Trilean, Trilean> EqualData => new()
        {
            { Trilean.True, Trilean.True },
            { Trilean.True, new Trilean(true) },
            { Trilean.True, new Trilean(Trilean.Value.True) },
            { Trilean.Unknown, Trilean.Unknown },
        };

        /// <summary>
        /// <see cref="AllTrileanTests.TrileansAreUnequal(Trilean, Trilean)"/> and
        /// <see cref="AllTrileanTests.TrileansAreUnequalsByOperator(Trilean, Trilean)"/> test data.
        /// </summary>
        public static TheoryData<Trilean, Trilean> UnequalData => new()
        {
            { Trilean.True, Trilean.False },
            { Trilean.True, new Trilean(false) },
            { Trilean.True, new Trilean(Trilean.Value.False) },
            { Trilean.Unknown, Trilean.True },
            { Trilean.Unknown, Trilean.False },
            { Trilean.True, Trilean.False },
        };

        /// <summary>
        /// <see cref="AllTrileanTests.NotEqual(Trilean, Trilean, bool)"/> test data.
        /// </summary>
        public static TheoryData<Trilean, Trilean, bool> NotEqualData => new()
        {
            { Trilean.True, Trilean.True, true },
            { Trilean.True, new Trilean(true), true },
            { Trilean.True, new Trilean(Trilean.Value.True), true },
            { Trilean.True, Trilean.False, false },
            { Trilean.True, new Trilean(false), false },
            { Trilean.True, new Trilean(Trilean.Value.False), false },
            { Trilean.Unknown, Trilean.Unknown, true },
            { Trilean.Unknown, Trilean.True, false },
            { Trilean.Unknown, Trilean.False, false },
            { Trilean.True, Trilean.False, false },
        };
    }
}
