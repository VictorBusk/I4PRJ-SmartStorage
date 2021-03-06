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

namespace UnitTests.Services
{
  [TestFixture]
  class WholesalerServiceUnitTest
  {
    private IUnitOfWork _uow;
    private WholesalerService _wholesalerService;
    private List<Wholesaler> wholesalerList;


    [SetUp]
    public void SetUp()
    {
      _uow = Substitute.For<IUnitOfWork>();
      Mapper.Initialize(c => c.AddProfile<MappingProfile>());
      _wholesalerService = new WholesalerService(_uow);

      wholesalerList = new List<Wholesaler>
            {
          new Wholesaler()
          {
              ByUser = "Test",
              IsDeleted = false,
              WholesalerId = 1,
              Name = "Test",
              Updated = DateTime.Now
          },
           new Wholesaler()
          {
              ByUser = "Test",
              IsDeleted = true,
              WholesalerId = 1,
              Name = "Test",
              Updated = DateTime.Now
          }
      };
    }

    [Test]
    public void WholesalerServiceAdd_UnitOfWorkAddAndComplete_ReturnsUnitOfWorkAddAndComplete()
    {
      var wholesalerDto = new WholesalerDto() { Name = "Test" };
      var entity = Mapper.Map<WholesalerDto, Wholesaler>(wholesalerDto);

      _wholesalerService.Add(wholesalerDto);

      _uow.Received().Wholesalers.Add(entity);
      _uow.Received().Complete();
    }

    [Test]
    public void WholesalerServiceUpdate_UnitOfWorkUpdateAndComplete_ReturnsUnitOfWorkUpdateAndComplete()
    {
      var wholesalerDto = new WholesalerDto() { Name = "Test" };
      var entity = Mapper.Map<WholesalerDto, Wholesaler>(wholesalerDto);

      _wholesalerService.Update(wholesalerDto);

      _uow.Received().Wholesalers.Update(entity);
      _uow.Received().Complete();
    }

    [Test]
    public void SuppllierService_GetAll_CountEqualTo2()
    {
      _uow.Wholesalers.GetAll().Returns(wholesalerList);

      Assert.That(_wholesalerService.GetAll().Count, Is.EqualTo(2));
    }

    [Test]
    public void wholesalerService_GetAllActive_CountEqualTo1()
    {
      _uow.Wholesalers.GetAll(Arg.Any<Expression<Func<Wholesaler, bool>>>()).Returns(wholesalerList.Where(e => e.IsDeleted == false).ToList());

      Assert.That(_wholesalerService.GetAllActive().Count, Is.EqualTo(1));
    }

    [Test]
    public void WholesalerService_GetSingle_ReturnsWholesaler1()
    {
      var entityDto = Mapper.Map<Wholesaler, WholesalerDto>(wholesalerList[0]);
      _uow.Wholesalers.Get(1).Returns(wholesalerList[0]);

      Assert.That(_wholesalerService.GetSingle(1).WholesalerId, Is.EqualTo(entityDto.WholesalerId));
    }

    [Test]
    public void WholesalerServiceDelete_UnitOfWorkDeleteAndComplete_ReturnsUnitOfWorkDeleteAndComplete()
    {
      var wholesaler = new Wholesaler() { Name = "Test" };
      _uow.Wholesalers.Get(1).Returns(wholesaler);

      _wholesalerService.Delete(1);

      _uow.Received().Wholesalers.Update(wholesaler);
      _uow.Received().Complete();
      Assert.That(wholesaler.IsDeleted, Is.EqualTo(true));
    }
  }
}
