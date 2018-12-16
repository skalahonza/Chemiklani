namespace Chemiklani.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolTeam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "School", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "School");
        }
    }
}
