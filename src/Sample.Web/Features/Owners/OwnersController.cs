using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Web.Features.Owners
{
    public class OwnersController : BaseAdminController
    {
        private readonly IMediator _mediator;

        public OwnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] Index.Query query)
        {
            var items = await _mediator.Send(query).ConfigureAwait(false);

            return Ok(items);
        }

        [HttpGet("{id:long}", Name = "GetOwner")]
        public async Task<IActionResult> Get(Details.Query query)
        {
            var entity = await _mediator.Send(query).ConfigureAwait(false);

            return Ok(entity);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] Create.Command model)
        {
            var createdId = await _mediator.Send(model).ConfigureAwait(false);
            if (createdId == 0)
                return Conflict();

            return CreatedAtRoute("GetOwner", new { id = createdId }, model);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] Edit.Command model)
        {
            await _mediator.Send(model).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(Delete.Command command)
        {
            await _mediator.Send(command).ConfigureAwait(false);
            return NoContent();
        }
    }
}
