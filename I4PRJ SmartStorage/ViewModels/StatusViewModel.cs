﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I4PRJ_SmartStorage.Models.Domain;

namespace I4PRJ_SmartStorage.ViewModels
{
    public class StatusViewModel
    {
        public Status Status { get; set; }

        public List<Inventory> Inventories { get; set; }

        public List<int> StatusStartedInventories { get; set; }
    }
}