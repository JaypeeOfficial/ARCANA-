using AutoMapper;
using RDF.Arcana.API.Domain;

namespace MineralWaterMonitoring.Features.Authenticate;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<User, AuthenticateUser.AuthenticateUserResult>();
    }
}