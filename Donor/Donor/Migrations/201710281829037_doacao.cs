namespace Donor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doacao", "Excluido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doacao", "Excluido");
        }
    }
}
