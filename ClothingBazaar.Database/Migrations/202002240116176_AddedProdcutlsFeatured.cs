namespace ClothingBazaar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProdcutlsFeatured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "AddedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Products", "IsFeatured");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "IsFeatured", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "AddedOn");
        }
    }
}
