using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Features.Setup.Location.Exception;

namespace RDF.Arcana.API.Features.Setup.Location;

[Route("api/[controller]")]
[ApiController]

public class LocationController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewLocation")]
    public async Task<IActionResult> AddNewLocation(AddNewLocation.AddNewLocationCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add($"Location {command.LocationName} successfully added");
            response.Status = StatusCodes.Status200OK;
            return Ok(response);

        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("UpdateLocationStatus")]
    public async Task<IActionResult> UpdateLocationStatus(UpdateLocationStatus.UpdateLocationStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Successfully updated the status");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }      
    }

    [HttpGet("GetAllLocations")]
    public async Task<IActionResult> GetAllLocations([FromQuery]GetAllLocationAsync.GetAllLocationAsyncQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);
            Response.AddPaginationHeader(
                result.CurrentPage,
                result.PageSize,
                result.TotalCount,
                result.TotalPages,
                result.HasPreviousPage,
                result.HasNextPage
                );

            var results = new QueryOrCommandResult<object>()
            {
                Success = true,
                Data = new
                {
                    result.CurrentPage,
                    result.PageSize,
                    result.TotalCount,
                    result.TotalPages,
                    result.HasPreviousPage,
                    result.HasNextPage
                }
            };
            results.Messages.Add("Successfully Data Fetch");
            return Ok(result);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("UpdateLocation")]
    public async Task<IActionResult> UpdateLocation(UpdateLocation.UpdateLocationCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Location updated successfully");
            response.Status = StatusCodes.Status200OK;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}