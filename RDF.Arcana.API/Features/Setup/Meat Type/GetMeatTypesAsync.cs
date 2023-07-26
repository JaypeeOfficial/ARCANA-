// using MediatR;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
// using RDF.Arcana.API.Common.Pagination;
// using RDF.Arcana.API.Data;
// using RDF.Arcana.API.Domain;
//
// namespace RDF.Arcana.API.Features.Setup.Meat_Type;
//
// public class GetMeatTypesAsync
// {
//     public class GetMeatTypeQuery : UserParams, IRequest<PagedList<GetMeatTypeQueryResult>>
//     {
//         public string Search { get; set; }
//         public bool Status { get; set; }
//     }
//
//     public class GetMeatTypeQueryResult
//     {
//         public string MeatTypeName { get; set; }
//         public DateTime CreatedAt { get; set; }
//         public DateTime? UpdatedAt { get; set; }
//         public string ModifiedBy { get; set; }
//         public string AddedBy { get; set; }
//         public bool IsActive { get; set; }
//     }
//     
//     public class Handler : IRequestHandler<GetMeatTypeQuery, PagedList<GetMeatTypeQueryResult>>
//     {
//         private readonly DataContext _context;
//
//         public Handler(DataContext context)
//         {
//             _context = context;
//         }
//
//         public Task<PagedList<GetMeatTypeQueryResult>> Handle(GetMeatTypeQuery request, CancellationToken cancellationToken)
//         {
//             IQueryable<MeatType> meatTypes = _context.MeatTypes;
//             retur
//         }
//     }
// }