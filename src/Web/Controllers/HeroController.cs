using System.Threading.Tasks;
using ApplicationCore.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos;
using Web.Features.HeroFeature.Commands;
using Web.Features.HeroFeature.Queries;

namespace Web.Controllers
{
    [ApiController]
    [Route("heroes")]
    [Produces("application/json")]
    public class HeroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HeroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet] // GET: heroes
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllHeroesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")] // GET: heroes/123
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var query = new GetHeroByIdQuery() { HeroId = id };
            var result = await _mediator.Send(query);

            return result == null ? (IActionResult)NotFound() : Ok(result);
        }

        [HttpDelete("{id}")] // DELETE: heroes/123
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var query = new DeleteHeroByIdCommand() { HeroId = id };
            await _mediator.Send(query);

            return NoContent();
        }

        [HttpPost] // POST: heroes
        public async Task<IActionResult> Post([FromBody] HeroDto newHero)
        {
            var query = new CreateHeroCommand() { NewHero = newHero };
            var result = await _mediator.Send(query);

            return Accepted(result);
        }

        [HttpPut("{id}")] // PUT: heroes/123
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] HeroDto updateHero)
        {
            if (id != updateHero.Id)
                return BadRequest();

            var query = new UpdateHeroCommand() { HeroDto = updateHero };
            await _mediator.Send(query);

            return NoContent();
        }
    }
}