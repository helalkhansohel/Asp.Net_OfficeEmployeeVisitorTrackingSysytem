namespace OfficeEmployeeVisitorTrackingSysytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCompanyIdtoOffice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Office", "CompanyId", c => c.Int());
            CreateIndex("dbo.Office", "CompanyId");
            AddForeignKey("dbo.Office", "CompanyId", "dbo.Company", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Office", "CompanyId", "dbo.Company");
            DropIndex("dbo.Office", new[] { "CompanyId" });
            DropColumn("dbo.Office", "CompanyId");
        }
    }
}
