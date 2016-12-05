﻿using System;

namespace I4PRJ_SmartStorage.BLL.Interfaces.Dtos
{
  public interface ISupplierDto
  {
    string ByUser { get; set; }
    bool IsDeleted { get; set; }
    string Name { get; set; }
    int SupplierId { get; set; }
    DateTime Updated { get; set; }
  }
}