namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryAndJoiningTableWithNote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Note", "Category_CategoryId", c => c.Int());
            CreateIndex("dbo.Note", "Category_CategoryId");
            AddForeignKey("dbo.Note", "Category_CategoryId", "dbo.Category", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "Category_CategoryId", "dbo.Category");
            DropIndex("dbo.Note", new[] { "Category_CategoryId" });
            DropColumn("dbo.Note", "Category_CategoryId");
            DropTable("dbo.Category");
        }
    }
}
