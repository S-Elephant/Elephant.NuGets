﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Elephant.Types.ResponseWrappers
{
	/// <summary>
	/// Error (HTTP response code 500) <see cref="ResponseWrapper{TData}"/>.
	/// </summary>
	[SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public class ResponseWrapperInternalServerError<TData> : ResponseWrapper<TData>
		where TData : new()
	{
		// ReSharper disable once InconsistentNaming
		private const int StatusCodeInternalServerError = 500;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(TData? data = default, string message = "Internal server error.")
            : base(data, StatusCodeInternalServerError, message)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(string message)
            : base(default, StatusCodeInternalServerError, message)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(Exception exception)
            : base(default, StatusCodeInternalServerError, exception.ToString())
		{
		}
	}

	/// <summary>
	/// Error (HTTP response code 500) <see cref="ResponseWrapper"/>.
	/// </summary>
	public class ResponseWrapperInternalServerError : ResponseWrapper
	{
		// ReSharper disable once InconsistentNaming
		private const int StatusCodeInternalServerError = 500;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(string message = "Internal server error.")
            : base(StatusCodeInternalServerError, message)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResponseWrapperInternalServerError(Exception exception)
            : base(StatusCodeInternalServerError, exception.ToString())
		{
		}
	}
}
