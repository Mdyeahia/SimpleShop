namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductUnitrecheck : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Unit", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Unit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
