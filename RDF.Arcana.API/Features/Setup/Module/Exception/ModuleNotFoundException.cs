namespace RDF.Arcana.API.Features.Setup.Module.Exception;

public class ModuleNotFoundException : System.Exception
{
    public ModuleNotFoundException() : base("Module not found"){}
}