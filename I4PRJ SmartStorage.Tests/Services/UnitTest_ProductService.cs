﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using SmartStorage.BLL.Dtos;
using SmartStorage.BLL.Mapping;
using SmartStorage.BLL.Services;
using SmartStorage.DAL.Interfaces;
using SmartStorage.DAL.Models;

namespace SmartStorage.UnitTests.Services
{
  [TestFixture]
  class UnitTest_ProductService
  {
    private IUnitOfWork _uow;
    private ProductService _productService;
    private List<Product> productList;

    [SetUp]
    public void SetUp()
    {
      _uow = Substitute.For<IUnitOfWork>();
      Mapper.Initialize(c => c.AddProfile<MappingProfile>());
      _productService = new ProductService(_uow);

      productList = new List<Product>
      {
          new Product()
          {
              ByUser = "Test",
              IsDeleted = false,
              ProductId = 1,
              Name = "Test",
              Updated = DateTime.Now
          },
           new Product()
          {
              ByUser = "Test",
              IsDeleted = true,
              ProductId = 1,
              Name = "Test",
              Updated = DateTime.Now
          }
      };
    }

    [Test]
    public void ProductServiceAdd_UnitOfWorkAddAndComplete_ReturnsUnitOfWorkAddAndComplete()
    {
      var productDto = new ProductDto() { Name = "Test" };
      var entity = Mapper.Map<ProductDto, Product>(productDto);

      _productService.Add(productDto);

      _uow.Received().Products.Add(entity);
      _uow.Received().Complete();
    }

    [Test]
    public void ProductServiceUpdate_UnitOfWorkUpdateAndComplete_ReturnsUnitOfWorkUpdateAndComplete()
    {
      var productDto = new ProductDto() { Name = "Test" };
      var entity = Mapper.Map<ProductDto, Product>(productDto);

      _productService.Update(productDto);

      _uow.Received().Products.Update(entity);
      _uow.Received().Complete();
    }

    [Test]
    public void productService_GetAll_CountEqualTo2()
    {
        _uow.Products.GetAll().Returns(productList);

        Assert.That(_productService.GetAll().Count, Is.EqualTo(2));
    }

    [Test]
    public void productService_GetAllActive_CountEqualTo1()
    {
        _uow.Products.GetAll(Arg.Any<Expression<Func<Product, bool>>>()).Returns(productList.Where(e => e.IsDeleted == false).ToList());

        Assert.That(_productService.GetAllActive().Count, Is.EqualTo(1));
    }
  }
}
