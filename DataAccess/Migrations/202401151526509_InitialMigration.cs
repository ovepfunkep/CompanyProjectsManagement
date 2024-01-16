namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Surname = c.String(maxLength: 100),
                        Patronymic = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Project_ID = c.Int(),
                        Company_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .Index(t => t.Project_ID)
                .Index(t => t.Company_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        DateStarted = c.DateTime(nullable: false),
                        DateEnded = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        ManagerID = c.Int(nullable: false),
                        CustomerCompanyID = c.Int(nullable: false),
                        ContractorCompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.ContractorCompanyID)
                .ForeignKey("dbo.Companies", t => t.CustomerCompanyID)
                .ForeignKey("dbo.Employees", t => t.ManagerID)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ManagerID)
                .Index(t => t.CustomerCompanyID)
                .Index(t => t.ContractorCompanyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.Projects", "ManagerID", "dbo.Employees");
            DropForeignKey("dbo.Employees", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CustomerCompanyID", "dbo.Companies");
            DropForeignKey("dbo.Projects", "ContractorCompanyID", "dbo.Companies");
            DropIndex("dbo.Projects", new[] { "ContractorCompanyID" });
            DropIndex("dbo.Projects", new[] { "CustomerCompanyID" });
            DropIndex("dbo.Projects", new[] { "ManagerID" });
            DropIndex("dbo.Projects", new[] { "Name" });
            DropIndex("dbo.Employees", new[] { "Company_ID" });
            DropIndex("dbo.Employees", new[] { "Project_ID" });
            DropIndex("dbo.Companies", new[] { "Name" });
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
        }
    }
}
