namespace Chemiklani.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TeamCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "Category", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "Category");
        }
    }
}
