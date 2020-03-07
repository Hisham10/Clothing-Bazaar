namespace ClothingBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductIsFeatured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsFeatured");
        }
    }
}
