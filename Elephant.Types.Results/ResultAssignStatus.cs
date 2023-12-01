using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results
{
	/// <inheritdoc cref="IResult{TData}"/>
	[SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public partial class Result<TData>
	{
		#region HTTP Status codes that are missing in .Net Standard 2.0

		/// <summary>
		/// Indicates that the server has received and is processing the request, but no response is available yet.
		/// </summary>
		public const int HttpStatusCodeProcessing = 102;

		/// <summary>
		/// Is primarily intended to be used with the Link header, letting the user agent start preloading resources while the server prepares a response.
		/// </summary>
		public const int HttpStatusCodeEarlyHints = 103;

		/// <summary>
		/// Conveys information about multiple resources, for situations where multiple status codes
		/// might be appropriate.
		/// </summary>
		public const int HttpStatusCodeMultiStatus = 207;

		/// <summary>
		/// Used inside a <![CDATA[<dav:propstat>]]> response element to avoid repeatedly enumerating the
		/// internal members of multiple bindings to the same collection.
		/// </summary>
		public const int HttpStatusCodeAlreadyReported = 208;

		/// <summary>
		/// The server has fulfilled a GET request for the resource, and the response is a representation of
		/// the result of one or more instance-manipulations applied to the current instance.
		/// </summary>
		public const int HttpStatusCodeImUsed = 226;

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		public const int HttpStatusCodePermanentRedirect = 308;

		/// <summary>
		/// Request entity is larger than limits defined by server. The server
		/// might close the connection or return an Retry-After header field.
		/// </summary>
		public const int HttpStatusCodePayloadTooLarge = 413;

		/// <summary>
		/// URI is longer than the server is willing to interpret.
		/// </summary>
		public const int HttpStatusCodeUriTooLong = 414;

		/// <summary>
		/// Range specified by the Range header field in the request cannot be fulfilled.
		/// It's possible that the range is outside the size of the target URI's data.
		/// </summary>
		public const int HttpStatusCodeRangeNotSatisfiable = 416;

		/// <summary>
		/// Server refuses the attempt to brew coffee with a teapot.
		/// </summary>
		public const int HttpStatusCodeImATeapot = 418;

		/// <summary>
		/// Request was directed at a server that is not able to produce a response.
		/// This can be sent by a server that is not configured to produce responses
		/// for the combination of scheme and authority that are included in the
		/// request URI.
		/// </summary>
		public const int HttpStatusCodeMisdirectedRequest = 421;

		/// <summary>
		/// Request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		public const int HttpStatusCodeUnprocessableContent = 422;

		/// <summary>
		/// Resource that is being accessed is locked.
		/// </summary>
		public const int HttpStatusCodeLocked = 423;

		/// <summary>
		/// Request failed due to failure of a previous request.
		/// </summary>
		public const int HttpStatusCodeFailedDependency = 424;

		/// <summary>
		/// Server is unwilling to risk processing a request that might be replayed.
		/// </summary>
		public const int HttpStatusCodeTooEarly = 425;

		/// <summary>
		/// Origin server requires the request to be conditional. This response is intended
		/// to prevent the 'lost update' problem, where a client GETs a resource's state,
		/// modifies it and PUTs it back to the server, when meanwhile a third party has
		/// modified the state on the server, leading to a conflict.
		/// </summary>
		public const int HttpStatusCodePreconditionRequired = 428;

		/// <summary>
		/// Received too many requests in a given amount of time.
		/// </summary>
		public const int HttpStatusCodeTooManyRequests = 429;

		/// <summary>
		/// Server is unwilling to process the request because its header fields
		/// are too large. The request may be resubmitted after reducing the size
		/// </summary>
		public const int HttpStatusCodeRequestHeaderFieldsTooLarge = 431;

		/// <summary>
		/// User agent requested a resource that cannot legally be provided,
		/// such as a web page censored by a government.
		/// </summary>
		public const int HttpStatusCodeUnavailableForLegalReasons = 451;

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		public const int HttpStatusCodeVariantAlsoNegotiates = 506;

		/// <summary>
		/// Server is unable to store the representation needed to successfully complete the request.
		/// </summary>
		public const int HttpStatusCodeInsufficientStorage = 507;

		/// <summary>
		/// Infinite loop detected.
		/// </summary>
		public const int HttpStatusCodeLoopDetected = 508;

		/// <summary>
		/// Further extensions to the request are required for the server to fulfill it.
		/// </summary>
		public const int HttpStatusCodeNotExtended = 510;

		/// <summary>
		/// Client needs to authenticate to gain network access.
		/// </summary>
		public const int HttpStatusCodeNetworkAuthenticationRequired = 511;

		#endregion

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="httpStatusCode">Custom HTTP status code.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Custom(TData? data, int httpStatusCode, string? message = null)
		{
			return new Result<TData>().AddCustom(data, httpStatusCode, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="httpStatusCode">Custom HTTP status code.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult CustomNoData(int httpStatusCode, string? message = null)
		{
			return (IResult)new ResultDataless().AddCustom(false, httpStatusCode, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddCustom(TData? data, int httpStatusCode, string? message = null)
		{
			ResultStatuses.Add(new ResultStatus<TData>(data, httpStatusCode, message));
			return this;
		}

		/// <inheritdoc/>
		public virtual IResult AddCustomNoData(int httpStatusCode, string? message = null)
		{
			ResultStatuses.Add(new ResultStatus<TData>(default, httpStatusCode, message));
			return (IResult)this;
		}

		#region Information

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Continue(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddContinue(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ContinueNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddContinue(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddContinue(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Continue, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddContinueNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Continue, message);
		}



		/// <summary>
		/// In response to an Upgrade request header from the client and indicates the protocol the server is switching to.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> SwitchingProtocols(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddSwitchingProtocols(data, message);
		}

		/// <summary>
		/// In response to an Upgrade request header from the client and indicates the protocol the server is switching to.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult SwitchingProtocolsNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddSwitchingProtocols(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddSwitchingProtocols(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.SwitchingProtocols, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddSwitchingProtocolsNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.SwitchingProtocols, message);
		}



		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Processing(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddProcessing(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ProcessingNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddProcessing(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddProcessing(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeProcessing, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddProcessingNoData(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeProcessing, message);
		}



		/// <summary>
		/// Is primarily intended to be used with the Link header, letting the user agent start preloading resources while the server prepares a response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> EarlyHints(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddEarlyHints(data, message);
		}

		/// <summary>
		/// Is primarily intended to be used with the Link header, letting the user agent start preloading resources while the server prepares a response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult EarlyHintsNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddEarlyHints(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddEarlyHints(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeEarlyHints, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddEarlyHintsNoData(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeEarlyHints, message);
		}

		#endregion

		#region Success

		/// <summary>
		/// Success.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Ok(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddOk(data, message);
		}

		/// <summary>
		/// Success.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult OkNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddOk(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddOk(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.OK, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddOkNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.OK, message);
		}



		/// <summary>
		/// Success and a new resource was created as a result.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Created(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddCreated(data, message);
		}

		/// <summary>
		/// Success and a new resource was created as a result.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult CreatedNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddCreated(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddCreated(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Created, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddCreatedNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Created, message);
		}



		/// <summary>
		/// Request has been received but not yet acted upon. It is noncommittal, since there is no way in HTTP to later
		/// send an asynchronous response indicating the outcome of the request. It is intended for cases where another
		/// process or server handles the request, or for batch processing.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Accepted(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddAccepted(data, message);
		}

		/// <summary>
		/// Request has been received but not yet acted upon. It is noncommittal, since there is no way in HTTP to later
		/// send an asynchronous response indicating the outcome of the request. It is intended for cases where another
		/// process or server handles the request, or for batch processing.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult AcceptedNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddAccepted(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddAccepted(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Accepted, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddAcceptedNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Accepted, message);
		}



		/// <summary>
		/// Means the returned metadata is not exactly the same as is available from the origin server, but is collected
		/// from a local or a third-party copy. This is mostly used for mirrors or backups of another resource. Except for
		/// that specific case, the 200 OK response is preferred to this status.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NonAuthoritativeInformation(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddNonAuthoritativeInformation(data, message);
		}

		/// <summary>
		/// Means the returned metadata is not exactly the same as is available from the origin server, but is collected
		/// from a local or a third-party copy. This is mostly used for mirrors or backups of another resource. Except for
		/// that specific case, the 200 OK response is preferred to this status.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NonAuthoritativeInformationNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddNonAuthoritativeInformation(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNonAuthoritativeInformation(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NonAuthoritativeInformation, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNonAuthoritativeInformationNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NonAuthoritativeInformation, message);
		}



		/// <summary>
		/// Success, but there's no content.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NoContent(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddNoContent(data, message);
		}

		/// <summary>
		/// Success, but there's no content.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NoContentNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddNoContent(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNoContent(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NoContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNoContentNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NoContent, message);
		}



		/// <summary>
		/// Tells the user agent to reset the document which sent this request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ResetContent(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddResetContent(data, message);
		}

		/// <summary>
		/// Tells the user agent to reset the document which sent this request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ResetContentNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddResetContent(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddResetContent(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.ResetContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddResetContentNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.ResetContent, message);
		}



		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the
		/// request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PartialContent(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddPartialContent(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the
		/// request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PartialContentNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddPartialContent(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPartialContent(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.PartialContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPartialContentNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.PartialContent, message);
		}



		/// <summary>
		/// Conveys information about multiple resources, for situations where multiple status codes
		/// might be appropriate.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MultiStatus(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddMultiStatus(data, message);
		}

		/// <summary>
		/// Conveys information about multiple resources, for situations where multiple status codes
		/// might be appropriate.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MultiStatusNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddMultiStatus(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMultiStatus(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeMultiStatus, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMultiStatusNoData(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeMultiStatus, message);
		}



		/// <summary>
		/// Used inside a <![CDATA[<dav:propstat>]]> response element to avoid repeatedly enumerating the
		/// internal members of multiple bindings to the same collection.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> AlreadyReported(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddAlreadyReported(data, message);
		}

		/// <summary>
		/// Used inside a <![CDATA[<dav:propstat>]]> response element to avoid repeatedly enumerating the
		/// internal members of multiple bindings to the same collection.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult AlreadyReportedNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddAlreadyReported(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddAlreadyReported(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeAlreadyReported, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddAlreadyReportedNoData(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeAlreadyReported, message);
		}



		/// <summary>
		/// The server has fulfilled a GET request for the resource, and the response is a representation of
		/// the result of one or more instance-manipulations applied to the current instance.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ImUsed(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddImUsed(data, message);
		}

		/// <summary>
		/// The server has fulfilled a GET request for the resource, and the response is a representation of
		/// the result of one or more instance-manipulations applied to the current instance.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ImUsedNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddImUsed(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddImUsed(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeImUsed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddImUsedNoData(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeImUsed, message);
		}

		#endregion

		#region Redirection

		/// <summary>
		/// Request has more than one possible response. The user agent or user should choose one of them.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MultipleChoices(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddMultipleChoices(data, message);
		}

		/// <summary>
		/// Request has more than one possible response. The user agent or user should choose one of them.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MultipleChoicesNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddMultipleChoices(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMultipleChoices(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.MultipleChoices, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMultipleChoicesNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.MultipleChoices, message);
		}



		/// <summary>
		/// URL of the requested resource has been changed permanently. The new URL is given in the response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MovedPermanently(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddMovedPermanently(data, message);
		}

		/// <summary>
		/// URL of the requested resource has been changed permanently. The new URL is given in the response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MovedPermanentlyNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddMovedPermanently(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMovedPermanently(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.MovedPermanently, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMovedPermanentlyNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.MovedPermanently, message);
		}



		/// <summary>
		/// URI of requested resource has been changed temporarily. Further changes in the URI might be made
		/// in the future. Therefore, this same URI should be used by the client in future requests.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Found(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddFound(data, message);
		}

		/// <summary>
		/// URI of requested resource has been changed temporarily. Further changes in the URI might be made
		/// in the future. Therefore, this same URI should be used by the client in future requests.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult FoundNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddFound(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddFound(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Found, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddFoundNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Found, message);
		}



		/// <summary>
		/// Server sent this response to direct the client to get the requested resource at another
		/// URI with a GET request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> SeeOther(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddSeeOther(data, message);
		}

		/// <summary>
		/// Server sent this response to direct the client to get the requested resource at another
		/// URI with a GET request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult SeeOtherNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddSeeOther(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddSeeOther(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.SeeOther, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddSeeOtherNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.SeeOther, message);
		}



		/// <summary>
		/// Used for caching purposes. It tells the client that the response has not been modified,
		/// so the client can continue to use the same cached version of the response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotModified(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddNotModified(data, message);
		}

		/// <summary>
		/// Used for caching purposes. It tells the client that the response has not been modified,
		/// so the client can continue to use the same cached version of the response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotModifiedNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddNotModified(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotModified(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NotModified, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotModifiedNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NotModified, message);
		}



		/// <summary>
		/// Server sends this response to direct the client to get the requested resource at another
		/// URI with the same method that was used in the prior request. This has the same semantics
		/// as the 302 Found HTTP response code, with the exception that the user agent must not change
		/// the HTTP method used: if a POST was used in the first request, a POST must be used in the
		/// second request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> TemporaryRedirect(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddTemporaryRedirect(data, message);
		}

		/// <summary>
		/// Server sends this response to direct the client to get the requested resource at another
		/// URI with the same method that was used in the prior request. This has the same semantics
		/// as the 302 Found HTTP response code, with the exception that the user agent must not change
		/// the HTTP method used: if a POST was used in the first request, a POST must be used in the
		/// second request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult TemporaryRedirectNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddTemporaryRedirect(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddTemporaryRedirect(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.TemporaryRedirect, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddTemporaryRedirectNoData(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.TemporaryRedirect, message);
		}



		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PermanentRedirect(TData? data = default, string? message = null)
		{
			return new Result<TData>().AddPermanentRedirect(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PermanentRedirectNoData(string? message = null)
		{
			return (IResult)new ResultDataless().AddPermanentRedirect(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPermanentRedirect(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodePermanentRedirect, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPermanentRedirectNoData(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodePermanentRedirect, message);
		}

		#endregion

		#region Error

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/></param>
		/// <param name="statusCode">HTTP error status code. Defaults to 500.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Error(Exception exception, int statusCode = 500, TData? data = default)
		{
			return Custom(data, statusCode, exception.ToString());
		}

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/></param>
		/// <param name="statusCode">HTTP error status code. Defaults to 500.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ErrorNoData(Exception exception, int statusCode = 500)
		{
			return CustomNoData(statusCode, exception.ToString());
		}

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Error(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddError(message, data);
		}

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ErrorNoData(string? message = null)
		{
			return new Result().AddErrorNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddError(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddErrorNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.InternalServerError, message);
		}

		#region Client

		/// <summary>
		/// Server cannot or will not process the request due to something that is
		/// perceived to be a client error (e.g., malformed request syntax,
		/// invalid request message framing, or deceptive request routing).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> BadRequest(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddBadRequest(message, data);
		}

		/// <summary>
		/// Server cannot or will not process the request due to something that is
		/// perceived to be a client error (e.g., malformed request syntax,
		/// invalid request message framing, or deceptive request routing).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult BadRequestNoData(string? message = null)
		{
			return new Result().AddBadRequestNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddBadRequest(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.BadRequest, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddBadRequestNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.BadRequest, message);
		}



		/// <summary>
		/// Although the HTTP standard specifies "unauthorized", semantically
		/// this response means "unauthenticated". That is, the client must
		/// authenticate itself to get the requested response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Unauthorized(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddUnauthorized(message, data);
		}

		/// <summary>
		/// Although the HTTP standard specifies "unauthorized", semantically
		/// this response means "unauthenticated". That is, the client must
		/// authenticate itself to get the requested response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UnauthorizedNoData(string? message = null)
		{
			return new Result().AddUnauthorizedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnauthorized(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.Unauthorized, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnauthorizedNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.Unauthorized, message);
		}



		/// <summary>
		/// Is reserved for future use. The initial aim for creating this code was using it
		/// for digital payment systems, however this status code is used very rarely and
		/// no standard convention exists.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PaymentRequired(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddPaymentRequired(message, data);
		}

		/// <summary>
		/// Is reserved for future use. The initial aim for creating this code was using it
		/// for digital payment systems, however this status code is used very rarely and
		/// no standard convention exists.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PaymentRequiredNoData(string? message = null)
		{
			return new Result().AddPaymentRequiredNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPaymentRequired(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.PaymentRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPaymentRequiredNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.PaymentRequired, message);
		}



		/// <summary>
		/// Client does not have access rights to the content; that is, it is unauthorized,
		/// so the server is refusing to give the requested resource. Unlike 401 Unauthorized,
		/// the client's identity is known to the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Forbidden(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddForbidden(message, data);
		}

		/// <summary>
		/// Client does not have access rights to the content; that is, it is unauthorized,
		/// so the server is refusing to give the requested resource. Unlike 401 Unauthorized,
		/// the client's identity is known to the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ForbiddenNoData(string? message = null)
		{
			return new Result().AddForbiddenNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddForbidden(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.Forbidden, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddForbiddenNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.Forbidden, message);
		}



		/// <summary>
		/// Something isn't found.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotFound(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddNotFound(message, data);
		}

		/// <summary>
		/// Something isn't found.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotFoundNoData(string? message = null)
		{
			return new Result().AddNotFoundNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotFound(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.NotFound, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotFoundNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.NotFound, message);
		}



		/// <summary>
		/// Method is known by the server but is not supported by the target
		/// resource. For example, an API may not allow calling DELETE to
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MethodNotAllowed(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddMethodNotAllowed(message, data);
		}

		/// <summary>
		/// Method is known by the server but is not supported by the target
		/// resource. For example, an API may not allow calling DELETE to
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MethodNotAllowedNoData(string? message = null)
		{
			return new Result().AddMethodNotAllowedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMethodNotAllowed(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.MethodNotAllowed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMethodNotAllowedNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.MethodNotAllowed, message);
		}



		/// <summary>
		/// Is sent when the web server, after performing server-driven content
		/// negotiation, doesn't find any content that conforms to the criteria
		/// given by the user agent.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotAcceptable(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddNotAcceptable(message, data);
		}

		/// <summary>
		/// Is sent when the web server, after performing server-driven content
		/// negotiation, doesn't find any content that conforms to the criteria
		/// given by the user agent.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotAcceptableNoData(string? message = null)
		{
			return new Result().AddNotAcceptableNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotAcceptable(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.NotAcceptable, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotAcceptableNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.NotAcceptable, message);
		}



		/// <summary>
		/// This is similar to <see cref="AddUnauthorized"/> but
		/// authentication is needed to be done by a proxy.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ProxyAuthenticationRequired(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddProxyAuthenticationRequired(message, data);
		}

		/// <summary>
		/// This is similar to <see cref="AddUnauthorized"/> but
		/// authentication is needed to be done by a proxy.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ProxyAuthenticationRequiredNoData(string? message = null)
		{
			return new Result().AddProxyAuthenticationRequiredNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddProxyAuthenticationRequired(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.ProxyAuthenticationRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddProxyAuthenticationRequiredNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.ProxyAuthenticationRequired, message);
		}



		/// <summary>
		/// Is sent on an idle connection by some servers, even without any previous
		/// request by the client. It means that the server would like to shut down
		/// this unused connection. This response is used much more since some browsers,
		/// like Chrome, Firefox 27+, or IE9, use HTTP pre-connection mechanisms to
		/// speed up surfing. Also note that some servers merely shut down the
		/// connection without sending this message.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> RequestTimeout(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddRequestTimeout(message, data);
		}

		/// <summary>
		/// Is sent on an idle connection by some servers, even without any previous
		/// request by the client. It means that the server would like to shut down
		/// this unused connection. This response is used much more since some browsers,
		/// like Chrome, Firefox 27+, or IE9, use HTTP pre-connection mechanisms to
		/// speed up surfing. Also note that some servers merely shut down the
		/// connection without sending this message.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult RequestTimeoutNoData(string? message = null)
		{
			return new Result().AddRequestTimeoutNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddRequestTimeout(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.RequestTimeout, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddRequestTimeoutNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.RequestTimeout, message);
		}



		/// <summary>
		/// This response is sent when a request conflicts with the current state
		/// of the server. Example: a database concurrency conflict occurred.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Conflict(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddConflict(message, data);
		}

		/// <summary>
		/// This response is sent when a request conflicts with the current state
		/// of the server. Example: a database concurrency conflict occurred.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ConflictNoData(string? message = null)
		{
			return new Result().AddConflictNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddConflict(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.Conflict, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddConflictNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.Conflict, message);
		}



		/// <summary>
		/// Is sent when the requested content has been permanently deleted from server,
		/// with no forwarding address. Clients are expected to remove their caches and
		/// links to the resource. The HTTP specification intends this status code to
		/// be used for "limited-time, promotional services". APIs should not feel
		/// compelled to indicate resources that have been deleted with this status
		/// code.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Gone(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddGone(message, data);
		}

		/// <summary>
		/// Is sent when the requested content has been permanently deleted from server,
		/// with no forwarding address. Clients are expected to remove their caches and
		/// links to the resource. The HTTP specification intends this status code to
		/// be used for "limited-time, promotional services". APIs should not feel
		/// compelled to indicate resources that have been deleted with this status
		/// code.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult GoneNoData(string? message = null)
		{
			return new Result().AddGoneNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddGone(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.Gone, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddGoneNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.Gone, message);
		}



		/// <summary>
		/// Request rejected because the Content-Length header field is not
		/// defined and the server requires it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> LengthRequired(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddLengthRequired(message, data);
		}

		/// <summary>
		/// Request rejected because the Content-Length header field is not
		/// defined and the server requires it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult LengthRequiredNoData(string? message = null)
		{
			return new Result().AddLengthRequiredNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddLengthRequired(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.LengthRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddLengthRequiredNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.LengthRequired, message);
		}



		/// <summary>
		/// Client has indicated preconditions in its headers which the server
		/// does not meet.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PreconditionFailed(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddPreconditionFailed(message, data);
		}

		/// <summary>
		/// Client has indicated preconditions in its headers which the server
		/// does not meet.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PreconditionFailedNoData(string? message = null)
		{
			return new Result().AddPreconditionFailedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPreconditionFailed(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.PreconditionFailed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPreconditionFailedNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.PreconditionFailed, message);
		}



		/// <summary>
		/// Request entity is larger than limits defined by server. The server
		/// might close the connection or return an Retry-After header field.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PayloadTooLarge(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddPayloadTooLarge(message, data);
		}

		/// <summary>
		/// Request entity is larger than limits defined by server. The server
		/// might close the connection or return an Retry-After header field.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PayloadTooLargeNoData(string? message = null)
		{
			return new Result().AddPayloadTooLargeNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPayloadTooLarge(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodePayloadTooLarge, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPayloadTooLargeNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodePayloadTooLarge, message);
		}



		/// <summary>
		/// URI is longer than the server is willing to interpret.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UriTooLong(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddUriTooLong(message, data);
		}

		/// <summary>
		/// URI is longer than the server is willing to interpret.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UriTooLongNoData(string? message = null)
		{
			return new Result().AddUriTooLongNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUriTooLong(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeUriTooLong, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUriTooLongNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeUriTooLong, message);
		}



		/// <summary>
		/// Media format of the requested data is not supported by the server, so the server is rejecting the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UnsupportedMediaType(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddUnsupportedMediaType(message, data);
		}

		/// <summary>
		/// Media format of the requested data is not supported by the server, so the server is rejecting the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UnsupportedMediaTypeNoData(string? message = null)
		{
			return new Result().AddUnsupportedMediaTypeNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnsupportedMediaType(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.UnsupportedMediaType, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnsupportedMediaTypeNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.UnsupportedMediaType, message);
		}



		/// <summary>
		/// Range specified by the Range header field in the request cannot be fulfilled.
		/// It's possible that the range is outside the size of the target URI's data.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> RangeNotSatisfiable(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddRangeNotSatisfiable(message, data);
		}


		/// <summary>
		/// Range specified by the Range header field in the request cannot be fulfilled.
		/// It's possible that the range is outside the size of the target URI's data.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult RangeNotSatisfiableNoData(string? message = null)
		{
			return new Result().AddRangeNotSatisfiableNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddRangeNotSatisfiable(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeRangeNotSatisfiable, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddRangeNotSatisfiableNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeRangeNotSatisfiable, message);
		}



		/// <summary>
		/// Expectation indicated by the Expect request header field cannot be met by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ExpectationFailed(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddExpectationFailed(message, data);
		}

		/// <summary>
		/// Expectation indicated by the Expect request header field cannot be met by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ExpectationFailedNoData(string? message = null)
		{
			return new Result().AddExpectationFailedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddExpectationFailed(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.ExpectationFailed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddExpectationFailedNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.ExpectationFailed, message);
		}



		/// <summary>
		/// Server refuses the attempt to brew coffee with a teapot.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ImATeapot(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddImATeapot(message, data);
		}

		/// <summary>
		/// Server refuses the attempt to brew coffee with a teapot.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ImATeapotNoData(string? message = null)
		{
			return new Result().AddImATeapotNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddImATeapot(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeImATeapot, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddImATeapotNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeImATeapot, message);
		}



		/// <summary>
		/// Request was directed at a server that is not able to produce a response.
		/// This can be sent by a server that is not configured to produce responses
		/// for the combination of scheme and authority that are included in the
		/// request URI.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MisdirectedRequest(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddMisdirectedRequest(message, data);
		}

		/// <summary>
		/// Request was directed at a server that is not able to produce a response.
		/// This can be sent by a server that is not configured to produce responses
		/// for the combination of scheme and authority that are included in the
		/// request URI.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MisdirectedRequestNoData(string? message = null)
		{
			return new Result().AddMisdirectedRequestNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMisdirectedRequest(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeMisdirectedRequest, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMisdirectedRequestNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeMisdirectedRequest, message);
		}



		/// <summary>
		/// Request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UnprocessableContent(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddUnprocessableContent(message, data);
		}

		/// <summary>
		/// Request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UnprocessableContentNoData(string? message = null)
		{
			return new Result().AddUnprocessableContentNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnprocessableContent(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeUnprocessableContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnprocessableContentNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeUnprocessableContent, message);
		}



		/// <summary>
		/// Resource that is being accessed is locked.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Locked(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddLocked(message, data);
		}

		/// <summary>
		/// Resource that is being accessed is locked.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult LockedNoData(string? message = null)
		{
			return new Result().AddLockedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddLocked(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeLocked, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddLockedNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeLocked, message);
		}



		/// <summary>
		/// Request failed due to failure of a previous request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> FailedDependency(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddFailedDependency(message, data);
		}

		/// <summary>
		/// Request failed due to failure of a previous request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult FailedDependencyNoData(string? message = null)
		{
			return new Result().AddFailedDependencyNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddFailedDependency(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeFailedDependency, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddFailedDependencyNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeFailedDependency, message);
		}



		/// <summary>
		/// Server is unwilling to risk processing a request that might be replayed.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> TooEarly(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddTooEarly(message, data);
		}

		/// <summary>
		/// Server is unwilling to risk processing a request that might be replayed.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult TooEarlyNoData(string? message = null)
		{
			return new Result().AddTooEarlyNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddTooEarly(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeTooEarly, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddTooEarlyNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeTooEarly, message);
		}



		/// <summary>
		/// Server refuses to perform the request using the current protocol but might
		/// be willing to do so after the client upgrades to a different protocol. The
		/// server sends an Upgrade header in a 426 response to indicate the required
		/// protocol(s).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UpgradeRequired(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddUpgradeRequired(message, data);
		}

		/// <summary>
		/// Server refuses to perform the request using the current protocol but might
		/// be willing to do so after the client upgrades to a different protocol. The
		/// server sends an Upgrade header in a 426 response to indicate the required
		/// protocol(s).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UpgradeRequiredNoData(string? message = null)
		{
			return new Result().AddUpgradeRequiredNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUpgradeRequired(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.UpgradeRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUpgradeRequiredNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.UpgradeRequired, message);
		}



		/// <summary>
		/// Origin server requires the request to be conditional. This response is intended
		/// to prevent the 'lost update' problem, where a client GETs a resource's state,
		/// modifies it and PUTs it back to the server, when meanwhile a third party has
		/// modified the state on the server, leading to a conflict.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PreconditionRequired(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddPreconditionRequired(message, data);
		}

		/// <summary>
		/// Origin server requires the request to be conditional. This response is intended
		/// to prevent the 'lost update' problem, where a client GETs a resource's state,
		/// modifies it and PUTs it back to the server, when meanwhile a third party has
		/// modified the state on the server, leading to a conflict.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PreconditionRequiredNoData(string? message = null)
		{
			return new Result().AddPreconditionRequiredNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPreconditionRequired(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodePreconditionRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPreconditionRequiredNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodePreconditionRequired, message);
		}



		/// <summary>
		/// Received too many requests in a given amount of time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> TooManyRequests(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddTooManyRequests(message, data);
		}

		/// <summary>
		/// Received too many requests in a given amount of time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult TooManyRequestsNoData(string? message = null)
		{
			return new Result().AddTooManyRequestsNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddTooManyRequests(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeTooManyRequests, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddTooManyRequestsNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeTooManyRequests, message);
		}



		/// <summary>
		/// Server is unwilling to process the request because its header fields
		/// are too large. The request may be resubmitted after reducing the size
		/// of the request header fields.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> RequestHeaderFieldsTooLarge(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddRequestHeaderFieldsTooLarge(message, data);
		}

		/// <summary>
		/// Server is unwilling to process the request because its header fields
		/// are too large. The request may be resubmitted after reducing the size
		/// of the request header fields.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult RequestHeaderFieldsTooLargeNoData(string? message = null)
		{
			return new Result().AddRequestHeaderFieldsTooLargeNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddRequestHeaderFieldsTooLarge(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeRequestHeaderFieldsTooLarge, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddRequestHeaderFieldsTooLargeNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeRequestHeaderFieldsTooLarge, message);
		}



		/// <summary>
		/// User agent requested a resource that cannot legally be provided,
		/// such as a web page censored by a government.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UnavailableForLegalReasons(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddUnavailableForLegalReasons(message, data);
		}

		/// <summary>
		/// User agent requested a resource that cannot legally be provided,
		/// such as a web page censored by a government.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UnavailableForLegalReasonsNoData(string? message = null)
		{
			return new Result().AddUnavailableForLegalReasonsNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnavailableForLegalReasons(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeUnavailableForLegalReasons, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnavailableForLegalReasonsNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeUnavailableForLegalReasons, message);
		}

		#endregion

		#region Server

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/></param>
		/// <param name="statusCode">HTTP error status code. Defaults to 500.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> InternalServerError(Exception exception, int statusCode = 500, TData? data = default)
		{
			return InternalServerError(exception.ToString(), data);
		}

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="exception"><see cref="Exception"/></param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult InternalServerErrorNoData(Exception exception)
		{
			return InternalServerErrorNoData(exception.ToString());
		}

		/// <summary>
		/// Server has encountered a situation it does not know how to handle.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> InternalServerError(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddInternalServerError(message, data);
		}

		/// <summary>
		/// Server has encountered a situation it does not know how to handle.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult InternalServerErrorNoData(string? message = null)
		{
			return new Result().AddInternalServerErrorNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddInternalServerError(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddInternalServerErrorNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.InternalServerError, message);
		}



		/// <summary>
		/// No records affected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NoRecordsAffected(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddNoRecordsAffected(message, data);
		}

		/// <summary>
		/// No records affected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NoRecordsAffectedNoData(string? message = null)
		{
			return new Result().AddNoRecordsAffectedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNoRecordsAffected(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNoRecordsAffectedNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.InternalServerError, message);
		}



		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotImplemented(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddNotImplemented(message, data);
		}

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotImplementedNoData(string? message = null)
		{
			return new Result().AddNotImplementedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotImplemented(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.NotImplemented, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotImplementedNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.NotImplemented, message);
		}



		/// <summary>
		/// Server, while working as a gateway to get a response needed to
		/// handle the request, got an invalid response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> BadGateway(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddBadGateway(message, data);
		}

		/// <summary>
		/// Server, while working as a gateway to get a response needed to
		/// handle the request, got an invalid response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult BadGatewayNoData(string? message = null)
		{
			return new Result().AddBadGatewayNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddBadGateway(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.BadGateway, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddBadGatewayNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.BadGateway, message);
		}



		/// <summary>
		/// Server is not ready to handle the request. Common causes are a server that is down
		/// for maintenance or that is overloaded. Note that together with this response, a
		/// user-friendly page explaining the problem should be sent. This response should be
		/// used for temporary conditions and the Retry-After HTTP header should, if possible,
		/// contain the estimated time before the recovery of the service. The webmaster must
		/// also take care about the caching-related headers that are sent along with this
		/// response, as these temporary condition responses should usually not be cached.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ServiceUnavailable(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddServiceUnavailable(message, data);
		}

		/// <summary>
		/// Server is not ready to handle the request. Common causes are a server that is down
		/// for maintenance or that is overloaded. Note that together with this response, a
		/// user-friendly page explaining the problem should be sent. This response should be
		/// used for temporary conditions and the Retry-After HTTP header should, if possible,
		/// contain the estimated time before the recovery of the service. The webmaster must
		/// also take care about the caching-related headers that are sent along with this
		/// response, as these temporary condition responses should usually not be cached.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ServiceUnavailableNoData(string? message = null)
		{
			return new Result().AddServiceUnavailableNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddServiceUnavailable(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.ServiceUnavailable, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddServiceUnavailableNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.ServiceUnavailable, message);
		}



		/// <summary>
		/// Server is acting as a gateway and cannot get a response in time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> GatewayTimeout(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddGatewayTimeout(message, data);
		}

		/// <summary>
		/// Server is acting as a gateway and cannot get a response in time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult GatewayTimeoutNoData(string? message = null)
		{
			return new Result().AddGatewayTimeoutNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddGatewayTimeout(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.GatewayTimeout, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddGatewayTimeoutNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.GatewayTimeout, message);
		}



		/// <summary>
		/// HTTP version used in the request is not supported by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> HttpVersionNotSupported(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddHttpVersionNotSupported(message, data);
		}

		/// <summary>
		/// HTTP version used in the request is not supported by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult HttpVersionNotSupportedNoData(string? message = null)
		{
			return new Result().AddHttpVersionNotSupportedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddHttpVersionNotSupported(string? message = null, TData? data = default)
		{
			return AddCustom(data, (int)HttpStatusCode.HttpVersionNotSupported, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddHttpVersionNotSupportedNoData(string? message = null)
		{
			return AddCustomNoData((int)HttpStatusCode.HttpVersionNotSupported, message);
		}



		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> VariantAlsoNegotiates(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddVariantAlsoNegotiates(message, data);
		}

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult VariantAlsoNegotiatesNoData(string? message = null)
		{
			return new Result().AddVariantAlsoNegotiatesNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddVariantAlsoNegotiates(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeVariantAlsoNegotiates, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddVariantAlsoNegotiatesNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeVariantAlsoNegotiates, message);
		}



		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ConfigurationError(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddConfigurationError(message, data);
		}

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ConfigurationErrorNoData(string? message = null)
		{
			return new Result().AddConfigurationErrorNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddConfigurationError(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeVariantAlsoNegotiates, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddConfigurationErrorNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeVariantAlsoNegotiates, message);
		}



		/// <summary>
		/// Server is unable to store the representation needed to successfully complete the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> InsufficientStorage(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddInsufficientStorage(message, data);
		}

		/// <summary>
		/// Server is unable to store the representation needed to successfully complete the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult InsufficientStorageNoData(string? message = null)
		{
			return new Result().AddInsufficientStorageNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddInsufficientStorage(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeInsufficientStorage, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddInsufficientStorageNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeInsufficientStorage, message);
		}



		/// <summary>
		/// Infinite loop detected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> LoopDetected(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddLoopDetected(message, data);
		}

		/// <summary>
		/// Infinite loop detected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult LoopDetectedNoData(string? message = null)
		{
			return new Result().AddLoopDetectedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddLoopDetected(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeLoopDetected, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddLoopDetectedNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeLoopDetected, message);
		}



		/// <summary>
		/// Further extensions to the request are required for the server to fulfill it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotExtended(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddNotExtended(message, data);
		}

		/// <summary>
		/// Further extensions to the request are required for the server to fulfill it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotExtendedNoData(string? message = null)
		{
			return new Result().AddNotExtendedNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotExtended(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeNotExtended, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotExtendedNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeNotExtended, message);
		}



		/// <summary>
		/// Client needs to authenticate to gain network access.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <param name="data">Optional data.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NetworkAuthenticationRequired(string? message = null, TData? data = default)
		{
			return new Result<TData>().AddNetworkAuthenticationRequired(message, data);
		}

		/// <summary>
		/// Client needs to authenticate to gain network access.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NetworkAuthenticationRequiredNoData(string? message = null)
		{
			return new Result().AddNetworkAuthenticationRequiredNoData(message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNetworkAuthenticationRequired(string? message = null, TData? data = default)
		{
			return AddCustom(data, HttpStatusCodeNetworkAuthenticationRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNetworkAuthenticationRequiredNoData(string? message = null)
		{
			return AddCustomNoData(HttpStatusCodeNetworkAuthenticationRequired, message);
		}

		#endregion

		#endregion
	}
}
