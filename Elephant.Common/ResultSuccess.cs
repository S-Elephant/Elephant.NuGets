namespace Elephant.Common
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class ResultSuccess<T> : IResultSuccess<T>
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public T? Value { get; set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public bool IsSuccess { get; set; } = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultSuccess{T}"/> class.
		/// </summary>
		public ResultSuccess()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultSuccess{T}"/> class.
		/// </summary>
		public ResultSuccess(T? value, bool isSuccess)
		{
			Value = value;
			IsSuccess = isSuccess;
		}

		/// <summary>
		/// Creates a new failed <see cref="ResultSuccess{T}"/>..
		/// </summary>
		public static ResultSuccess<T> CreateFailure()
		{
			return new ResultSuccess<T>(default, false);
		}
	}
}
