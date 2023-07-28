using RDF.Arcana.API.Common.Pagination;
 using RDF.Arcana.API.Data;
 using RDF.Arcana.API.Domain;
 
 namespace RDF.Arcana.API.Features.Setup.Meat_Type
 {
     public class GetMeatTypesAsync
     {
         public class GetMeatTypeQuery : UserParams, IRequest<PagedList<GetMeatTypeQueryResult>>
         {
             public string Search { get; set; }
             public bool? Status { get; set; }
         }
 
         public class GetMeatTypeQueryResult
         {
             public int Id { get; set; }
             public string MeatTypeName { get; set; }
             public DateTime CreatedAt { get; set; }
             public DateTime? UpdatedAt { get; set; }
             public string ModifiedBy { get; set; }
             public string AddedBy { get; set; }
             public bool IsActive { get; set; }
         }
 
         public class Handler : IRequestHandler<GetMeatTypeQuery, PagedList<GetMeatTypeQueryResult>>
         {
             private readonly DataContext _context;
 
             public Handler(DataContext context)
             {
                 _context = context;
             }
 
             public async Task<PagedList<GetMeatTypeQueryResult>> Handle(GetMeatTypeQuery request, CancellationToken cancellationToken)
             {
                 var query = _context.MeatTypes.AsQueryable();
 
                 if (!string.IsNullOrEmpty(request.Search))
                 {
                     query = query.Where(m => m.MeatTypeName.Contains(request.Search));
                 }
 
                 if (request.Status.HasValue)
                 {
                     query = query.Where(m => m.IsActive == request.Status);
                 }
 
                 var meatTypes = query.Select(m => new GetMeatTypeQueryResult
                 {
                     Id = m.Id,
                     MeatTypeName = m.MeatTypeName,
                     CreatedAt = m.CreatedAt,
                     UpdatedAt = m.UpdatedAt,
                     ModifiedBy = m.ModifiedBy,
                     AddedBy = m.AddedBy,
                     IsActive = m.IsActive
                 });
 
                 return await PagedList<GetMeatTypeQueryResult>.CreateAsync(meatTypes, request.PageNumber, request.PageSize);
             }
         }
     }
 }