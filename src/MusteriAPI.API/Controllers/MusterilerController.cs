using Microsoft.AspNetCore.Mvc;
using MediatR;
using MusteriAPI.Application.Features.Musteriler.Commands.CreateMusteri;
using MusteriAPI.Application.Features.Musteriler.Commands.UpdateMusteri;
using MusteriAPI.Application.Features.Musteriler.Commands.DeleteMusteri;
using MusteriAPI.Application.Features.Musteriler.Queries.GetAllMusteriler;
using MusteriAPI.Application.Features.Musteriler.Queries.GetMusteriById;
using MusteriAPI.Domain.Entities;

namespace MusteriAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusterilerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MusterilerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musteri>>> GetAllMusteriler()
        {
            var query = new GetAllMusterilerQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Musteri>> GetMusteriById(int id)
        {
            var query = new GetMusteriByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateMusteri(CreateMusteriCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMusteri(int id, UpdateMusteriCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMusteri(int id)
        {
            var command = new DeleteMusteriCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
                return NotFound();
            
            return NoContent();
        }
    }
} 