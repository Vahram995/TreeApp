using TreeApplication.Exceptions;
using TreeApplication.Models;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;
using TreeApplication.DAL.Repositories.Abstraction;
using TreeApplication.DAL.Entities;

namespace TreeApplication.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IExceptionJournalRepository _exceptionJournalRepository;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SecureException secEx) 
            {
                _logger.Error($"Secure Exception was thrown: {secEx}.");

                await HandleExceptionAsync(context, secEx);
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception was thrown: {ex}.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var secEx = ex as SecureException;

            var queryParameters = JsonConvert.SerializeObject(context.Request.Query);
            var bodyParameters = string.Empty;

            context.Request.EnableBuffering(); 

            if (context.Request.ContentLength > 0)
            {
                context.Request.Body.Position = 0;
                using (var reader = new StreamReader(context.Request.Body))
                {
                    bodyParameters = await reader.ReadToEndAsync();
                }
                context.Request.Body.Position = 0;
            }


            var exception = new ExceptionJournal
            {
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                QueryParameters = queryParameters,
                BodyParameters = bodyParameters,
                StackTrace = secEx?.StackTrace ?? ex.StackTrace
            };

            await _exceptionJournalRepository.AddAsync(exception);
            await _exceptionJournalRepository.SaveChangesAsync();


            var errorModel = new ErrorModel
            {
                Id = exception.Id.ToString(),
                Type = ex.GetType().Name,
                Data = new
                {
                    Message = secEx?.Message ?? "Internal Server Error.",
                }
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorModel));
        }
    }
}
