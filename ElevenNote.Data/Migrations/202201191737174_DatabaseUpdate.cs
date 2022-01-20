namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Category", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Category", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "ModifiedUtc");
            DropColumn("dbo.Category", "CreatedUtc");
            DropColumn("dbo.Category", "OwnerId");
        }
    }
}
