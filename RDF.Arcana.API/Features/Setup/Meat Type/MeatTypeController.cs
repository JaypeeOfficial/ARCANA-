using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

[Route("api/[controller]")]
[ApiController]

public class MeatTypeController : Controller
{
    private readonly IMediator _mediator;

    public MeatTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewMeatType")]
    public async Task<IActionResult> AddNewMeatType(AddNewMeatType.AddNewMeatTypeCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Meat Type successfully added");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPut("UpdateMeatType/{id:int}")]
    public async Task<IActionResult> UpdateMeatType([FromRoute] int id, UpdateMeatType.UpdateMeatTypeCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.MeatTypeId = id;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Meat type updated successfully.");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(ex.Message);
            return Conflict(response);
        }
    }
    
    [HttpGet("GetMeatType")]
    public async Task<IActionResult> GetMeatTypes([FromQuery] GetMeatTypesAsync.GetMeatTypeQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var meatTypes = await _mediator.Send(query);
            Response.AddPaginationHeader(
                meatTypes.CurrentPage,
                meatTypes.PageSize,
                meatTypes.TotalCount,
                meatTypes.TotalPages,
                meatTypes.HasPreviousPage,
                meatTypes.HasNextPage
            );

            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Data = new
            {
                meatTypes,
                meatTypes.CurrentPage,
                meatTypes.PageSize,
                meatTypes.TotalCount,
                meatTypes.TotalPages,
                meatTypes.HasPreviousPage,
                meatTypes.HasNextPage
            };

            response.Messages.Add("Successfully fetch data");

            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);

            return Conflict(response);
        }
    }

    [HttpPut("UpdateStatus/{id:int}")]
    public async Task<IActionResult> UpdateMeatTypeStatus([FromRoute] int id, UpdateMeatTypeStatus.UpdateMeatTypeStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.MeatTypeId = id;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Meat type status updated successfully.");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(ex.Message);
            return Conflict(response);
        }
    }
}