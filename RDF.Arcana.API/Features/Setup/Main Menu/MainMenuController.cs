using MediatR;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;

namespace RDF.Arcana.API.Features.Setup.Main_Menu;

[Route("api/[controller]")]
[ApiController]

public class MainMenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MainMenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewMainMenu")]
    public async Task<IActionResult> AddNewMainMeu(AddMainMenu.AddMainMenuCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add($"{command.ModuleName} successfully added");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPut("UpdateMainMenu")]
    public async Task<IActionResult> UpdateMainMenu(UpdateMainMenu.UpdateMainMenuCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Messages.Add("The Main Menu has been updated successfully.");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    [HttpPatch("UpdateMainMenuStatus")]
    public async Task<IActionResult> UpdateMainMenuStatus(UpdateMainMenuStatus.UpdateMainMenuStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Messages.Add("The Main Menu status has been updated successfully.");
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Ok(response);
        }
    }

    [HttpGet("GetMainMenuAsync")]
    public async Task<IActionResult> GetMainMenuAsync([FromQuery]GetMainMenuAsync.GetMainMenuAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();

        try
        {
            var mainMenu = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                mainMenu.CurrentPage,
                mainMenu.PageSize,
                mainMenu.TotalPages,
                mainMenu.TotalCount,
                mainMenu.HasPreviousPage,
                mainMenu.HasNextPage
                );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    mainMenu,
                    mainMenu.CurrentPage,
                    mainMenu.PageSize,
                    mainMenu.TotalPages,
                    mainMenu.TotalCount,
                    mainMenu.HasPreviousPage,
                    mainMenu.HasNextPage
                }
            };
            
            response.Messages.Add("Successfully Fetch Data");
            return Ok(result);

        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;

            return Conflict(response);
        }
    }
}