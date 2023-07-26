using MediatR;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

[Route("api/[controller]")]
[ApiController]

public class ProductCategoryController : ControllerBase
{

   private readonly IMediator _mediator;

   public ProductCategoryController(IMediator mediator)
   {
      _mediator = mediator;
   }

   [HttpPost("AddNewProductCategory")]
   public async Task<IActionResult> AddNewProductCategory(AddNewProductCategory.AddNewProductCategoryCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         await _mediator.Send(command);
         response.Status = StatusCodes.Status200OK;
         response.Success = true;
         response.Messages.Add("Product Category successfully added");
         return Ok(response);
      }
      catch (Exception e)
      {
         response.Status = StatusCodes.Status409Conflict;
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }

   [HttpPut("UpdateProductCategory")]
   public async Task<IActionResult> UpdateProductCategory(UpdateProductCategory.UpdateProductCategoryCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         await _mediator.Send(command);
         response.Status = StatusCodes.Status200OK;
         response.Messages.Add("Product Category has been updated successfully");
         response.Success = true;
         return Ok(response);
      }
      catch (Exception e)
      {
         response.Messages.Add(e.Message);
         response.Status = StatusCodes.Status409Conflict;

         return Conflict(response);
      }
   }
   [HttpPatch("UpdateProductCategoryStatus/{productCategoryId:int}")]
   public async Task<IActionResult> UpdateProductCategoryStatus([FromRoute] int productCategoryId,
      [FromBody] UpdateProductCategoryStatus.UpdateProductCategoryStatusCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         command.ProductCategoryId = productCategoryId;
         await _mediator.Send(command);
         response.Success = true;
         response.Messages.Add("Product Category status has been updated");
         response.Status = StatusCodes.Status200OK;
         return Ok(response);
      }
      catch (Exception e)
      {
         response.Messages.Add(e.Message);
         response.Status = StatusCodes.Status409Conflict;
         return Conflict(response);
      }
   }

   [HttpGet("GetProductCategory")]
   public async Task<IActionResult> GetProductCategory([FromQuery]GetProductCategoryAsync.GetProductCategoryAsyncQuery query)
   {
      var response = new QueryOrCommandResult<object>();
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

         var productCategories = new QueryOrCommandResult<object>
         {
            Success = true,
            Status = StatusCodes.Status200OK,
            Data = new
            {
               result,
               result.CurrentPage,
               result.PageSize,
               result.TotalCount,
               result.TotalPages,
               result.HasPreviousPage,
               result.HasNextPage
            }
         };
         productCategories.Messages.Add("Successfully fetch data");
         return Ok(productCategories);
      }
      catch (Exception e)
      {
         response.Messages.Add(e.Message);
         response.Status = StatusCodes.Status409Conflict;
         return Conflict(response);
      }
   }
   
}