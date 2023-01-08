using System;

namespace Elephant.Database.Utilities.Tests
{
    /// <summary>
    /// <see cref="IdService"/> tests.
    /// </summary>
    public class IdServiceTests
    {
        /// <summary>
        /// <see cref="IdService.IsIdInsert(int)"/> tests.
        /// </summary>
        [Theory]
        [InlineData(0, 1, -1, false)]
        [InlineData(0, 1, 0, true)]
        [InlineData(0, 1, 1, false)]
        [InlineData(0, 1, int.MinValue, false)]
        [InlineData(0, 1, int.MaxValue, false)]
        [InlineData(-5, 7, -6, false)]
        [InlineData(-5, 7, -5, true)]
        [InlineData(-5, 7, -3, false)]
        [InlineData(-5, 7, -7, false)]
        [InlineData(-5, 7, int.MinValue, false)]
        [InlineData(-5, 7, int.MaxValue, false)]
        [SpeedVeryFast, UnitTest]
        public void IsIdInsertTests(int insertId, int firstInsertId, int idToTest, bool expectedIsInsertId)
        {
            IdService systemUnderTest = new(insertId, firstInsertId);

            bool isIdInsert = systemUnderTest.IsIdInsert(idToTest);

            Assert.Equal(expectedIsInsertId, isIdInsert);
        }

        /// <summary>
        /// <see cref="IdService.IsIdUpdate(int)"/> tests.
        /// </summary>
        [Theory]
        [InlineData(0, 1, -1, false)]
        [InlineData(0, 1, 0, false)]
        [InlineData(0, 1, 1, true)]
        [InlineData(0, 1, int.MinValue, false)]
        [InlineData(0, 1, int.MaxValue, true)]
        [InlineData(-5, 7, -1, false)]
        [InlineData(-5, 7, 0, false)]
        [InlineData(-5, 7, 1, false)]
        [InlineData(-5, 7, 6, false)]
        [InlineData(-5, 7, 7, true)]
        [InlineData(-5, 7, int.MinValue, false)]
        [InlineData(-5, 7, int.MaxValue, true)]
        [SpeedVeryFast, UnitTest]
        public void IsIdUpdateTests(int insertId, int firstInsertId, int idToTest, bool expectedIsUpdateId)
        {
            IdService systemUnderTest = new(insertId, firstInsertId);

            bool isIdInsert = systemUnderTest.IsIdUpdate(idToTest);

            Assert.Equal(expectedIsUpdateId, isIdInsert);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(-2, -5)]
        [InlineData(int.MaxValue, int.MinValue)]
        [SpeedVeryFast, UnitTest]
        public void InvalidConstructorParameterShouldThrow(int insertId, int firstInsertId)
        {
            // Arrange.
            Action a = () => new IdService(insertId, firstInsertId);

            // Act/Assert.
            Assert.Throws<ArgumentOutOfRangeException>(a);
        }
    }
}