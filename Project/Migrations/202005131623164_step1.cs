namespace Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "prodid", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "prodid" });
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Order_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_Id, t.Product_Id })
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id);
            
            DropColumn("dbo.Orders", "prodid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "prodid", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderProducts", new[] { "Product_Id" });
            DropIndex("dbo.OrderProducts", new[] { "Order_Id" });
            DropTable("dbo.OrderProducts");
            CreateIndex("dbo.Orders", "prodid");
            AddForeignKey("dbo.Orders", "prodid", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
