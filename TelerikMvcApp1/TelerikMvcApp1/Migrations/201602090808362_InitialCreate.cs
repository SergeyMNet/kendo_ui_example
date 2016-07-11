namespace TelerikMvcApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductSubCategories",
                c => new
                    {
                        ProductSubCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProductId = c.Int(nullable: false),
                        ProductCategory_ProductCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductSubCategoryId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_ProductCategoryId)
                .Index(t => t.ProductCategory_ProductCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSubCategories", "ProductCategory_ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.ProductSubCategories", new[] { "ProductCategory_ProductCategoryId" });
            DropTable("dbo.ProductSubCategories");
            DropTable("dbo.ProductCategories");
        }
    }
}
