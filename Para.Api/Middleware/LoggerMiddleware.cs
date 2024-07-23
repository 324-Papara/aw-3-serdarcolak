using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Para.Api.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LoggerMiddleware> logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var originalBodyStream = httpContext.Response.Body;
            
            string requestText = string.Empty;
            if (httpContext.Request.Body.CanSeek)
            {
                httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                requestText = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
                httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            }
            
            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;
            
            await next.Invoke(httpContext);
            
            tempStream.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(tempStream, Encoding.UTF8).ReadToEndAsync();
            tempStream.Seek(0, SeekOrigin.Begin);
            
            await tempStream.CopyToAsync(originalBodyStream);
            
            httpContext.Response.Body = originalBodyStream;
            
            logger.LogInformation($"Request: {requestText}");
            logger.LogInformation($"Response: {responseText}");
        }
    }
}
