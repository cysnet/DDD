namespace Bbing.EF.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        ParentId = c.String(),
                        Posts_Id = c.Int(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Posts_Id, cascadeDelete: true)
                .Index(t => t.Posts_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Introduce = c.String(),
                        UserName = c.String(),
                        Content = c.String(),
                        Type = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        LastModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Praises",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Posts_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Posts_Id, cascadeDelete: true)
                .Index(t => t.Posts_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Mobile = c.String(),
                        HeadUrl = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        LastModifyTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Praises", "Posts_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Posts_Id", "dbo.Posts");
            DropIndex("dbo.Praises", new[] { "Posts_Id" });
            DropIndex("dbo.Comments", new[] { "Posts_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Praises");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
        }
    }
}
