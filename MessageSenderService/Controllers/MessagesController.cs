using MediatR;
using MessageSenderService.Model.Middleware;
using MessageSenderService.Tools;
using Microsoft.AspNetCore.Mvc;

namespace MessageSenderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IMediator mediator) : ControllerBase
    {
        [HttpPost("[action]/{commandName}")]
        public async Task<IActionResult> Execute(string commandName, params object[]? parameters)
        {
            var command = TypesManager.GetCommand(commandName, HttpContext.RequestServices, parameters) ?? 
                throw new CustomException() { ErrorCode = 404, ErrorMessage= "Данный метод не найден"};
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
