namespace RDF.Arcana.API.Features.Setup.Module.Exception;

public class ModulePathAlreadyExistException : System.Exception
{
    public ModulePathAlreadyExistException(string path) : base($"Path {path} is already exist"){}
}