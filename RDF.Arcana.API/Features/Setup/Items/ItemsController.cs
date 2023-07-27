using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/[controller]")]
[ApiController]

public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewItem")]
    public async Task<IActionResult> AddNewItem(AddNewItems.AddNewItemsCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add($"Item {command.ItemCode} successfully added");
            response.Status = StatusCodes.Status200OK;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
    
    [HttpPatch("UpdateItemStatus/{itemCode}")]
    public async Task<IActionResult> UpdateItemStatus([FromBody]UpdateItemStatus.UpdateItemStatusCommand command, [FromRoute]string itemCode)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ItemCode = itemCode;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Successfully updated the item status");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }      
    }

    [HttpPut("UpdateItem/{itemCode}")]
    public async Task<IActionResult> UpdateItem(UpdateItem.UpdateItemCommand command, [FromRoute] string itemCode)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ItemCode = itemCode;
            await _mediator.Send(command);
            response.Messages.Add("Successfully Fetch Data");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Ok(response);
        }
    }
}