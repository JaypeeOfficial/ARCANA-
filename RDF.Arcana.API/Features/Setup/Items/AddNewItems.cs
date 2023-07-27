﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items.Exceptions;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;
using RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Items;

public class AddNewItems
{
    public class AddNewItemsCommand : IRequest<Unit>
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int UomId { get; set; }
        public int ProductCategoryId { get; set; }
        public int MeatTypeId { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewItemsCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewItemsCommand request, CancellationToken cancellationToken)
        {
            var existingItem = await _context.Items.FirstOrDefaultAsync(x => x.ItemCode == request.ItemCode, cancellationToken);
            var validateProductCategory =
                await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == request.ProductCategoryId,
                    cancellationToken);
            var validateUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);
            var validateMeatType =
                await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            if (existingItem is not null)
            {
                throw new ItemAlreadyExistException();
            }

            if (validateUom is null)
            {
                throw new UomNotFoundException();
            }

            if (validateProductCategory is null)
            {
                throw new NoProductCategoryFoundException();
            }

            if (validateMeatType is null)
            {
                throw new MeatTypeNotFoundException();
            }

            var items = new Domain.Items
            {
                ItemCode = request.ItemCode,
                ItemDescription = request.ItemDescription,
                UomId = request.UomId,
                ProductCategoryId = request.ProductCategoryId,
                MeatTypeId = request.MeatTypeId,
                IsActive = true
            };

            await _context.Items.AddAsync(items, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;

        }
    }
}