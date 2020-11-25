namespace Donor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pontodedoacao : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PontoDeDoacao", "Senha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PontoDeDoacao", "Senha", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
