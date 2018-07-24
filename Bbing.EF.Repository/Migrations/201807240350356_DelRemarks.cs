namespace Bbing.EF.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelRemarks : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "Remarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Remarks", c => c.String());
        }
    }
}
