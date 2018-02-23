namespace Chemiklani.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TeamsRooms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "Room", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "Room");
        }
    }
}
