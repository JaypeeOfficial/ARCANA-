using FluentValidation;
using FluentValidation.Validators;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class User : BaseEntity
{
    public string Fullname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public int LocationId { get; set; }
    public int RoleId { get; set; }
    public virtual Company Company { get; set; }
    public virtual Department Department { get; set; }
    public virtual Location Location { get; set; }
    public virtual Role Role { get; set; }
}

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required!")
            .MinimumLength(3).WithMessage("Username must be at least 3 character long!");
    }
}