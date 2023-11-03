namespace Elephant.Types.Results.Abstractions
{
	/// <summary>
	/// Result wrapper for returning data along with a message and success status.
	/// </summary>
	/// <typeparam name="TData">Your data type.</typeparam>
	public interface IResult<TData>
	{
		/// <summary>
		/// The wrapped data. Default: default(T).
		/// </summary>
		TData? Data { get; }

		/// <summary>
		/// Indicates if the operations were successful.
		/// </summary>
		bool IsSuccess { get; }

		/// <summary>
		/// Indicates if the operations were unsuccessful.
		/// </summary>
		bool IsError { get; }

		/// <summary>
		/// Indicates if the operations were neither successful nor unsuccessful
		/// (thus, is informative, redirectional or custom).
		/// </summary>
		bool IsInformativeRedirectionOrCustom { get; }

		/// <summary>
		/// Your optional custom message.
		/// </summary>
		string? Message { get; }

		/// <summary>
		/// HTTP status code. Defaults to 200.
		/// </summary>
		int StatusCode { get; }

		/// <summary>
		/// If true then this Response Wrapper may use have useful <see cref="Data"/>. If false,
		/// it will NEVER have any useful <see cref="Data"/> and <see cref="Data"/> should be ignored.
		/// </summary>
		bool UsesData { get; }

		/// <summary>
		/// Add custom result.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="httpStatusCode">Custom HTTP status code.</param>
		/// <param name="message">Optional custom message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddCustom(TData? data, int httpStatusCode, string? message = null);

		/// <summary>
		/// Add custom result.
		/// </summary>
		/// <param name="httpStatusCode">Custom HTTP status code.</param>
		/// <param name="message">Optional custom message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddCustom(int httpStatusCode, string? message = null);

		#region Statuses

		#region Informational

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddContinue(TData? data, string? message = null);

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddContinue(string? message = null);

		/// <summary>
		/// In response to an Upgrade request header from the client and indicates the protocol the server is switching to.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddSwitchingProtocols(TData? data, string? message = null);

		/// <summary>
		/// In response to an Upgrade request header from the client and indicates the protocol the server is switching to.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddSwitchingProtocols(string? message = null);

		/// <summary>
		/// Indicates that the server has received and is processing the request, but no response is available yet.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddProcessing(TData? data, string? message = null);

		/// <summary>
		/// Indicates that the server has received and is processing the request, but no response is available yet.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddProcessing(string? message = null);

		/// <summary>
		/// Is primarily intended to be used with the Link header, letting the user agent start preloading
		/// resources while the server prepares a response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddEarlyHints(TData? data, string? message = null);

		/// <summary>
		/// Is primarily intended to be used with the Link header, letting the user agent start preloading
		/// resources while the server prepares a response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddEarlyHints(string? message = null);

		#endregion

		#region Success

		/// <summary>
		/// Success.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddOk(TData? data, string? message = null);

		/// <summary>
		/// Success.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddOk(string? message = null);

		/// <summary>
		/// Success and a new resource was created as a result.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddCreated(TData? data, string? message = null);

		/// <summary>
		/// Success and a new resource was created as a result.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddCreated(string? message = null);

		/// <summary>
		/// Request has been received but not yet acted upon. It is noncommittal, since there is no way in HTTP to later
		/// send an asynchronous response indicating the outcome of the request. It is intended for cases where another
		/// process or server handles the request, or for batch processing.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddAccepted(TData? data, string? message = null);

		/// <summary>
		/// Request has been received but not yet acted upon. It is noncommittal, since there is no way in HTTP to later
		/// send an asynchronous response indicating the outcome of the request. It is intended for cases where another
		/// process or server handles the request, or for batch processing.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddAccepted(string? message = null);

		/// <summary>
		/// Means the returned metadata is not exactly the same as is available from the origin server, but is collected
		/// from a local or a third-party copy. This is mostly used for mirrors or backups of another resource. Except for
		/// that specific case, the 200 OK response is preferred to this status.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNonAuthoritativeInformation(TData? data, string? message = null);

		/// <summary>
		/// Means the returned metadata is not exactly the same as is available from the origin server, but is collected
		/// from a local or a third-party copy. This is mostly used for mirrors or backups of another resource. Except for
		/// that specific case, the 200 OK response is preferred to this status.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNonAuthoritativeInformation(string? message = null);

		/// <summary>
		/// Success, but there's no content.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNoContent(TData? data, string? message = null);

		/// <summary>
		/// Success, but there's no content.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNoContent(string? message = null);

		/// <summary>
		/// Tells the user agent to reset the document which sent this request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddResetContent(TData? data, string? message = null);

		/// <summary>
		/// Tells the user agent to reset the document which sent this request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddResetContent(string? message = null);

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the
		/// request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddPartialContent(TData? data, string? message = null);

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the
		/// request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddPartialContent(string? message = null);

		/// <summary>
		/// Conveys information about multiple resources, for situations where multiple status codes
		/// might be appropriate.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddMultiStatus(TData? data, string? message = null);

		/// <summary>
		/// Conveys information about multiple resources, for situations where multiple status codes
		/// might be appropriate.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddMultiStatus(string? message = null);

		/// <summary>
		/// Used inside a <![CDATA[<dav:propstat>]]> response element to avoid repeatedly enumerating the
		/// internal members of multiple bindings to the same collection.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddAlreadyReported(TData? data, string? message = null);

		/// <summary>
		/// Used inside a <![CDATA[<dav:propstat>]]> response element to avoid repeatedly enumerating the
		/// internal members of multiple bindings to the same collection.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddAlreadyReported(string? message = null);

		/// <summary>
		/// The server has fulfilled a GET request for the resource, and the response is a representation of
		/// the result of one or more instance-manipulations applied to the current instance.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddImUsed(TData? data, string? message = null);

		/// <summary>
		/// The server has fulfilled a GET request for the resource, and the response is a representation of
		/// the result of one or more instance-manipulations applied to the current instance.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddImUsed(string? message = null);

		#endregion

		#region Redirection

		/// <summary>
		/// Request has more than one possible response. The user agent or user should choose one of them.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddMultipleChoices(TData? data, string? message = null);

		/// <summary>
		/// Request has more than one possible response. The user agent or user should choose one of them.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddMultipleChoices(string? message = null);

		/// <summary>
		/// URL of the requested resource has been changed permanently. The new URL is given in the response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddMovedPermanently(TData? data, string? message = null);

		/// <summary>
		/// URL of the requested resource has been changed permanently. The new URL is given in the response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddMovedPermanently(string? message = null);

		/// <summary>
		/// URI of requested resource has been changed temporarily. Further changes in the URI might be made
		/// in the future. Therefore, this same URI should be used by the client in future requests.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddFound(TData? data, string? message = null);

		/// <summary>
		/// URI of requested resource has been changed temporarily. Further changes in the URI might be made
		/// in the future. Therefore, this same URI should be used by the client in future requests.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddFound(string? message = null);

		/// <summary>
		/// Server sent this response to direct the client to get the requested resource at another
		/// URI with a GET request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddSeeOther(TData? data, string? message = null);

		/// <summary>
		/// Server sent this response to direct the client to get the requested resource at another
		/// URI with a GET request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddSeeOther(string? message = null);

		/// <summary>
		/// Used for caching purposes. It tells the client that the response has not been modified,
		/// so the client can continue to use the same cached version of the response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNotModified(TData? data, string? message = null);

		/// <summary>
		/// Used for caching purposes. It tells the client that the response has not been modified,
		/// so the client can continue to use the same cached version of the response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNotModified(string? message = null);

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
		IResult<TData> AddTemporaryRedirect(TData? data, string? message = null);

		/// <summary>
		/// Server sends this response to direct the client to get the requested resource at another
		/// URI with the same method that was used in the prior request. This has the same semantics
		/// as the 302 Found HTTP response code, with the exception that the user agent must not change
		/// the HTTP method used: if a POST was used in the first request, a POST must be used in the
		/// second request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddTemporaryRedirect(string? message = null);

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddPermanentRedirect(TData? data, string? message = null);

		/// <summary>
		/// Indicates that the client should continue the request or ignore the response if the request is already finished.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddPermanentRedirect(string? message = null);

		#endregion

		#region Error

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional error message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddError(TData? data, string? message = null);

		/// <summary>
		/// Custom error with HTTP status code 500.
		/// </summary>
		/// <param name="message">Optional error message.</param>
		/// <returns><see cref="IResult"/></returns>
		IResult AddError(string? message = null);

		#region Client

		/// <summary>
		/// Server cannot or will not process the request due to something that is
		/// perceived to be a client error (e.g., malformed request syntax,
		/// invalid request message framing, or deceptive request routing).
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddBadRequest(TData? data, string? message = null);

		/// <summary>
		/// Server cannot or will not process the request due to something that is
		/// perceived to be a client error (e.g., malformed request syntax,
		/// invalid request message framing, or deceptive request routing).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddBadRequest(string? message = null);

		/// <summary>
		/// Although the HTTP standard specifies "unauthorized", semantically
		/// this response means "unauthenticated". That is, the client must
		/// authenticate itself to get the requested response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddUnauthorized(TData? data, string? message = null);

		/// <summary>
		/// Although the HTTP standard specifies "unauthorized", semantically
		/// this response means "unauthenticated". That is, the client must
		/// authenticate itself to get the requested response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddUnauthorized(string? message = null);

		/// <summary>
		/// Is reserved for future use. The initial aim for creating this code was using it
		/// for digital payment systems, however this status code is used very rarely and
		/// no standard convention exists.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddPaymentRequired(TData? data, string? message = null);

		/// <summary>
		/// Is reserved for future use. The initial aim for creating this code was using it
		/// for digital payment systems, however this status code is used very rarely and
		/// no standard convention exists.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddPaymentRequired(string? message = null);

		/// <summary>
		/// Client does not have access rights to the content; that is, it is unauthorized,
		/// so the server is refusing to give the requested resource. Unlike 401 Unauthorized,
		/// the client's identity is known to the server.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddForbidden(TData? data, string? message = null);

		/// <summary>
		/// Client does not have access rights to the content; that is, it is unauthorized,
		/// so the server is refusing to give the requested resource. Unlike 401 Unauthorized,
		/// the client's identity is known to the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddForbidden(string? message = null);

		/// <summary>
		/// Something isn't found.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNotFound(TData? data, string? message = null);

		/// <summary>
		/// Something isn't found.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNotFound(string? message = null);

		/// <summary>
		/// Method is known by the server but is not supported by the target
		/// resource. For example, an API may not allow calling DELETE to
		/// remove a resource.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddMethodNotAllowed(TData? data, string? message = null);

		/// <summary>
		/// Method is known by the server but is not supported by the target
		/// resource. For example, an API may not allow calling DELETE to
		/// remove a resource.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddMethodNotAllowed(string? message = null);

		/// <summary>
		/// Is sent when the web server, after performing server-driven content
		/// negotiation, doesn't find any content that conforms to the criteria
		/// given by the user agent.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNotAcceptable(TData? data, string? message = null);

		/// <summary>
		/// Is sent when the web server, after performing server-driven content
		/// negotiation, doesn't find any content that conforms to the criteria
		/// given by the user agent.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNotAcceptable(string? message = null);

		/// <summary>
		/// This is similar to <see cref="AddUnauthorized(TData?,string?)"/> but
		/// authentication is needed to be done by a proxy.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddProxyAuthenticationRequired(TData? data, string? message = null);

		/// <summary>
		/// This is similar to <see cref="AddUnauthorized(string?)"/> but
		/// authentication is needed to be done by a proxy.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddProxyAuthenticationRequired(string? message = null);

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
		IResult<TData> AddRequestTimeout(TData? data, string? message = null);

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
		IResult AddRequestTimeout(string? message = null);

		/// <summary>
		/// This response is sent when a request conflicts with the current state
		/// of the server. Example: a database concurrency conflict occurred.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddConflict(TData? data, string? message = null);

		/// <summary>
		/// This response is sent when a request conflicts with the current state
		/// of the server. Example: a database concurrency conflict occurred.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddConflict(string? message = null);

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
		IResult<TData> AddGone(TData? data, string? message = null);

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
		IResult AddGone(string? message = null);

		/// <summary>
		/// Request rejected because the Content-Length header field is not
		/// defined and the server requires it.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddLengthRequired(TData? data, string? message = null);

		/// <summary>
		/// Request rejected because the Content-Length header field is not
		/// defined and the server requires it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddLengthRequired(string? message = null);

		/// <summary>
		/// Client has indicated preconditions in its headers which the server
		/// does not meet.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddPreconditionFailed(TData? data, string? message = null);

		/// <summary>
		/// Client has indicated preconditions in its headers which the server
		/// does not meet.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddPreconditionFailed(string? message = null);

		/// <summary>
		/// Request entity is larger than limits defined by server. The server
		/// might close the connection or return an Retry-After header field.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddPayloadTooLarge(TData? data, string? message = null);

		/// <summary>
		/// Request entity is larger than limits defined by server. The server
		/// might close the connection or return an Retry-After header field.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddPayloadTooLarge(string? message = null);

		/// <summary>
		/// URI is longer than the server is willing to interpret.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddUriTooLong(TData? data, string? message = null);

		/// <summary>
		/// URI is longer than the server is willing to interpret.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddUriTooLong(string? message = null);

		/// <summary>
		/// Media format of the requested data is not supported by the server, so the server is rejecting the request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddUnsupportedMediaType(TData? data, string? message = null);

		/// <summary>
		/// Media format of the requested data is not supported by the server, so the server is rejecting the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddUnsupportedMediaType(string? message = null);

		/// <summary>
		/// Range specified by the Range header field in the request cannot be fulfilled.
		/// It's possible that the range is outside the size of the target URI's data.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddRangeNotSatisfiable(TData? data, string? message = null);

		/// <summary>
		/// Range specified by the Range header field in the request cannot be fulfilled.
		/// It's possible that the range is outside the size of the target URI's data.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddRangeNotSatisfiable(string? message = null);

		/// <summary>
		/// Expectation indicated by the Expect request header field cannot be met by the server.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddExpectationFailed(TData? data, string? message = null);

		/// <summary>
		/// Expectation indicated by the Expect request header field cannot be met by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddExpectationFailed(string? message = null);

		/// <summary>
		/// Server refuses the attempt to brew coffee with a teapot.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddImATeapot(TData? data, string? message = null);

		/// <summary>
		/// Server refuses the attempt to brew coffee with a teapot.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddImATeapot(string? message = null);

		/// <summary>
		/// Request was directed at a server that is not able to produce a response.
		/// This can be sent by a server that is not configured to produce responses
		/// for the combination of scheme and authority that are included in the
		/// request URI.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddMisdirectedRequest(TData? data, string? message = null);

		/// <summary>
		/// Request was directed at a server that is not able to produce a response.
		/// This can be sent by a server that is not configured to produce responses
		/// for the combination of scheme and authority that are included in the
		/// request URI.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddMisdirectedRequest(string? message = null);

		/// <summary>
		/// Request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddUnprocessableContent(TData? data, string? message = null);

		/// <summary>
		/// Request was well-formed but was unable to be followed due to semantic errors.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddUnprocessableContent(string? message = null);

		/// <summary>
		/// Resource that is being accessed is locked.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddLocked(TData? data, string? message = null);

		/// <summary>
		/// Resource that is being accessed is locked.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddLocked(string? message = null);

		/// <summary>
		/// Request failed due to failure of a previous request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddFailedDependency(TData? data, string? message = null);

		/// <summary>
		/// Request failed due to failure of a previous request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddFailedDependency(string? message = null);

		/// <summary>
		/// Server is unwilling to risk processing a request that might be replayed.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddTooEarly(TData? data, string? message = null);

		/// <summary>
		/// Server is unwilling to risk processing a request that might be replayed.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddTooEarly(string? message = null);

		/// <summary>
		/// Server refuses to perform the request using the current protocol but might
		/// be willing to do so after the client upgrades to a different protocol. The
		/// server sends an Upgrade header in a 426 response to indicate the required
		/// protocol(s).
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddUpgradeRequired(TData? data, string? message = null);

		/// <summary>
		/// Server refuses to perform the request using the current protocol but might
		/// be willing to do so after the client upgrades to a different protocol. The
		/// server sends an Upgrade header in a 426 response to indicate the required
		/// protocol(s).
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddUpgradeRequired(string? message = null);

		/// <summary>
		/// Origin server requires the request to be conditional. This response is intended
		/// to prevent the 'lost update' problem, where a client GETs a resource's state,
		/// modifies it and PUTs it back to the server, when meanwhile a third party has
		/// modified the state on the server, leading to a conflict.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddPreconditionRequired(TData? data, string? message = null);

		/// <summary>
		/// Origin server requires the request to be conditional. This response is intended
		/// to prevent the 'lost update' problem, where a client GETs a resource's state,
		/// modifies it and PUTs it back to the server, when meanwhile a third party has
		/// modified the state on the server, leading to a conflict.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddPreconditionRequired(string? message = null);

		/// <summary>
		/// Received too many requests in a given amount of time.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddTooManyRequests(TData? data, string? message = null);

		/// <summary>
		/// Received too many requests in a given amount of time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddTooManyRequests(string? message = null);

		/// <summary>
		/// Server is unwilling to process the request because its header fields
		/// are too large. The request may be resubmitted after reducing the size
		/// of the request header fields.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddRequestHeaderFieldsTooLarge(TData? data, string? message = null);

		/// <summary>
		/// Server is unwilling to process the request because its header fields
		/// are too large. The request may be resubmitted after reducing the size
		/// of the request header fields.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddRequestHeaderFieldsTooLarge(string? message = null);

		/// <summary>
		/// User agent requested a resource that cannot legally be provided,
		/// such as a web page censored by a government.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddUnavailableForLegalReasons(TData? data, string? message = null);

		/// <summary>
		/// User agent requested a resource that cannot legally be provided,
		/// such as a web page censored by a government.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddUnavailableForLegalReasons(string? message = null);


		#endregion

		#region Server

		/// <summary>
		/// Server has encountered a situation it does not know how to handle.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddInternalServerError(TData? data, string? message = null);

		/// <summary>
		/// Server has encountered a situation it does not know how to handle.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddInternalServerError(string? message = null);

		/// <summary>
		/// No records affected.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNoRecordsAffected(TData? data, string? message = null);

		/// <summary>
		/// No records affected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNoRecordsAffected(string? message = null);

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNotImplemented(TData? data, string? message = null);

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNotImplemented(string? message = null);

		/// <summary>
		/// Server, while working as a gateway to get a response needed to
		/// handle the request, got an invalid response.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddBadGateway(TData? data, string? message = null);

		/// <summary>
		/// Server, while working as a gateway to get a response needed to
		/// handle the request, got an invalid response.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddBadGateway(string? message = null);

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
		IResult<TData> AddServiceUnavailable(TData? data, string? message = null);

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
		IResult AddServiceUnavailable(string? message = null);

		/// <summary>
		/// Server is acting as a gateway and cannot get a response in time.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddGatewayTimeout(TData? data, string? message = null);

		/// <summary>
		/// Server is acting as a gateway and cannot get a response in time.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddGatewayTimeout(string? message = null);

		/// <summary>
		/// HTTP version used in the request is not supported by the server.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddHttpVersionNotSupported(TData? data, string? message = null);

		/// <summary>
		/// HTTP version used in the request is not supported by the server.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddHttpVersionNotSupported(string? message = null);

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddConfigurationError(TData? data, string? message = null);

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddConfigurationError(string? message = null);

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddVariantAlsoNegotiates(TData? data, string? message = null);

		/// <summary>
		/// Server has an internal configuration error.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddVariantAlsoNegotiates(string? message = null);

		/// <summary>
		/// Server is unable to store the representation needed to successfully complete the request.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddInsufficientStorage(TData? data, string? message = null);

		/// <summary>
		/// Server is unable to store the representation needed to successfully complete the request.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddInsufficientStorage(string? message = null);

		/// <summary>
		/// Infinite loop detected.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddLoopDetected(TData? data, string? message = null);

		/// <summary>
		/// Infinite loop detected.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddLoopDetected(string? message = null);

		/// <summary>
		/// Further extensions to the request are required for the server to fulfill it.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNotExtended(TData? data, string? message = null);

		/// <summary>
		/// Further extensions to the request are required for the server to fulfill it.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNotExtended(string? message = null);

		/// <summary>
		/// Client needs to authenticate to gain network access.
		/// </summary>
		/// <param name="data">Optional data.</param>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult<TData> AddNetworkAuthenticationRequired(TData? data, string? message = null);

		/// <summary>
		/// Client needs to authenticate to gain network access.
		/// </summary>
		/// <param name="message">Optional message.</param>
		/// <returns><see cref="IResult{TData}"/></returns>
		IResult AddNetworkAuthenticationRequired(string? message = null);

		#endregion

		#endregion

		#endregion
	}

	/// <summary>
	/// A wrapper for returning data along with a message and success status.
	///
	/// Response ranges:
	/// Informational responses (100 – 199).
	/// Successful responses(200 – 299).
	/// Redirection messages(300 – 399).
	/// Client error responses(400 – 499).
	/// Server error responses(500 – 599).
	/// </summary>
	public interface IResult : IResult<bool>
	{
	}
}