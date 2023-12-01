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
            IdService systemUnderTest = new (insertId, firstInsertId);

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
            IdService systemUnderTest = new (insertId, firstInsertId);

            bool isIdInsert = systemUnderTest.IsIdUpdate(idToTest);

            Assert.Equal(expectedIsUpdateId, isIdInsert);
        }

        /// <summary>
        /// <see cref="IdService.IsIdNotInsert(int)"/> tests.
        /// </summary>
        [Theory]
        [InlineData(0, 1, -1, true)]
        [InlineData(0, 1, 0, false)]
        [InlineData(0, 1, 1, true)]
        [InlineData(0, 1, int.MinValue, true)]
        [InlineData(0, 1, int.MaxValue, true)]
        [InlineData(-5, 7, -6, true)]
        [InlineData(-5, 7, -5, false)]
        [InlineData(-5, 7, -3, true)]
        [InlineData(-5, 7, -7, true)]
        [InlineData(-5, 7, int.MinValue, true)]
        [InlineData(-5, 7, int.MaxValue, true)]
        [SpeedVeryFast, UnitTest]
        public void IsIdNotInsertTests(int insertId, int firstInsertId, int idToTest, bool expectedIsInsertId)
        {
            IdService systemUnderTest = new (insertId, firstInsertId);

            bool isIdInsert = systemUnderTest.IsIdNotInsert(idToTest);

            Assert.Equal(expectedIsInsertId, isIdInsert);
        }

        /// <summary>
        /// <see cref="IdService.IsIdNotUpdate(int)"/> tests.
        /// </summary>
        [Theory]
        [InlineData(0, 1, -1, true)]
        [InlineData(0, 1, 0, true)]
        [InlineData(0, 1, 1, false)]
        [InlineData(0, 1, int.MinValue, true)]
        [InlineData(0, 1, int.MaxValue, false)]
        [InlineData(-5, 7, -1, true)]
        [InlineData(-5, 7, 0, true)]
        [InlineData(-5, 7, 1, true)]
        [InlineData(-5, 7, 6, true)]
        [InlineData(-5, 7, 7, false)]
        [InlineData(-5, 7, int.MinValue, true)]
        [InlineData(-5, 7, int.MaxValue, false)]
        [SpeedVeryFast, UnitTest]
        public void IsIdNotUpdateTests(int insertId, int firstInsertId, int idToTest, bool expectedIsUpdateId)
        {
            IdService systemUnderTest = new (insertId, firstInsertId);

            bool isIdInsert = systemUnderTest.IsIdNotUpdate(idToTest);

            Assert.Equal(expectedIsUpdateId, isIdInsert);
        }

        /// <summary>
        /// Invalid constructor should throw.
        /// </summary>
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(-2, -5)]
        [InlineData(int.MaxValue, int.MinValue)]
        [SpeedVeryFast, UnitTest]
        public void InvalidConstructorParameterShouldThrow(int insertId, int firstInsertId)
        {
            // Arrange.
            // ReSharper disable once ObjectCreationAsStatement
            Action a = () => new IdService(insertId, firstInsertId);

            // Act/Assert.
            Assert.Throws<ArgumentOutOfRangeException>(a);
        }

        /// <summary>
        /// <see cref="IdService.IsValid(int)"/> tests.
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
        [SpeedVeryFast, UnitTest]
        public void ValidIdShouldreturnTrue(int id)
        {
            IdService idService = new (0, 1);

            bool isValid = idService.IsValid(id);

            Assert.True(isValid);
        }

        /// <summary>
        /// <see cref="IdService.IsInvalid(int)"/> tests.
        /// </summary>
        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-1000)]
        [InlineData(int.MinValue)]
        [SpeedVeryFast, UnitTest]
        public void InvalidIdShouldreturnTrue(int id)
        {
            IdService idService = new (0, 1);

            bool isInvalid = idService.IsInvalid(id);

            Assert.True(isInvalid);
        }
    }
}