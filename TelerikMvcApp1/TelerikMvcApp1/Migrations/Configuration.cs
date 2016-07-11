//namespace TelerikMvcApp1.Migrations
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Data.Entity;
//    using System.Data.Entity.Migrations;
//    using System.Linq;
//    using TelerikMvcApp1.Models;

//    internal sealed class Configuration : DbMigrationsConfiguration<TelerikMvcApp1.Models.DocContext>
//    {
//        public Configuration()
//        {
//            AutomaticMigrationDataLossAllowed = true;
//            AutomaticMigrationsEnabled = true;
//            ContextKey = "TelerikMvcApp1.Models.DocContext";

//        }

//        protected override void Seed(TelerikMvcApp1.Models.DocContext context)
//        {
//            var r = new Random();

//            var categories = new List<Category>();
//            for (int i = 1; i <= 10; i++)
//            {
//                categories.Add(new Category { Id = i, Name = "ProdSubCat_" + i });
//            }
//            categories.ForEach(c => context.Categories.Add(c));
//            context.SaveChanges();

//            var documents = new List<Document>();
//            for (int i = 1; i <= 50; i++)
//            {
//                documents.Add(new Document { Id = i, Name = "Doc_" + i, CategoryId = r.Next(1, 10), Status = (Status)r.Next(3), Upload_date = DateTime.Now.AddDays(-i), Description = String.Format("Document #{0}, last update = {1}", i, DateTime.Now.AddDays(-i).ToShortDateString()) });
//            }
//            documents.ForEach(d => context.Documents.Add(d));
//            context.SaveChanges();

//            //  This method will be called after migrating to the latest version.

//            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//            //  to avoid creating duplicate seed data. E.g.
//            //
//            //    context.People.AddOrUpdate(
//            //      p => p.FullName,
//            //      new Person { FullName = "Andrew Peters" },
//            //      new Person { FullName = "Brice Lambson" },
//            //      new Person { FullName = "Rowan Miller" }
//            //    );
//            //

//        }
//    }
//}
