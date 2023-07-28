using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.UOM;

[Route("api/[controller]")]
[ApiController]

public class UomController : Controller
{
    private readonly IMediator _mediator;

    public UomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewUom")]
    public async Task<IActionResult> AddNewUom(AddNewUom.AddNewUomCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("UOM has been added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPut("UpdateUom/{id:int}")]
    public async Task<IActionResult> UpdateUom([FromRoute] int id, UpdateUom.UpdateUomCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UomId = id;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("UOM has been updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("UpdateUomStatus/{id:int}")]
    public async Task<IActionResult> UpdateUomStatus([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateUomStatus.UpdateUomStatusCommand
            {
                UomId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("UOM status haas been updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Ok(response);
        }
    }

    [HttpGet("GetUom")]
    public async Task<IActionResult> GetUom([FromQuery] GetUomAsync.GetUomAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();

        try
        {
            var uom = await _mediator.Send(query);
            Response.AddPaginationHeader(
                uom.CurrentPage,
                uom.PageSize,
                uom.TotalCount,
                uom.TotalPages,
                uom.HasPreviousPage,
                uom.HasNextPage
                );

            var result = new QueryOrCommandResult<object>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Data = new
                {
                    uom,
                    uom.CurrentPage,
                    uom.PageSize,
                    uom.TotalCount,
                    uom.TotalPages,
                    uom.HasPreviousPage,
                    uom.HasNextPage
                }
            };
            result.Messages.Add("Successfully fetch data");
            return Ok(result);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}