using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Clients.Prospecting;

[Route("api/Company")]
[ApiController]


public class AddNewProspect : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewProspect(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewProspectCommand : IRequest<Unit>
    {
        public string OwnersName { get; set; }
        public string OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int AddedBy { get; set; }
    }
    public class Handler : IRequestHandler<AddNewProspectCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewProspectCommand request, CancellationToken cancellationToken)
        {
            var existingProspectCustomer =
                await _context.ProspectingClients.FirstOrDefaultAsync(x => x.BusinessName == request.BusinessName,
                    cancellationToken);

            switch (existingProspectCustomer?.RegistrationStatus)
            {
                case "Request":
                    throw new System.Exception("Business is already requested, and subject for approval");
                case "Approved":
                    throw new System.Exception("Business is already Approved, please check your Approved Clients page");
            }

            var prospectingClients = new ProspectingClients
            {
                OwnersName = request.OwnersName,
                OwnersAddress = request.OwnersAddress,
                PhoneNumber = request.PhoneNumber,
                BusinessName = request  .BusinessName,
                RegistrationStatus = "Requested",
                AddedBy = request.AddedBy
            };

            await _context.ProspectingClients.AddAsync(prospectingClients, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    [HttpPost("AddNewProspect")]
    public async Task<IActionResult> Add(AddNewProspectCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("The new prospect is requested successfully");
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