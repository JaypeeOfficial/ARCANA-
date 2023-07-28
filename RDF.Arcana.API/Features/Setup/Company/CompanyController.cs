using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tls.Crypto.Impl;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;


namespace RDF.Arcana.API.Features.Setup.Company;

[Route("api/[controller]")]
[ApiController]

public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;
    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("AddNewCompany")]
    public async Task<IActionResult> AddNewCompany(AddNewCompany.AddNewCompanyCommand command)
    {
            try
            {
                await _mediator.Send(command);
                return Ok("Successfully added!");
                
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
    }
    
    [HttpGet("GetAllCompanies")]
    public async Task<IActionResult> GetAllCompanies([FromQuery]GetCompaniesAsync.GetCompaniesQuery request)
    {
        var response = new QueryOrCommandResult<object>();
      
        try
        {
            var companies = await _mediator.Send(request);
            Response.AddPaginationHeader(
                companies.CurrentPage,
                companies.PageSize,
                companies.TotalCount,
                companies.TotalPages,
                companies.HasPreviousPage,
                companies.HasNextPage
                );
            var results = new QueryOrCommandResult<object>
            {
                Success = true,
                Data = new
                {
                    companies,
                    companies.CurrentPage,
                    companies.PageSize,
                    companies.TotalCount,
                    companies.TotalPages,
                    companies.HasPreviousPage,
                    companies.HasNextPage
                },
                Status = StatusCodes.Status200OK
            };
            results.Messages.Add("Successfully Fetch");
            return Ok(results);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(e.Message);
        }
    }

    [HttpPut]
    [Route("UpdateCompany")]
    public async Task<IActionResult> UpdateCompany(UpdateCompany.UpdateCompanyCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var result = await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Company successfully updated");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Ok(response);
        }
    }

    [HttpPatch("UpdateCompanyStatus")]
    public async Task<IActionResult> UpdateCompanyStatus(UpdateCompanyStatus.UpdateCompanyStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Successfully updated the status");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
    
    
}