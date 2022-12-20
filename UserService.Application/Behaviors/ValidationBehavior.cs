using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UserService.Application.Behaviors;

internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var typeName = request.GetType().Name;
        _logger.LogInformation($"----- Validating command {typeName}");

        var errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .ToList();
        if (!errors.Any()) return await next();

        _logger.LogWarning($"Validation errors - {typeName} - Command: {request} - Errors: {errors}");
        throw new ValidationException($"Validation errors - {typeName} - Command: {request} - Errors: {errors}", errors);
    }
}