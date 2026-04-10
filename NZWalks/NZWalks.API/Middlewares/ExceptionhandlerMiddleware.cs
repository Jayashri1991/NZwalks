using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionhandlerMiddleware
    {
        private readonly ILogger<ExceptionhandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionhandlerMiddleware(ILogger<ExceptionhandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorid = Guid.NewGuid();
                //log this exception
                logger.LogError(ex,$"{errorid} : {ex.Message}");
                //Return a custom error exception
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType="application/json";
                var error = new {
                id=errorid,
                Errormessage="Something went wrong! We are looking into resolving this"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
                throw;
            }
        }
    }
}
