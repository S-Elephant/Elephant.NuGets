using Elephant.Database.Utilities.Interfaces;
using System;

namespace Elephant.Database.Utilities
{
    /// <inheritdoc/>
    public class IdService : IIdService
    {
        private int _firstInsertId = 1;

        /// <inheritdoc/>
        public int FirstInsertId
        {
            get => _firstInsertId;
            set
            {
                if (value <= _insertId)
                    throw new ArgumentOutOfRangeException($"{nameof(FirstInsertId)} must always be greater than {nameof(InsertId)}.");

                _firstInsertId = value;
            }
        }

        private int _insertId = 0;

        /// <inheritdoc/>
        public int InsertId
        {
            get => _insertId;
            set
            {
                if (value >= _firstInsertId)
                    throw new ArgumentOutOfRangeException($"{nameof(InsertId)} must always be smaller than {nameof(FirstInsertId)}.");

                _insertId = value;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public IdService()
        {
        }

        /// <summary>
        /// Constructor with initializers.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the parameter ranges are bad.</exception>
        public IdService(int insertId, int firstInsertId)
        {
            _insertId = insertId;
            _firstInsertId = firstInsertId;

            if (insertId >= _firstInsertId)
                throw new ArgumentOutOfRangeException($"{nameof(InsertId)} must always be smaller than {nameof(FirstInsertId)}.");

            if (firstInsertId <= _insertId)
                throw new ArgumentOutOfRangeException($"{nameof(FirstInsertId)} must always be greater than {nameof(InsertId)}.");
        }

        /// <inheritdoc/>
        public bool IsIdInsert(int id)
        {
            return id == InsertId;
        }

        /// <inheritdoc/>
        public bool IsIdUpdate(int id)
        {
            return id >= FirstInsertId;
        }
    }
}
