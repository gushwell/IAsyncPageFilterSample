using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAppSample;

public class MyAsyncPageFilter : IAsyncPageFilter
{
    private readonly ILogger _logger;
    public MyAsyncPageFilter(ILogger logger)
    {
        _logger = logger;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        await next.Invoke();
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        var httpContext = context.HttpContext;
        var message = $"HttpType: {httpContext.Request?.Method}|Path :{httpContext.Request?.Path}\tUserAgent: {httpContext.Request?.Headers["User-Agent"].ToString()}\tPage: {context.HandlerInstance?.GetType().Name}\tHandler: {context.HandlerMethod?.MethodInfo?.Name}";
        _logger.LogTrace(message);
        return Task.CompletedTask;
    }

}

