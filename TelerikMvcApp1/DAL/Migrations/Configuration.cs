namespace DAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ModelDB> 
    {
        public Configuration()
        {            
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(ModelDB context)
        {
            //var r = new Random();
                        
            //var categories = new List<Category>();
            //for (int i = 1; i <= 10; i++)
            //{
            //    if(i == 1)
            //    {
            //       categories.Add(new Category { Id = i, Name = "ProdSubCat_" + i, LeftNode = 0, RightNode = i + 1 });
            //    }
            //    else if (i == 10)
            //    {
            //        categories.Add(new Category { Id = i, Name = "ProdSubCat_" + i, LeftNode = i - 1, RightNode = 0 });
            //    }
            //    else
            //    {
            //        categories.Add(new Category { Id = i, Name = "ProdSubCat_" + i, LeftNode = i - 1, RightNode = i + 1 });
            //    }
            //}
            //categories.ForEach(c => context.Categories.AddOrUpdate(c));
            //context.SaveChanges();

            //var documents = new List<Document>();
            //for (int i = 1; i <= 50; i++)
            //{
            //    documents.Add(new Document { Id = i, Name = "Doc_" + i, CategoryId = r.Next(1, 10), Status = (Status)r.Next(3), Upload_date = DateTime.Now.AddDays(-i), Description = String.Format("Document #{0}, last update = {1}", i, DateTime.Now.AddDays(-i).ToShortDateString()) });
            //}
            //documents.ForEach(d => context.Documents.AddOrUpdate(d));
            //context.SaveChanges();

        }
    }
}
