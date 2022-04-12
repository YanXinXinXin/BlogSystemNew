namespace BlogSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_User_UserType3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserType");
        }
    }
}
