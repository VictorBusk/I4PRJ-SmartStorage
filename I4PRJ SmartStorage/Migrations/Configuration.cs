using I4PRJ_SmartStorage.Models.Domain;

namespace I4PRJ_SmartStorage.Migrations
{
  using System;
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<I4PRJ_SmartStorage.Models.ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(I4PRJ_SmartStorage.Models.ApplicationDbContext context)
    {
      context.Inventories.AddOrUpdate(i => i.Name,
          new Inventory { Name = "Main storage", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Inventory { Name = "Bar 1", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Inventory { Name = "Bar 2", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Inventory { Name = "Hostess station", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false }
          );

      context.Categories.AddOrUpdate(c => c.Name,
          new Category { Name = "Liquor", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Category { Name = "Beer", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Category { Name = "Soft Drink", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Category { Name = "Champange", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false }
      );

      context.Suppliers.AddOrUpdate(s => s.Name,
          new Supplier { Name = "Liquor manufacturer", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Supplier { Name = "Beer manufacturer", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false }
      );

      context.Wholesalers.AddOrUpdate(w => w.Name,
          new Wholesaler { Name = "Liquor wholesaler", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false },
          new Wholesaler { Name = "Beer wholesaler", ByUser = "Dummy", Updated = DateTime.Now, IsDeleted = false }
      );

      //context.Users.AddOrUpdate(u => u.Email,
      //  new ApplicationUser
      //  {
      //    UserName = "no-reply@smartstorage.dk",
      //    Email = "no-reply@smartstorage.dk",
      //    FullName = "Admin",
      //    PhoneNumber = "12345678",
      //    ProfilePicture = "/Content/images/rubber-duck.png",
      //    EmailConfirmed = true,
      //    LockoutEnabled = false,
      //    PasswordHash = "APzG0CpFDA+zlGKwXm1atHlBEQN62++rT9aMmhPjCUV88nkAy4sqOf7W+ELWQWMG0g==",
      //    SecurityStamp = "b9d672a6-7ceb-4f69-a7d9-1a428610731d"
      //  });
    }
  }
}
