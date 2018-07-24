namespace Bbing.EF.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Re", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Re");
        }
    }
}
