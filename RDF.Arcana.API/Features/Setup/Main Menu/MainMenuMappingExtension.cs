using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Main_Menu;

public static class MainMenuMappingExtension
{
    public static GetMainMenuAsync.GetMainMenuAsyncQueryResult
        ToGetMainMenuAsyncQueryResult(this MainMenu mainMenu)
    {
        return new GetMainMenuAsync.GetMainMenuAsyncQueryResult
        {
            ModuleName = mainMenu.ModuleName,
            CreatedAt = mainMenu.CreatedAt,
            UpdatedAt = mainMenu.UpdatedAt,
            AddedBy = mainMenu.AddedBy,
            IsActive = mainMenu.IsActive,
            ModifiedBy = mainMenu.ModifiedBy,
            MenuPath = mainMenu.MenuPath
        };
    }
}