using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Features.Setup.Product_Category;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

[Route("api/[controller]")]
[ApiController]


public class ProductSubCategoryController : Controller
{
   private readonly IMediator _mediator;

   public ProductSubCategoryController(IMediator mediator)
   {
      _mediator = mediator;
   }

   [HttpPost("AddNewProductSubCategory")]
   public async Task<IActionResult> AddNewProductSubCategory(
      AddNewProductSubCategory.AddNewProductSubCategoryCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         await _mediator.Send(command);
         response.Success = true;
         response.Status = StatusCodes.Status200OK;
         response.Messages.Add("Product Sub Category successfully added");
         return Ok(response);
      }
      catch (Exception e)
      {
         response.Status = StatusCodes.Status409Conflict;
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }

   [HttpPut("UpdateProductSubCategory/productSubCategory={id:int}")]
   public async Task<IActionResult> UpdateProductSubCategory([FromRoute] int id,
      UpdateProductSubCategory.UpdateProductSubCategoryCommand command)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         command.ProductSubCategoryId = id;
         await _mediator.Send(command);
         response.Status = StatusCodes.Status200OK;
         response.Success = true;
         response.Messages.Add("Product Sub Category has been updated successfully");
         return Ok(response);
      }
      catch (Exception e)
      {
         response.Status = StatusCodes.Status409Conflict;
         response.Messages.Add(e.Message);
         return Conflict(response);
      }
   }

   [HttpPatch("UpdateProductSubCategoryStatus/{id:int}")]
    public async Task<IActionResult> UpdateProductSubCategoryStatus([FromRoute]int id)
    {
        var response = new QueryOrCommandResult<object>();
    
        try
        {
            var command = new UpdateProductSubCategoryStatus.UpdateProductSubCategoryStatusCommand {
                ProductSubCategoryId = id
            };
    
            await _mediator.Send(command);
    
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Product Sub Category status has been successfully updated");
            
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

   [HttpGet("GetProductSubCategories")]
   public async Task<IActionResult> GetProductSubCategories([FromQuery]GetProductSubCategories.GetProductSubCategoriesQuery query)
   {
      var response = new QueryOrCommandResult<object>();
      try
      {
         var productSubCategories =  await _mediator.Send(query);
         Response.AddPaginationHeader(
            productSubCategories.CurrentPage,
            productSubCategories.PageSize,
            productSubCategories.TotalCount,
            productSubCategories.TotalPages,
            productSubCategories.HasPreviousPage,
            productSubCategories.HasNextPage
            );

         var result = new QueryOrCommandResult<object>
         {
            Success = true,
            Status = StatusCodes.Status200OK,
            Data = new
            {
               productSubCategories,
               productSubCategories.CurrentPage,
               productSubCategories.PageSize,
               productSubCategories.TotalCount,
               productSubCategories.TotalPages,
               productSubCategories.HasPreviousPage,
               productSubCategories.HasNextPage

            }
         };
         result.Messages.Add("Successfully Fetch Data");
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