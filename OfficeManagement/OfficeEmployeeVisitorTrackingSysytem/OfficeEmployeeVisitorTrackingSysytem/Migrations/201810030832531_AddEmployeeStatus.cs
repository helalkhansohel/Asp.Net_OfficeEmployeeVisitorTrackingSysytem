namespace OfficeEmployeeVisitorTrackingSysytem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Status");
        }
    }
}
