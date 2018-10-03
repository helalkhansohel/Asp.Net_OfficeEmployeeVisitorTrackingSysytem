namespace OfficeEmployeeVisitorTrackingSysytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAdminCompanyInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admin", "Company", c => c.String());
            AddColumn("dbo.Admin", "Address", c => c.String());
            AddColumn("dbo.Admin", "Phone", c => c.String());
            AddColumn("dbo.Admin", "Details", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admin", "Details");
            DropColumn("dbo.Admin", "Phone");
            DropColumn("dbo.Admin", "Address");
            DropColumn("dbo.Admin", "Company");
        }
    }
}
