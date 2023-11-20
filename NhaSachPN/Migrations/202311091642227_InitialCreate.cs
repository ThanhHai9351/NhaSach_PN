namespace NhaSachPN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        AuthorName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Evaluates",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(),
                        ProductEvaluate = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(),
                        Price = c.Long(),
                        Quantity = c.Long(),
                        Image = c.String(),
                        Date = c.DateTime(nullable: false),
                        PublisherID = c.Long(),
                        CategoryID = c.Long(),
                        AuthorID = c.Long(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Authors", t => t.AuthorID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Publishers", t => t.PublisherID)
                .Index(t => t.PublisherID)
                .Index(t => t.CategoryID)
                .Index(t => t.AuthorID);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PublisherName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.PurchaseVouchers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(),
                        Quantity = c.Long(nullable: false),
                        Money = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseVouchers", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Favourites", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Evaluates", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "PublisherID", "dbo.Publishers");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Products", "AuthorID", "dbo.Authors");
            DropIndex("dbo.PurchaseVouchers", new[] { "ProductID" });
            DropIndex("dbo.Favourites", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "AuthorID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "PublisherID" });
            DropIndex("dbo.Evaluates", new[] { "ProductID" });
            DropTable("dbo.PurchaseVouchers");
            DropTable("dbo.Favourites");
            DropTable("dbo.Publishers");
            DropTable("dbo.Products");
            DropTable("dbo.Evaluates");
            DropTable("dbo.Categories");
            DropTable("dbo.Authors");
        }
    }
}
