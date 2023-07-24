using ELIXIR.DATA.DATA_ACCESS_LAYER.HELPERS;
using MediatR;
using RDF.Arcana.API.Common.Pagination;

namespace RDF.Arcana.API.Features.Setup.Module;

public class GetModuleAsync
{
    public class GetModuleAsyncQuery : UserParams, IRequest<PagedList<GetModuleAsyncResult>>
    {
        
    }

    public class GetModuleAsyncResult
    {
        public int Id { get; set; }
        public string Module { get; set; }
    }
}