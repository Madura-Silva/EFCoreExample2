using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreSample.Api.Controllers
{
    /// <summary>
    /// https://www.youtube.com/watch?v=95EbHz3aKYA&ab_channel=DotNetCoreCentral
    /// </summary>
    

    //[Route("api/[Controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private ILogger _logger;

        public ErrorController(ILogger logger)
        {
            _logger = logger;  
        }

        //[NonAction]
        [Route("/handleError")]
        [AllowAnonymous]
        public IActionResult HandleError()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var statusCode = exception.Error.GetType().Name switch
            {
                "NullReferenceException" => HttpStatusCode.NotFound,              
                "NotImplementedException" => HttpStatusCode.NotImplemented,
                "ApplicationException" => HttpStatusCode.NotImplemented,
                _ => HttpStatusCode.ServiceUnavailable

            };

            return Problem(detail: exception.Error.Message, statusCode: (int) statusCode, title: exception.Error.Source);
        }
    }
}