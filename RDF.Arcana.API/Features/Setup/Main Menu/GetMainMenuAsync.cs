using MediatR;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Main_Menu;

public class GetMainMenuAsync
{
    public class GetMainMenuAsyncQuery : UserParams, IRequest<PagedList<GetMainMenuAsyncQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status{ get; set; }
    }

    public class GetMainMenuAsyncQueryResult
    {
        public string ModuleName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
        public string ModifiedBy { get; set; }
        public string MenuPath { get; set; }
    }
    
    public class Handler : IRequestHandler<GetMainMenuAsyncQuery, PagedList<GetMainMenuAsyncQueryResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetMainMenuAsyncQueryResult>> Handle(GetMainMenuAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<MainMenu> mainMenus = _context.MainMenus;

            if (!string.IsNullOrEmpty(request.Search))
            {
                mainMenus = mainMenus.Where(x => x.ModuleName.Contains(request.Search));
            }

            if (request.Status != null)
            {
                mainMenus = mainMenus.Where(x => x.IsActive == request.Status);
            }

            var result = mainMenus.Select(x => x.ToGetMainMenuAsyncQueryResult());

            return await PagedList<GetMainMenuAsyncQueryResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}