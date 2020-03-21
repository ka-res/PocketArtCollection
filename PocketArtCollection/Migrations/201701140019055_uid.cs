namespace PocketArtCollection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PieceOfArts", "userID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PieceOfArts", "userID");
        }
    }
}
