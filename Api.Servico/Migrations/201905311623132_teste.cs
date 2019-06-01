namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DscCPF = c.String(),
                        DscNome = c.String(),
                        DscTelCelular = c.String(),
                        DscEmail = c.String(),
                        DscEndereco = c.String(),
                        DscCEP = c.String(),
                        DscBairro = c.String(),
                        DscMunicipio = c.String(),
                        DscUF = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pessoas");
        }
    }
}
