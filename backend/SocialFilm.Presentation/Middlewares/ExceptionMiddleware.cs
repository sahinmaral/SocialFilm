using AutoMapper;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

using SocialFilm.Domain.Exceptions;

using System.Net;

namespace SocialFilm.Presentation.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ExceptionMiddleware(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            //await LogExceptionToDatabaseAsync(exception, context.Request);
            await HandleExceptionAsync(context, exception);
        }
    }

    //private async Task LogExceptionToDatabaseAsync(Exception exception, HttpRequest httpRequest)
    //{
    //    ErrorLog errorLog = new ErrorLog()
    //    {
    //        ErrorMessage = exception.Message,
    //        StackTrace = exception.StackTrace,
    //        RequestPath = httpRequest.Path,
    //        RequestMethod = httpRequest.Method,
    //        Timestamp = DateTime.Now
    //    };

    //    CreateErrorLogCommand request = _mapper.Map<CreateErrorLogCommand>(errorLog);

    //    await _mediator.Send(request);
    //}

    private int HandleStatusCode(Exception exception)
    {
        switch (exception)
        {
            case SecurityTokenException:
            case ValidationException:
            case InvalidOperationException:
                return (int)HttpStatusCode.BadRequest;
            case EntityNullException:
                return (int)HttpStatusCode.NotFound;
            default:
                return (int)HttpStatusCode.InternalServerError;
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = HandleStatusCode(exception);

        return context.Response.WriteAsync(new ErrorResult()
        {
            Message = exception.Message,
            StatusCode = context.Response.StatusCode
        }.ToString());
    }
}
