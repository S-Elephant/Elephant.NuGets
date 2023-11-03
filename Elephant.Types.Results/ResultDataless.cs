using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results
{
	/// <inheritdoc cref="IResult"/>
	public class ResultDataless : Result<bool>, IResult
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ResultDataless()
		{
			UsesData = false;
		}
	}
}
