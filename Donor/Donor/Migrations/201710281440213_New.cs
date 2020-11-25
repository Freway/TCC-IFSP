namespace Donor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Doacao", new[] { "idTipoDoacao" });
            CreateIndex("dbo.Doacao", "IdTipoDoacao");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Doacao", new[] { "IdTipoDoacao" });
            CreateIndex("dbo.Doacao", "idTipoDoacao");
        }
    }
}
