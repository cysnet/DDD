namespace Bbing.EF.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelRe : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "Re");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Re", c => c.String());
        }
    }
}
