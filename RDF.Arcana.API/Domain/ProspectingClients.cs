using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ProspectingClients : BaseEntity
{
    public string OwnersName { get; set; }
    public string OwnersAddress { get; set; }
    public string BusinessName { get; set; }
    public string BusinessAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string RegistrationStatus { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
    public string AuthorizedRepresentative { get; set; }
    public string AuthorizedRepresentativePosition { get; set; }
    public string TitleAuthorizedSignatory { get; set; }
    public string AuthorizationLetter { get; set; }
    public string OwnerValidId { get; set; }
    public string RepresentativeValidId { get; set; }
    public string DtiPermitPhoto { get; set; }
    public string BarangayOrOtherPermitPhoto { get; set; }
    public string StorePhoto { get; set; }
    public string StoreCategory { get; set; }
    public int AddedBy { get; set; }
    [ForeignKey("ApprovedByUser")]
    public int? ApprovedBy { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    
    //Relationships && Navigational Properties
    public virtual User AddedByUser { get; set; }
    public virtual User ApprovedByUser { get; set; }
}