using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Rekognition.MediatR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnalyzerController(IMediator mediator) { _mediator = mediator; }
        

        // POST api/values
        [HttpPost]
        public async Task<Result> Post([FromBody] Message message)
        {
            var result = await _mediator.Send(message);

            return result;
        }
    }
}
