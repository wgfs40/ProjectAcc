namespace ProjectEng.Repositories.Migrations
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
                        ID = c.Int(nullable: false, identity: true),
                        TaskID = c.Int(nullable: false),
                        Comments = c.String(nullable: false, unicode: false, storeType: "text"),
                        Stage = c.Short(nullable: false),
                        Username = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Tasks", t => t.TaskID)
                .Index(t => t.TaskID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Description = c.String(unicode: false, storeType: "text"),
                        DueDate = c.DateTime(),
                        CurrentState = c.Int(),
                        Create = c.DateTime(),
                        CompletedDate = c.DateTime(),
                        Responsable = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Ref = c.String(maxLength: 20),
                        CustomerId = c.Int(nullable: false),
                        Description = c.String(nullable: false, unicode: false, storeType: "text"),
                        Comments = c.String(),
                        Status = c.String(nullable: false, maxLength: 10),
                        DueDate = c.DateTime(nullable: false),
                        LaunchDate = c.DateTime(nullable: false),
                        Create = c.DateTime(),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.ProjectStaff",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        CMStaff = c.String(maxLength: 20),
                        PMStaff = c.String(maxLength: 20),
                        PDEStaff = c.String(maxLength: 20),
                        SEStaff = c.String(maxLength: 20),
                        QEStaff = c.String(maxLength: 20),
                        DRStaff = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Staff",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        StaffName = c.String(maxLength: 60),
                        StaffPosition = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "TaskID", "dbo.Tasks");
            DropForeignKey("dbo.Tasks", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectStaff", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ProjectStaff", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.Tasks", new[] { "ProjectID" });
            DropIndex("dbo.Comments", new[] { "TaskID" });
            DropTable("dbo.Users");
            DropTable("dbo.Staff");
            DropTable("dbo.ProjectStaff");
            DropTable("dbo.Customers");
            DropTable("dbo.Projects");
            DropTable("dbo.Tasks");
            DropTable("dbo.Comments");
        }
    }
}
