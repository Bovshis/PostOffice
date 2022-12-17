using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Queries;
using UserService.Domain.Entities;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetById([FromRoute] GetUserByIdQuery getUserByIdQuery, CancellationToken cancellationToken)
    {
        var user = await _mediator.Send(getUserByIdQuery, cancellationToken);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost("create")]
    public async Task<ActionResult<User>> Create([FromBody]CreateUserCommand createUserCommand, CancellationToken cancellationToken)
    { 
        var user = await _mediator.Send(createUserCommand, cancellationToken);
        return Ok(user);
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> Delete([FromRoute]DeleteUserCommand deleteUserCommand, CancellationToken cancellationToken)
    {
        var isDeleted = await _mediator.Send(deleteUserCommand, cancellationToken);
        return isDeleted ? Ok() : NotFound();
    }

    [HttpPut("update")]
    public async Task<ActionResult> Delete([FromBody]UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
    {
        var isUpdated = await _mediator.Send(updateUserCommand, cancellationToken);
        return isUpdated ? Ok() : NotFound();
    }

}