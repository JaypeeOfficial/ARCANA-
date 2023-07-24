namespace RDF.Arcana.API.Features.Setup.Main_Menu.Exceptions;

public class MenuNameAlreadyExist : Exception
{
    public MenuNameAlreadyExist(string menuName) : base($"{menuName} already exist!"){}
}