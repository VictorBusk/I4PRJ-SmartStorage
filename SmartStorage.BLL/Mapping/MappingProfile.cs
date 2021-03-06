﻿using AutoMapper;
using SmartStorage.BLL.Dtos;
using SmartStorage.DAL.Models;
using System.Diagnostics.CodeAnalysis;

namespace SmartStorage.BLL.Mapping
{
  [ExcludeFromCodeCoverage]
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Model to Dto
      CreateMap<Category, CategoryDto>();
      CreateMap<Inventory, InventoryDto>();
      CreateMap<Product, ProductDto>();
      CreateMap<Status, StatusDto>();
      CreateMap<Stock, StockDto>();
      CreateMap<Supplier, SupplierDto>();
      CreateMap<Transaction, TransactionDto>();
      CreateMap<Wholesaler, WholesalerDto>();

      // Dto to Model
      CreateMap<CategoryDto, Category>();
      CreateMap<InventoryDto, Inventory>();
      CreateMap<ProductDto, Product>();
      CreateMap<StatusDto, Status>();
      CreateMap<StockDto, Stock>();
      CreateMap<SupplierDto, Supplier>();
      CreateMap<TransactionDto, Transaction>();
      CreateMap<WholesalerDto, Wholesaler>();
    }
  }
}
