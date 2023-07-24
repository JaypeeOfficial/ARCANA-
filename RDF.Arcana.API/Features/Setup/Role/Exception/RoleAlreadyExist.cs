namespace RDF.Arcana.API.Features.Setup.Role.Exception;

public class RoleAlreadyExist : System.Exception
{
    public RoleAlreadyExist(string roleName) : base($"{roleName} already exist, try something else"){}
}