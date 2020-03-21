namespace PocketArtCollection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ueml : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceOfArts", "usersEmail", c => c.String());
            DropColumn("dbo.PieceOfArts", "userID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PieceOfArts", "userID", c => c.String());
            DropColumn("dbo.PieceOfArts", "usersEmail");
        }
    }
}
