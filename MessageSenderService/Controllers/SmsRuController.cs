using MediatR;
using MessageSenderService.CQRS;
using MessageSenderService.Model.ResponseClass;
using Microsoft.AspNetCore.Mvc;

namespace MessageSenderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsRuController(IMediator mediator) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> SendMessage(string telephone, string message = "")
        {
            var command = new SendMessageCommand<SendMessageResponse>() { Telephone = telephone, Message = message };
            //Медиатором запускаем и ждём выполнение команды
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBalance()
        {
            var command = new GetBalanceCommand<BalanceResponse>();
            var result = await mediator.Send(command);
            return Ok(result);
        }

    }
}
