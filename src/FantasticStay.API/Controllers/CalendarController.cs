using System;
using System.Net;
using System.Threading.Tasks;
using FantasticStay.API.Application.Commands;
using FantasticStay.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FantasticStay.API.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CalendarController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<CalendarController> _logger;

		public CalendarController(IMediator mediator, ILogger<CalendarController> logger)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[Route("create")]
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreateCalendarAsync([FromBody]CreateCalendarCommand command)
		{
			_logger.LogInformation("Sending command: {CommandName} - ({@Command})", command.GetType().FullName, command);

			var commandResult = await _mediator.Send(command);

			if (!commandResult)
			{
				return BadRequest();
			}

			return Ok();
		}

		[Route("blockDates")]
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> BlockDatesAsync([FromBody]BlockCalendarDatesCommand command)
		{
			_logger.LogInformation("Sending command: {CommandName} - ({@Command})", command.GetType().FullName, command);

			var commandResult = await _mediator.Send(command);

			if (!commandResult)
			{
				return BadRequest();
			}

			return Ok();
		}

		[Route("addAvailability")]
		[HttpPut]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> AddAvailabilityAsync([FromBody]AddCalendarAvailabilityCommand command)
		{
			_logger.LogInformation("Sending command: {CommandName} - ({@Command})", command.GetType().FullName, command);

			var commandResult = await _mediator.Send(command);

			if (!commandResult)
			{
				return BadRequest();
			}

			return Ok();
		}

		[Route("getAvailability")]
		[HttpGet]
		[ProducesResponseType(typeof(GetCalendarAvailabilityViewModel), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetAvailabilityAsync([FromBody]GetCalendarAvailabilityQuery query)
		{
			_logger.LogInformation("Sending query: {QueryName} - ({@Query})", query.GetType().FullName, query);

			var queryResult = await _mediator.Send(query);

			return Ok(queryResult);
		}
	}
}