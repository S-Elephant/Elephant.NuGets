using System.Diagnostics.CodeAnalysis;

namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Success (HTTP response code 200) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public class ResponseWrapperOk<TData> : ResponseWrapper<TData>
        where TData : new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseWrapperOk(TData? data = default, string message = "Success.")
            : base(data, 200, message)
        {
        }
    }

	/// <summary>
	/// Success (HTTP response code 200) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperOk : ResponseWrapper
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperOk(string message = "Success.")
            : base(200, message)
		{
		}
	}
}
