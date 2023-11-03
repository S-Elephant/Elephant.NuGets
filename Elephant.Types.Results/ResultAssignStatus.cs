using System.Diagnostics.CodeAnalysis;
using System.Net;
using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results
{
	/// <inheritdoc cref="IResult{TData}"/>
	[SuppressMessage("Microsoft.StyleCop.CSharp.OrderingRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic and non-generic version belong together.")]
	public partial class Result<TData> : IResult<TData>
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
		public static IResult Custom(int httpStatusCode, string? message = null)
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
		public virtual IResult AddCustom(int httpStatusCode, string? message = null)
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
		public static IResult<TData> Continue(TData? data, string? message = null)
		{
			return new Result<TData>().AddContinue(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Continue(string? message = null)
		{
			return (IResult)new ResultDataless().AddContinue(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddContinue(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Continue, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddContinue(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Continue, message);
		}



		/// <summary>
		/// In response to an Upgrade request header from the client and indicates the protocol the server is switching to.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> SwitchingProtocols(TData? data, string? message = null)
		{
			return new Result<TData>().AddSwitchingProtocols(data, message);
		}

		/// <summary>
		/// In response to an Upgrade request header from the client and indicates the protocol the server is switching to.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult SwitchingProtocols(string? message = null)
		{
			return (IResult)new ResultDataless().AddSwitchingProtocols(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddSwitchingProtocols(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.SwitchingProtocols, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddSwitchingProtocols(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.SwitchingProtocols, message);
		}



		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Processing(TData? data, string? message = null)
		{
			return new Result<TData>().AddProcessing(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Processing(string? message = null)
		{
			return (IResult)new ResultDataless().AddProcessing(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddProcessing(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeProcessing, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddProcessing(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeProcessing, message);
		}



		/// <summary>
		/// Is primarily intended to be used with the Link header, letting the user agent start preloading resources while the server prepares a response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> EarlyHints(TData? data, string? message = null)
		{
			return new Result<TData>().AddEarlyHints(data, message);
		}

		/// <summary>
		/// Is primarily intended to be used with the Link header, letting the user agent start preloading resources while the server prepares a response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult EarlyHints(string? message = null)
		{
			return (IResult)new ResultDataless().AddEarlyHints(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddEarlyHints(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeEarlyHints, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddEarlyHints(string? message = null)
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
		public static IResult<TData> Ok(TData? data, string? message = null)
		{
			return new Result<TData>().AddOk(data, message);
		}

		/// <summary>
		/// Success.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Ok(string? message = null)
		{
			return (IResult)new ResultDataless().AddOk(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddOk(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.OK, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddOk(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.OK, message);
		}



		/// <summary>
		/// Success and a new resource was created as a result.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Created(TData? data, string? message = null)
		{
			return new Result<TData>().AddCreated(data, message);
		}

		/// <summary>
		/// Success and a new resource was created as a result.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Created(string? message = null)
		{
			return (IResult)new ResultDataless().AddCreated(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddCreated(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Created, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddCreated(string? message = null)
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
		public static IResult<TData> Accepted(TData? data, string? message = null)
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
		public static IResult Accepted(string? message = null)
		{
			return (IResult)new ResultDataless().AddAccepted(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddAccepted(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Accepted, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddAccepted(string? message = null)
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
		public static IResult<TData> NonAuthoritativeInformation(TData? data, string? message = null)
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
		public static IResult NonAuthoritativeInformation(string? message = null)
		{
			return (IResult)new ResultDataless().AddNonAuthoritativeInformation(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNonAuthoritativeInformation(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NonAuthoritativeInformation, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNonAuthoritativeInformation(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NonAuthoritativeInformation, message);
		}



		/// <summary>
		/// Success, but there's no content.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NoContent(TData? data, string? message = null)
		{
			return new Result<TData>().AddNoContent(data, message);
		}

		/// <summary>
		/// Success, but there's no content.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NoContent(string? message = null)
		{
			return (IResult)new ResultDataless().AddNoContent(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNoContent(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NoContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNoContent(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NoContent, message);
		}



		/// <summary>
		/// Tells the user agent to reset the document which sent this request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ResetContent(TData? data, string? message = null)
		{
			return new Result<TData>().AddResetContent(data, message);
		}

		/// <summary>
		/// Tells the user agent to reset the document which sent this request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ResetContent(string? message = null)
		{
			return (IResult)new ResultDataless().AddResetContent(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddResetContent(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.ResetContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddResetContent(string? message = null)
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
		public static IResult<TData> PartialContent(TData? data, string? message = null)
		{
			return new Result<TData>().AddPartialContent(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the
		/// request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PartialContent(string? message = null)
		{
			return (IResult)new ResultDataless().AddPartialContent(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPartialContent(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.PartialContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPartialContent(string? message = null)
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
		public static IResult<TData> MultiStatus(TData? data, string? message = null)
		{
			return new Result<TData>().AddMultiStatus(data, message);
		}

		/// <summary>
		/// Conveys information about multiple resources, for situations where multiple status codes
		/// might be appropriate.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MultiStatus(string? message = null)
		{
			return (IResult)new ResultDataless().AddMultiStatus(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMultiStatus(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeMultiStatus, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMultiStatus(string? message = null)
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
		public static IResult<TData> AlreadyReported(TData? data, string? message = null)
		{
			return new Result<TData>().AddAlreadyReported(data, message);
		}

		/// <summary>
		/// Used inside a <![CDATA[<dav:propstat>]]> response element to avoid repeatedly enumerating the
		/// internal members of multiple bindings to the same collection.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult AlreadyReported(string? message = null)
		{
			return (IResult)new ResultDataless().AddAlreadyReported(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddAlreadyReported(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeAlreadyReported, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddAlreadyReported(string? message = null)
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
		public static IResult<TData> ImUsed(TData? data, string? message = null)
		{
			return new Result<TData>().AddImUsed(data, message);
		}

		/// <summary>
		/// The server has fulfilled a GET request for the resource, and the response is a representation of
		/// the result of one or more instance-manipulations applied to the current instance.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ImUsed(string? message = null)
		{
			return (IResult)new ResultDataless().AddImUsed(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddImUsed(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeImUsed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddImUsed(string? message = null)
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
		public static IResult<TData> MultipleChoices(TData? data, string? message = null)
		{
			return new Result<TData>().AddMultipleChoices(data, message);
		}

		/// <summary>
		/// Request has more than one possible response. The user agent or user should choose one of them.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MultipleChoices(string? message = null)
		{
			return (IResult)new ResultDataless().AddMultipleChoices(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMultipleChoices(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.MultipleChoices, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMultipleChoices(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.MultipleChoices, message);
		}



		/// <summary>
		/// URL of the requested resource has been changed permanently. The new URL is given in the response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MovedPermanently(TData? data, string? message = null)
		{
			return new Result<TData>().AddMovedPermanently(data, message);
		}

		/// <summary>
		/// URL of the requested resource has been changed permanently. The new URL is given in the response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MovedPermanently(string? message = null)
		{
			return (IResult)new ResultDataless().AddMovedPermanently(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMovedPermanently(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.MovedPermanently, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMovedPermanently(string? message = null)
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
		public static IResult<TData> Found(TData? data, string? message = null)
		{
			return new Result<TData>().AddFound(data, message);
		}

		/// <summary>
		/// URI of requested resource has been changed temporarily. Further changes in the URI might be made
		/// in the future. Therefore, this same URI should be used by the client in future requests.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Found(string? message = null)
		{
			return (IResult)new ResultDataless().AddFound(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddFound(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Found, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddFound(string? message = null)
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
		public static IResult<TData> SeeOther(TData? data, string? message = null)
		{
			return new Result<TData>().AddSeeOther(data, message);
		}

		/// <summary>
		/// Server sent this response to direct the client to get the requested resource at another
		/// URI with a GET request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult SeeOther(string? message = null)
		{
			return (IResult)new ResultDataless().AddSeeOther(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddSeeOther(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.SeeOther, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddSeeOther(string? message = null)
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
		public static IResult<TData> NotModified(TData? data, string? message = null)
		{
			return new Result<TData>().AddNotModified(data, message);
		}

		/// <summary>
		/// Used for caching purposes. It tells the client that the response has not been modified,
		/// so the client can continue to use the same cached version of the response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotModified(string? message = null)
		{
			return (IResult)new ResultDataless().AddNotModified(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotModified(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NotModified, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotModified(string? message = null)
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
		public static IResult<TData> TemporaryRedirect(TData? data, string? message = null)
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
		public static IResult TemporaryRedirect(string? message = null)
		{
			return (IResult)new ResultDataless().AddTemporaryRedirect(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddTemporaryRedirect(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.TemporaryRedirect, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddTemporaryRedirect(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.TemporaryRedirect, message);
		}



		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PermanentRedirect(TData? data, string? message = null)
		{
			return new Result<TData>().AddPermanentRedirect(data, message);
		}

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PermanentRedirect(string? message = null)
		{
			return (IResult)new ResultDataless().AddPermanentRedirect(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPermanentRedirect(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodePermanentRedirect, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPermanentRedirect(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodePermanentRedirect, message);
		}

		#endregion

		#region Error

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Error(TData? data, string? message = null)
		{
			return new Result<TData>().AddError(data, message);
		}

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Error(string? message = null)
		{
			return (IResult)new ResultDataless().AddError(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddError(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddError(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.InternalServerError, message);
		}

		#region Client

		/// <summary>
		/// Server cannot or will not process the request due to something that is
		/// perceived to be a client error (e.g., malformed request syntax,
		/// invalid request message framing, or deceptive request routing).
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> BadRequest(TData? data, string? message = null)
		{
			return new Result<TData>().AddBadRequest(data, message);
		}

		/// <summary>
		/// Server cannot or will not process the request due to something that is
		/// perceived to be a client error (e.g., malformed request syntax,
		/// invalid request message framing, or deceptive request routing).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult BadRequest(string? message = null)
		{
			return (IResult)new ResultDataless().AddBadRequest(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddBadRequest(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.BadRequest, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddBadRequest(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.BadRequest, message);
		}



		/// <summary>
		/// Although the HTTP standard specifies "unauthorized", semantically
		/// this response means "unauthenticated". That is, the client must
		/// authenticate itself to get the requested response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Unauthorized(TData? data, string? message = null)
		{
			return new Result<TData>().AddUnauthorized(data, message);
		}

		/// <summary>
		/// Although the HTTP standard specifies "unauthorized", semantically
		/// this response means "unauthenticated". That is, the client must
		/// authenticate itself to get the requested response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Unauthorized(string? message = null)
		{
			return (IResult)new ResultDataless().AddUnauthorized(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnauthorized(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Unauthorized, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnauthorized(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Unauthorized, message);
		}



		/// <summary>
		/// Is reserved for future use. The initial aim for creating this code was using it
		/// for digital payment systems, however this status code is used very rarely and
		/// no standard convention exists.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PaymentRequired(TData? data, string? message = null)
		{
			return new Result<TData>().AddPaymentRequired(data, message);
		}

		/// <summary>
		/// Is reserved for future use. The initial aim for creating this code was using it
		/// for digital payment systems, however this status code is used very rarely and
		/// no standard convention exists.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PaymentRequired(string? message = null)
		{
			return (IResult)new ResultDataless().AddPaymentRequired(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPaymentRequired(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.PaymentRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPaymentRequired(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.PaymentRequired, message);
		}



		/// <summary>
		/// Client does not have access rights to the content; that is, it is unauthorized,
		/// so the server is refusing to give the requested resource. Unlike 401 Unauthorized,
		/// the client's identity is known to the server.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Forbidden(TData? data, string? message = null)
		{
			return new Result<TData>().AddForbidden(data, message);
		}

		/// <summary>
		/// Client does not have access rights to the content; that is, it is unauthorized,
		/// so the server is refusing to give the requested resource. Unlike 401 Unauthorized,
		/// the client's identity is known to the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Forbidden(string? message = null)
		{
			return (IResult)new ResultDataless().AddForbidden(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddForbidden(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Forbidden, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddForbidden(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Forbidden, message);
		}



		/// <summary>
		/// Something isn't found.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotFound(TData? data, string? message = null)
		{
			return new Result<TData>().AddNotFound(data, message);
		}

		/// <summary>
		/// Something isn't found.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotFound(string? message = null)
		{
			return (IResult)new ResultDataless().AddNotFound(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotFound(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NotFound, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotFound(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NotFound, message);
		}



		/// <summary>
		/// Method is known by the server but is not supported by the target
		/// resource. For example, an API may not allow calling DELETE to
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MethodNotAllowed(TData? data, string? message = null)
		{
			return new Result<TData>().AddMethodNotAllowed(data, message);
		}

		/// <summary>
		/// Method is known by the server but is not supported by the target
		/// resource. For example, an API may not allow calling DELETE to
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MethodNotAllowed(string? message = null)
		{
			return (IResult)new ResultDataless().AddMethodNotAllowed(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMethodNotAllowed(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.MethodNotAllowed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMethodNotAllowed(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.MethodNotAllowed, message);
		}



		/// <summary>
		/// Is sent when the web server, after performing server-driven content
		/// negotiation, doesn't find any content that conforms to the criteria
		/// given by the user agent.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotAcceptable(TData? data, string? message = null)
		{
			return new Result<TData>().AddNotAcceptable(data, message);
		}

		/// <summary>
		/// Is sent when the web server, after performing server-driven content
		/// negotiation, doesn't find any content that conforms to the criteria
		/// given by the user agent.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotAcceptable(string? message = null)
		{
			return (IResult)new ResultDataless().AddNotAcceptable(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotAcceptable(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NotAcceptable, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotAcceptable(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NotAcceptable, message);
		}



		/// <summary>
		/// This is similar to <see cref="AddUnauthorized(TData?,string?)"/> but
		/// authentication is needed to be done by a proxy.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ProxyAuthenticationRequired(TData? data, string? message = null)
		{
			return new Result<TData>().AddProxyAuthenticationRequired(data, message);
		}

		/// <summary>
		/// This is similar to <see cref="AddUnauthorized(TData?,string?)"/> but
		/// authentication is needed to be done by a proxy.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ProxyAuthenticationRequired(string? message = null)
		{
			return (IResult)new ResultDataless().AddProxyAuthenticationRequired(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddProxyAuthenticationRequired(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.ProxyAuthenticationRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddProxyAuthenticationRequired(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.ProxyAuthenticationRequired, message);
		}



		/// <summary>
		/// Is sent on an idle connection by some servers, even without any previous
		/// request by the client. It means that the server would like to shut down
		/// this unused connection. This response is used much more since some browsers,
		/// like Chrome, Firefox 27+, or IE9, use HTTP pre-connection mechanisms to
		/// speed up surfing. Also note that some servers merely shut down the
		/// connection without sending this message.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> RequestTimeout(TData? data, string? message = null)
		{
			return new Result<TData>().AddRequestTimeout(data, message);
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
		public static IResult RequestTimeout(string? message = null)
		{
			return (IResult)new ResultDataless().AddRequestTimeout(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddRequestTimeout(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.RequestTimeout, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddRequestTimeout(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.RequestTimeout, message);
		}



		/// <summary>
		/// This response is sent when a request conflicts with the current state
		/// of the server. Example: a database concurrency conflict occurred.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Conflict(TData? data, string? message = null)
		{
			return new Result<TData>().AddConflict(data, message);
		}

		/// <summary>
		/// This response is sent when a request conflicts with the current state
		/// of the server. Example: a database concurrency conflict occurred.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Conflict(string? message = null)
		{
			return (IResult)new ResultDataless().AddConflict(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddConflict(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Conflict, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddConflict(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Conflict, message);
		}



		/// <summary>
		/// Is sent when the requested content has been permanently deleted from server,
		/// with no forwarding address. Clients are expected to remove their caches and
		/// links to the resource. The HTTP specification intends this status code to
		/// be used for "limited-time, promotional services". APIs should not feel
		/// compelled to indicate resources that have been deleted with this status
		/// code.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Gone(TData? data, string? message = null)
		{
			return new Result<TData>().AddGone(data, message);
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
		public static IResult Gone(string? message = null)
		{
			return (IResult)new ResultDataless().AddGone(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddGone(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.Gone, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddGone(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.Gone, message);
		}



		/// <summary>
		/// Request rejected because the Content-Length header field is not
		/// defined and the server requires it.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> LengthRequired(TData? data, string? message = null)
		{
			return new Result<TData>().AddLengthRequired(data, message);
		}

		/// <summary>
		/// Request rejected because the Content-Length header field is not
		/// defined and the server requires it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult LengthRequired(string? message = null)
		{
			return (IResult)new ResultDataless().AddLengthRequired(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddLengthRequired(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.LengthRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddLengthRequired(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.LengthRequired, message);
		}



		/// <summary>
		/// Client has indicated preconditions in its headers which the server
		/// does not meet.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PreconditionFailed(TData? data, string? message = null)
		{
			return new Result<TData>().AddPreconditionFailed(data, message);
		}

		/// <summary>
		/// Client has indicated preconditions in its headers which the server
		/// does not meet.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PreconditionFailed(string? message = null)
		{
			return (IResult)new ResultDataless().AddPreconditionFailed(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPreconditionFailed(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.PreconditionFailed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPreconditionFailed(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.PreconditionFailed, message);
		}



		/// <summary>
		/// Request entity is larger than limits defined by server. The server
		/// might close the connection or return an Retry-After header field.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PayloadTooLarge(TData? data, string? message = null)
		{
			return new Result<TData>().AddPayloadTooLarge(data, message);
		}

		/// <summary>
		/// Request entity is larger than limits defined by server. The server
		/// might close the connection or return an Retry-After header field.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PayloadTooLarge(string? message = null)
		{
			return (IResult)new ResultDataless().AddPayloadTooLarge(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPayloadTooLarge(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodePayloadTooLarge, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPayloadTooLarge(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodePayloadTooLarge, message);
		}



		/// <summary>
		/// URI is longer than the server is willing to interpret.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UriTooLong(TData? data, string? message = null)
		{
			return new Result<TData>().AddUriTooLong(data, message);
		}

		/// <summary>
		/// URI is longer than the server is willing to interpret.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UriTooLong(string? message = null)
		{
			return (IResult)new ResultDataless().AddUriTooLong(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUriTooLong(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeUriTooLong, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUriTooLong(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeUriTooLong, message);
		}



		/// <summary>
		/// Media format of the requested data is not supported by the server, so the server is rejecting the request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UnsupportedMediaType(TData? data, string? message = null)
		{
			return new Result<TData>().AddUnsupportedMediaType(data, message);
		}

		/// <summary>
		/// Media format of the requested data is not supported by the server, so the server is rejecting the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UnsupportedMediaType(string? message = null)
		{
			return (IResult)new ResultDataless().AddUnsupportedMediaType(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnsupportedMediaType(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.UnsupportedMediaType, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnsupportedMediaType(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.UnsupportedMediaType, message);
		}



		/// <summary>
		/// Range specified by the Range header field in the request cannot be fulfilled.
		/// It's possible that the range is outside the size of the target URI's data.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> RangeNotSatisfiable(TData? data, string? message = null)
		{
			return new Result<TData>().AddRangeNotSatisfiable(data, message);
		}

		/// <summary>
		/// Range specified by the Range header field in the request cannot be fulfilled.
		/// It's possible that the range is outside the size of the target URI's data.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult RangeNotSatisfiable(string? message = null)
		{
			return (IResult)new ResultDataless().AddRangeNotSatisfiable(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddRangeNotSatisfiable(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeRangeNotSatisfiable, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddRangeNotSatisfiable(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeRangeNotSatisfiable, message);
		}



		/// <summary>
		/// Expectation indicated by the Expect request header field cannot be met by the server.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ExpectationFailed(TData? data, string? message = null)
		{
			return new Result<TData>().AddExpectationFailed(data, message);
		}

		/// <summary>
		/// Expectation indicated by the Expect request header field cannot be met by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ExpectationFailed(string? message = null)
		{
			return (IResult)new ResultDataless().AddExpectationFailed(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddExpectationFailed(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.ExpectationFailed, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddExpectationFailed(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.ExpectationFailed, message);
		}



		/// <summary>
		/// Server refuses the attempt to brew coffee with a teapot.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ImATeapot(TData? data, string? message = null)
		{
			return new Result<TData>().AddImATeapot(data, message);
		}

		/// <summary>
		/// Server refuses the attempt to brew coffee with a teapot.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ImATeapot(string? message = null)
		{
			return (IResult)new ResultDataless().AddImATeapot(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddImATeapot(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeImATeapot, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddImATeapot(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeImATeapot, message);
		}



		/// <summary>
		/// Request was directed at a server that is not able to produce a response.
		/// This can be sent by a server that is not configured to produce responses
		/// for the combination of scheme and authority that are included in the
		/// request URI.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> MisdirectedRequest(TData? data, string? message = null)
		{
			return new Result<TData>().AddMisdirectedRequest(data, message);
		}

		/// <summary>
		/// Request was directed at a server that is not able to produce a response.
		/// This can be sent by a server that is not configured to produce responses
		/// for the combination of scheme and authority that are included in the
		/// request URI.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult MisdirectedRequest(string? message = null)
		{
			return (IResult)new ResultDataless().AddMisdirectedRequest(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddMisdirectedRequest(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeMisdirectedRequest, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddMisdirectedRequest(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeMisdirectedRequest, message);
		}



		/// <summary>
		/// Request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UnprocessableContent(TData? data, string? message = null)
		{
			return new Result<TData>().AddUnprocessableContent(data, message);
		}

		/// <summary>
		/// Request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UnprocessableContent(string? message = null)
		{
			return (IResult)new ResultDataless().AddUnprocessableContent(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnprocessableContent(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeUnprocessableContent, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnprocessableContent(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeUnprocessableContent, message);
		}



		/// <summary>
		/// Resource that is being accessed is locked.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> Locked(TData? data, string? message = null)
		{
			return new Result<TData>().AddLocked(data, message);
		}

		/// <summary>
		/// Resource that is being accessed is locked.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult Locked(string? message = null)
		{
			return (IResult)new ResultDataless().AddLocked(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddLocked(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeLocked, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddLocked(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeLocked, message);
		}



		/// <summary>
		/// Request failed due to failure of a previous request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> FailedDependency(TData? data, string? message = null)
		{
			return new Result<TData>().AddFailedDependency(data, message);
		}

		/// <summary>
		/// Request failed due to failure of a previous request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult FailedDependency(string? message = null)
		{
			return (IResult)new ResultDataless().AddFailedDependency(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddFailedDependency(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeFailedDependency, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddFailedDependency(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeFailedDependency, message);
		}



		/// <summary>
		/// Server is unwilling to risk processing a request that might be replayed.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> TooEarly(TData? data, string? message = null)
		{
			return new Result<TData>().AddTooEarly(data, message);
		}

		/// <summary>
		/// Server is unwilling to risk processing a request that might be replayed.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult TooEarly(string? message = null)
		{
			return (IResult)new ResultDataless().AddTooEarly(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddTooEarly(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeTooEarly, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddTooEarly(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeTooEarly, message);
		}



		/// <summary>
		/// Server refuses to perform the request using the current protocol but might
		/// be willing to do so after the client upgrades to a different protocol. The
		/// server sends an Upgrade header in a 426 response to indicate the required
		/// protocol(s).
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UpgradeRequired(TData? data, string? message = null)
		{
			return new Result<TData>().AddUpgradeRequired(data, message);
		}

		/// <summary>
		/// Server refuses to perform the request using the current protocol but might
		/// be willing to do so after the client upgrades to a different protocol. The
		/// server sends an Upgrade header in a 426 response to indicate the required
		/// protocol(s).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UpgradeRequired(string? message = null)
		{
			return (IResult)new ResultDataless().AddUpgradeRequired(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUpgradeRequired(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.UpgradeRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUpgradeRequired(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.UpgradeRequired, message);
		}



		/// <summary>
		/// Origin server requires the request to be conditional. This response is intended
		/// to prevent the 'lost update' problem, where a client GETs a resource's state,
		/// modifies it and PUTs it back to the server, when meanwhile a third party has
		/// modified the state on the server, leading to a conflict.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> PreconditionRequired(TData? data, string? message = null)
		{
			return new Result<TData>().AddPreconditionRequired(data, message);
		}

		/// <summary>
		/// Origin server requires the request to be conditional. This response is intended
		/// to prevent the 'lost update' problem, where a client GETs a resource's state,
		/// modifies it and PUTs it back to the server, when meanwhile a third party has
		/// modified the state on the server, leading to a conflict.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult PreconditionRequired(string? message = null)
		{
			return (IResult)new ResultDataless().AddPreconditionRequired(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddPreconditionRequired(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodePreconditionRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddPreconditionRequired(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodePreconditionRequired, message);
		}



		/// <summary>
		/// Received too many requests in a given amount of time.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> TooManyRequests(TData? data, string? message = null)
		{
			return new Result<TData>().AddTooManyRequests(data, message);
		}

		/// <summary>
		/// Received too many requests in a given amount of time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult TooManyRequests(string? message = null)
		{
			return (IResult)new ResultDataless().AddTooManyRequests(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddTooManyRequests(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeTooManyRequests, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddTooManyRequests(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeTooManyRequests, message);
		}



		/// <summary>
		/// Server is unwilling to process the request because its header fields
		/// are too large. The request may be resubmitted after reducing the size
		/// of the request header fields.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> RequestHeaderFieldsTooLarge(TData? data, string? message = null)
		{
			return new Result<TData>().AddRequestHeaderFieldsTooLarge(data, message);
		}

		/// <summary>
		/// Server is unwilling to process the request because its header fields
		/// are too large. The request may be resubmitted after reducing the size
		/// of the request header fields.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult RequestHeaderFieldsTooLarge(string? message = null)
		{
			return (IResult)new ResultDataless().AddRequestHeaderFieldsTooLarge(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddRequestHeaderFieldsTooLarge(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeRequestHeaderFieldsTooLarge, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddRequestHeaderFieldsTooLarge(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeRequestHeaderFieldsTooLarge, message);
		}



		/// <summary>
		/// User agent requested a resource that cannot legally be provided,
		/// such as a web page censored by a government.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> UnavailableForLegalReasons(TData? data, string? message = null)
		{
			return new Result<TData>().AddUnavailableForLegalReasons(data, message);
		}

		/// <summary>
		/// User agent requested a resource that cannot legally be provided,
		/// such as a web page censored by a government.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult UnavailableForLegalReasons(string? message = null)
		{
			return (IResult)new ResultDataless().AddUnavailableForLegalReasons(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddUnavailableForLegalReasons(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeUnavailableForLegalReasons, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddUnavailableForLegalReasons(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeUnavailableForLegalReasons, message);
		}

		#endregion

		#region Server

		/// <summary>
		/// Server has encountered a situation it does not know how to handle.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> InternalServerError(TData? data, string? message = null)
		{
			return new Result<TData>().AddInternalServerError(data, message);
		}

		/// <summary>
		/// Server has encountered a situation it does not know how to handle.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult InternalServerError(string? message = null)
		{
			return (IResult)new ResultDataless().AddInternalServerError(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddInternalServerError(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddInternalServerError(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.InternalServerError, message);
		}



		/// <summary>
		/// No records affected.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NoRecordsAffected(TData? data, string? message = null)
		{
			return new Result<TData>().AddNoRecordsAffected(data, message);
		}

		/// <summary>
		/// No records affected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NoRecordsAffected(string? message = null)
		{
			return (IResult)new ResultDataless().AddNoRecordsAffected(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNoRecordsAffected(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.InternalServerError, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNoRecordsAffected(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.InternalServerError, message);
		}



		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotImplemented(TData? data, string? message = null)
		{
			return new Result<TData>().AddNotImplemented(data, message);
		}

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotImplemented(string? message = null)
		{
			return (IResult)new ResultDataless().AddNotImplemented(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotImplemented(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.NotImplemented, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotImplemented(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.NotImplemented, message);
		}



		/// <summary>
		/// Server, while working as a gateway to get a response needed to
		/// handle the request, got an invalid response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> BadGateway(TData? data, string? message = null)
		{
			return new Result<TData>().AddBadGateway(data, message);
		}

		/// <summary>
		/// Server, while working as a gateway to get a response needed to
		/// handle the request, got an invalid response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult BadGateway(string? message = null)
		{
			return (IResult)new ResultDataless().AddBadGateway(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddBadGateway(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.BadGateway, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddBadGateway(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.BadGateway, message);
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
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ServiceUnavailable(TData? data, string? message = null)
		{
			return new Result<TData>().AddServiceUnavailable(data, message);
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
		public static IResult ServiceUnavailable(string? message = null)
		{
			return (IResult)new ResultDataless().AddServiceUnavailable(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddServiceUnavailable(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.ServiceUnavailable, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddServiceUnavailable(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.ServiceUnavailable, message);
		}



		/// <summary>
		/// Server is acting as a gateway and cannot get a response in time.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> GatewayTimeout(TData? data, string? message = null)
		{
			return new Result<TData>().AddGatewayTimeout(data, message);
		}

		/// <summary>
		/// Server is acting as a gateway and cannot get a response in time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult GatewayTimeout(string? message = null)
		{
			return (IResult)new ResultDataless().AddGatewayTimeout(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddGatewayTimeout(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.GatewayTimeout, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddGatewayTimeout(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.GatewayTimeout, message);
		}



		/// <summary>
		/// HTTP version used in the request is not supported by the server.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> HttpVersionNotSupported(TData? data, string? message = null)
		{
			return new Result<TData>().AddHttpVersionNotSupported(data, message);
		}

		/// <summary>
		/// HTTP version used in the request is not supported by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult HttpVersionNotSupported(string? message = null)
		{
			return (IResult)new ResultDataless().AddHttpVersionNotSupported(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddHttpVersionNotSupported(TData? data, string? message = null)
		{
			return AddCustom(data, (int)HttpStatusCode.HttpVersionNotSupported, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddHttpVersionNotSupported(string? message = null)
		{
			return (IResult)AddCustom(default, (int)HttpStatusCode.HttpVersionNotSupported, message);
		}



		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> VariantAlsoNegotiates(TData? data, string? message = null)
		{
			return new Result<TData>().AddVariantAlsoNegotiates(data, message);
		}

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult VariantAlsoNegotiates(string? message = null)
		{
			return (IResult)new ResultDataless().AddVariantAlsoNegotiates(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddVariantAlsoNegotiates(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeVariantAlsoNegotiates, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddVariantAlsoNegotiates(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeVariantAlsoNegotiates, message);
		}



		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> ConfigurationError(TData? data, string? message = null)
		{
			return new Result<TData>().AddConfigurationError(data, message);
		}

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult ConfigurationError(string? message = null)
		{
			return (IResult)new ResultDataless().AddConfigurationError(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddConfigurationError(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeVariantAlsoNegotiates, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddConfigurationError(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeVariantAlsoNegotiates, message);
		}



		/// <summary>
		/// Server is unable to store the representation needed to successfully complete the request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> InsufficientStorage(TData? data, string? message = null)
		{
			return new Result<TData>().AddInsufficientStorage(data, message);
		}

		/// <summary>
		/// Server is unable to store the representation needed to successfully complete the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult InsufficientStorage(string? message = null)
		{
			return (IResult)new ResultDataless().AddInsufficientStorage(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddInsufficientStorage(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeInsufficientStorage, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddInsufficientStorage(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeInsufficientStorage, message);
		}



		/// <summary>
		/// Infinite loop detected.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> LoopDetected(TData? data, string? message = null)
		{
			return new Result<TData>().AddLoopDetected(data, message);
		}

		/// <summary>
		/// Infinite loop detected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult LoopDetected(string? message = null)
		{
			return (IResult)new ResultDataless().AddLoopDetected(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddLoopDetected(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeLoopDetected, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddLoopDetected(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeLoopDetected, message);
		}



		/// <summary>
		/// Further extensions to the request are required for the server to fulfill it.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NotExtended(TData? data, string? message = null)
		{
			return new Result<TData>().AddNotExtended(data, message);
		}

		/// <summary>
		/// Further extensions to the request are required for the server to fulfill it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NotExtended(string? message = null)
		{
			return (IResult)new ResultDataless().AddNotExtended(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNotExtended(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeNotExtended, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNotExtended(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeNotExtended, message);
		}



		/// <summary>
		/// Client needs to authenticate to gain network access.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult<TData> NetworkAuthenticationRequired(TData? data, string? message = null)
		{
			return new Result<TData>().AddNetworkAuthenticationRequired(data, message);
		}

		/// <summary>
		/// Client needs to authenticate to gain network access.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		public static IResult NetworkAuthenticationRequired(string? message = null)
		{
			return (IResult)new ResultDataless().AddNetworkAuthenticationRequired(false, message);
		}

		/// <inheritdoc/>
		public virtual IResult<TData> AddNetworkAuthenticationRequired(TData? data, string? message = null)
		{
			return AddCustom(data, HttpStatusCodeNetworkAuthenticationRequired, message);
		}

		/// <inheritdoc/>
		public virtual IResult AddNetworkAuthenticationRequired(string? message = null)
		{
			return (IResult)AddCustom(default, HttpStatusCodeNetworkAuthenticationRequired, message);
		}

		#endregion

		#endregion
	}
}
