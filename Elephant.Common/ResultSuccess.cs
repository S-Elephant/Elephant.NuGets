namespace Elephant.Common
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Obsolete("Use ResponseWrappers.ResponseWrapper instead.")]
    public class ResultSuccess<TData> : IResultSuccess<TData>
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public TData? Value { get; set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public bool IsSuccess { get; set; } = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultSuccess{TData}"/> class.
		/// </summary>
		public ResultSuccess()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ResultSuccess{TData}"/> class.
		/// </summary>
		public ResultSuccess(TData? value, bool isSuccess)
		{
			Value = value;
			IsSuccess = isSuccess;
		}

		/// <summary>
		/// Creates a new failed <see cref="ResultSuccess{TData}"/>..
		/// </summary>
		public static ResultSuccess<TData> CreateFailure()
		{
			return new ResultSuccess<TData>(default, false);
		}
	}
}
