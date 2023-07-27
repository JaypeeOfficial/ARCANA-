using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/[controller]")]
[ApiController]

public class DiscountController : Controller
{
    private readonly IMediator _mediator;

    public DiscountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewDiscount")]
    public async Task<IActionResult> AddNewDiscount(AddNewDiscount.AddNewDiscountCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Discount has been added successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Ok(response);
        }
    }

    [HttpPut("UpdateDiscount/{id:int}")]
    public async Task<IActionResult> UpdateDiscount([FromRoute] int id,
        [FromBody] UpdateDiscount.UpdateDiscountCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.Id = id;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Discount has been successfully updated");
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("UpdateDiscountStatus/{id:int}")]
    public async Task<IActionResult> UpdateDiscountStatus([FromRoute] int id,
        UpdateDiscountStatus.UpdateDiscountStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.Id = id;
            await _mediator.Send(command);
            response.Messages.Add("Discount status has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpGet("GetDiscount")]
    public async Task<IActionResult> GetAllDiscount([FromQuery]GetDiscountsAsync.GetDiscountAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var discount =  await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                discount.CurrentPage,
                discount.PageSize,
                discount.TotalCount,
                discount.TotalPages,
                discount.HasPreviousPage,
                discount.HasNextPage
                );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    discount,
                    discount.CurrentPage,
                    discount.PageSize,
                    discount.TotalCount,
                    discount.TotalPages,
                    discount.HasPreviousPage,
                    discount.HasNextPage
                }
            };
            result.Messages.Add("Successfully fetch data");
            return Ok(result);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}