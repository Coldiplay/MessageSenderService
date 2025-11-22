using MediatR;
using MessageSenderService.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace MessageSenderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public MessagesController(IMediator mediator) 
        {
            _mediator = mediator;
        }
        private readonly IMediator _mediator;

        [HttpPost("[action]")]
        public async Task<IActionResult> Send(string telephone, string message = "")
        {
            //Медиатором запускаем и ждём выполнение команды
            var result = await _mediator.Send(new SendMessageCommand() { Telephone = telephone, Message = message});
            // Потом изменить
            return Ok(result);
        }
    }
}
