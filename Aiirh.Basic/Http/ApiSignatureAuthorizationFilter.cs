using Aiirh.Basic.Messages;
using Aiirh.Basic.Security;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aiirh.Basic.Http
{
    public abstract class InternalApiController<TApiType> : InternalApiController where TApiType : Enum
    {
        public abstract TApiType Type { get; }

        internal override string ApiType => Type.ToString();
    }


    [ApiController]
    [ServiceFilter(typeof(ApiSignatureAuthorizationFilter), Order = 1)]
    public abstract class InternalApiController : Controller
    {
        internal abstract string ApiType { get; }
    }

    internal class ApiSignatureAuthorizationFilter : ActionFilterAttribute
    {
        private readonly IApiSignatureManager _apiSignatureManager;

        public ApiSignatureAuthorizationFilter(IApiSignatureManager apiSignatureManager)
        {
            _apiSignatureManager = apiSignatureManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as InternalApiController;
            var query = controller?.Request.Query;
            if (query == null)
            {
                var signatureIsMissingResponse = OperationResult.CreateError("Incorrect controller type is used");
                context.Result = new ObjectResult(signatureIsMissingResponse) { StatusCode = (int)HttpStatusCode.Unauthorized };
                base.OnActionExecuting(context);
                return;
            }

            var hashExists = query.TryGetValue("hash", out var hash);
            var keyExists = query.TryGetValue("key", out var key);
            var utcDateStringExists = query.TryGetValue("utcDate", out var utcDateString);

            if (!hashExists || !keyExists || !utcDateStringExists)
            {
                var signatureIsMissingResponse = OperationResult.CreateError("Api signature is missing in request");
                context.Result = new ObjectResult(signatureIsMissingResponse) { StatusCode = (int)HttpStatusCode.Unauthorized };
                base.OnActionExecuting(context);
                return;
            }

            var utcDateIsDate = DateTime.TryParse(utcDateString, out var utcDate);
            if (!utcDateIsDate)
            {
                var invalidDateResponse = OperationResult.CreateError("Api signature date has invalid format");
                context.Result = new ObjectResult(invalidDateResponse) { StatusCode = (int)HttpStatusCode.Unauthorized };
                base.OnActionExecuting(context);
                return;
            }

            try
            {
                _apiSignatureManager.ValidateKey(controller.ApiType, hash, key, utcDate);
            }
            catch (Exception e)
            {
                var response = OperationResult.CreateError(e);
                context.Result = new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Unauthorized };
                base.OnActionExecuting(context);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
