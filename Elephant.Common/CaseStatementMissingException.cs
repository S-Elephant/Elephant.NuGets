namespace Elephant.Common
{
    /// <summary>
    /// Use this when a case statement is missing.
    /// </summary>
    public class CaseStatementMissingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaseStatementMissingException"/> class.
        /// </summary>
        public CaseStatementMissingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseStatementMissingException"/> class.
        /// </summary>
        /// <param name="receivedValue">The missing case/switch value.</param>
        public CaseStatementMissingException(object receivedValue)
            : base($"Missing case statement. Got: {receivedValue}")
        {
        }
    }
}