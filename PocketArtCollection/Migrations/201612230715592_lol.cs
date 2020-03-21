namespace PocketArtCollection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PieceOfArts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Artist = c.String(),
                        Title = c.String(),
                        Period = c.String(),
                        Techinque = c.String(),
                        Description = c.String(),
                        DateOfCreation = c.Int(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PieceOfArts");
        }
    }
}
