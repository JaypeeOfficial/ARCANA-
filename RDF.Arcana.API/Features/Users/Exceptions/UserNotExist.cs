namespace RDF.Arcana.API.Features.Users.Exceptions;

public class UserNotExist : System.Exception
{
    public UserNotExist() : base("User not exist") {}
}