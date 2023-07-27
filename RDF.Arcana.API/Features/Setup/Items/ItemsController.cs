using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

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
            response.Messages.Add("ItemCode has been updated successfully");
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
    
    [HttpGet("GetAllItems")]
    public async Task<IActionResult> GetAllItems([FromQuery] GetItemsAsync.GetItemsAsyncQuery request)
    {
        var response = new QueryOrCommandResult<object>();

        try
        {
            var items = await _mediator.Send(request);
            Response.AddPaginationHeader(
                items.CurrentPage,
                items.PageSize,
                items.TotalCount,
                items.TotalPages,
                items.HasPreviousPage,
                items.HasNextPage
            );
            var results = new QueryOrCommandResult<object>
            {
                Success = true,
                Data = new
                {
                    items,
                    items.CurrentPage,
                    items.PageSize,
                    items.TotalCount,
                    items.TotalPages,
                    items.HasPreviousPage,
                    items.HasNextPage
                },
                Status = StatusCodes.Status200OK
            };
            results.Messages.Add("Successfully Fetched");
            return Ok(results);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(e.Message);
        }
    }
}