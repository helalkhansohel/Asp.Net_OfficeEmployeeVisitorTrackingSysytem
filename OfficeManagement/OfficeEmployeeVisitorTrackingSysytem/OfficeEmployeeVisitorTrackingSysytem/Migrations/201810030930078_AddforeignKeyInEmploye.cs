namespace OfficeEmployeeVisitorTrackingSysytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddforeignKeyInEmploye : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HotDesk", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Office", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Visitor", "EmployeeId", "dbo.Employee");
            DropIndex("dbo.Employee", new[] { "CompanyID" });
            DropPrimaryKey("dbo.Employee");
            AlterColumn("dbo.Employee", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Employee", "CompanyID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Employee", "Id");
            CreateIndex("dbo.Employee", "CompanyID");
            AddForeignKey("dbo.HotDesk", "EmployeeId", "dbo.Employee", "Id");
            AddForeignKey("dbo.Office", "EmployeeId", "dbo.Employee", "Id");
            AddForeignKey("dbo.Visitor", "EmployeeId", "dbo.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitor", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Office", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.HotDesk", "EmployeeId", "dbo.Employee");
            DropIndex("dbo.Employee", new[] { "CompanyID" });
            DropPrimaryKey("dbo.Employee");
            AlterColumn("dbo.Employee", "CompanyID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Employee", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Employee", "Id");
            CreateIndex("dbo.Employee", "CompanyID");
            AddForeignKey("dbo.Visitor", "EmployeeId", "dbo.Employee", "Id");
            AddForeignKey("dbo.Office", "EmployeeId", "dbo.Employee", "Id");
            AddForeignKey("dbo.HotDesk", "EmployeeId", "dbo.Employee", "Id");
        }
    }
}
