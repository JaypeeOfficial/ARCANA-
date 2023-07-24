namespace RDF.Arcana.API.Features.Setup.Main_Menu.Exceptions;

public class MenuPathAlreadyExist : Exception
{
    public MenuPathAlreadyExist(string path) : base($"{path} already exist!"){}
}