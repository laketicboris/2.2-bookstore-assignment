using BookstoreApplication.Exceptions;
using System.Text.Json;

namespace BookstoreApplication.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public ExceptionHandlingMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            switch (exception)
            {
                case GoogleAuthException:
                case AuthenticationException:
                case UserCreationException:
                    await HandleGoogleAuthExceptionAsync(httpContext, exception);
                    return;

                default:
                    await HandleRegularExceptionAsync(httpContext, exception);
                    break;
            }
        }

        private async Task HandleGoogleAuthExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = StatusCodes.Status302Found;
            httpContext.Response.Headers.Location = "http://localhost:5173/auth/google-error";
            await httpContext.Response.WriteAsync("");
        }
        private async Task HandleRegularExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new { error = exception.Message };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}