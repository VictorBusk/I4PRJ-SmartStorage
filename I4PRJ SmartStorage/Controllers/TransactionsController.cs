﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using I4PRJ_SmartStorage.Models;
using I4PRJ_SmartStorage.Models.Domain;
using I4PRJ_SmartStorage.ViewModels;

namespace I4PRJ_SmartStorage.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var viewModel = new TransactionViewModel
            {
                Transactions =
                    db.Transactions.Include(t => t.Product)
                        .Include(t => t.FromInventory)
                        .Include(t => t.ToInventory)
                        .ToList()
            };

            return View(viewModel);
        }

        public ActionResult New(TransactionViewModel viewModel)
        {
            if (viewModel.IsChecked)
            {
                viewModel.Transaction = new Transaction();
                viewModel.ToInventory = db.Inventories.ToList();
                viewModel.Products = db.Products.ToList();

                return View("RestockForm", viewModel);
            }
            else
            {
                viewModel.Transaction = new Transaction();
                viewModel.FromInventory = db.Inventories.ToList();
                viewModel.ToInventory = db.Inventories.ToList();
                viewModel.Products = db.Products.ToList();

                return View("TransactionForm", viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveRestock(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new TransactionViewModel
                {
                    ToInventory = db.Inventories.ToList(),
                    Products = db.Products.ToList()
                };

                return View("RestockForm", viewModel);
            }

            var toStockInDb =
                db.Stocks.Include(s => s.Inventory)
                    .Include(s => s.Product)
                    .Where(s => s.InventoryId == transaction.ToInventoryId)
                    .SingleOrDefault(s => s.ProductId == transaction.ProductId);

            if (toStockInDb == null)
            {
                var toStock = new Stock
                {
                    InventoryId = transaction.ToInventoryId,
                    ProductId = transaction.ProductId,
                    Quantity = transaction.Quantity
                };

                var product = db.Products.Single(p => p.ProductId == transaction.ProductId);
                product.Stocks.Add(toStock);

                var inventory = db.Inventories.Single(p => p.InventoryId == transaction.ToInventoryId);
                inventory.Stocks.Add(toStock);

                db.Stocks.Add(toStock);
            }
            else
            {
                toStockInDb.Quantity += transaction.Quantity;
            }

            transaction.DateTime = DateTime.Now;
            transaction.ByUser = User.Identity.Name;

            db.Transactions.Add(transaction);

            db.SaveChanges();

            return RedirectToAction("Index", "Transactions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                var inventoriesInDb = db.Inventories.ToList();
                var productsInDb = db.Products.ToList();

                var viewModel = new TransactionViewModel
                {
                    FromInventory = inventoriesInDb,
                    ToInventory = inventoriesInDb,
                    Products = productsInDb
                };

                return View("TransactionForm", viewModel);
            }

            var fromStockInDb =
                db.Stocks.Include(s => s.Inventory)
                    .Include(s => s.Product)
                    .Where(s => s.InventoryId == transaction.FromInventoryId)
                    .SingleOrDefault(s => s.ProductId == transaction.ProductId);

            if (fromStockInDb == null)
            {
                var inventoriesInDb = db.Inventories.ToList();
                var productsInDb = db.Products.ToList();

                var viewModel = new TransactionViewModel
                {
                    FromInventory = inventoriesInDb,
                    ToInventory = inventoriesInDb,
                    Products = productsInDb
                };

                return View("TransactionForm", viewModel);
            }
            else
            {
                fromStockInDb.Quantity -= transaction.Quantity;
            }

            var toStockInDb =
                db.Stocks.Include(s => s.Inventory)
                    .Include(s => s.Product)
                    .Where(s => s.InventoryId == transaction.ToInventoryId)
                    .SingleOrDefault(s => s.ProductId == transaction.ProductId);

            if (toStockInDb == null)
            {
                var toStock = new Stock
                {
                    InventoryId = transaction.ToInventoryId,
                    ProductId = transaction.ProductId,
                    Quantity = transaction.Quantity
                };

                var product = db.Products.Single(p => p.ProductId == transaction.ProductId);
                product.Stocks.Add(toStock);

                var inventory = db.Inventories.Single(p => p.InventoryId == transaction.ToInventoryId);
                inventory.Stocks.Add(toStock);

                db.Stocks.Add(toStock);
            }
            else
            {
                toStockInDb.Quantity += transaction.Quantity;
            }

            transaction.DateTime = DateTime.Now;
            transaction.ByUser = User.Identity.Name;

            db.Transactions.Add(transaction);

            db.SaveChanges();

            return RedirectToAction("Index", "Transactions");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}