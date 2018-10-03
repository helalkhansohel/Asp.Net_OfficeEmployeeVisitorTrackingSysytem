namespace OfficeEmployeeVisitorTrackingSysytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OfficeNumber = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.HotDesk",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(),
                        CompanyId = c.Int(),
                        CarNumber = c.String(),
                        Email = c.String(),
                        Date = c.DateTime(),
                        LogInTime = c.DateTime(),
                        LogOutTime = c.DateTime(),
                        CurrentStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Office",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(),
                        Date = c.DateTime(),
                        LogInTime = c.DateTime(),
                        LogOutTime = c.DateTime(),
                        CurrentStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Visitor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CompanyId = c.Int(),
                        EmployeeId = c.Int(),
                        CarNumber = c.String(),
                        Email = c.String(),
                        LogInTime = c.DateTime(),
                        LogOutTime = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.CompanyId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Visitor", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Visitor", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Office", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.HotDesk", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.HotDesk", "CompanyId", "dbo.Company");
            DropIndex("dbo.Visitor", new[] { "EmployeeId" });
            DropIndex("dbo.Visitor", new[] { "CompanyId" });
            DropIndex("dbo.Office", new[] { "EmployeeId" });
            DropIndex("dbo.HotDesk", new[] { "CompanyId" });
            DropIndex("dbo.HotDesk", new[] { "EmployeeId" });
            DropIndex("dbo.Employee", new[] { "CompanyID" });
            DropTable("dbo.Visitor");
            DropTable("dbo.Office");
            DropTable("dbo.HotDesk");
            DropTable("dbo.Employee");
            DropTable("dbo.Company");
            DropTable("dbo.Admin");
        }
    }
}
