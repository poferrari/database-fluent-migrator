using FluentMigrator;

namespace MigrationDatabase.Migrations
{
    [Migration(202102021509, "Add Address Table")]
    public class M202102021509_AddAddressTable : Migration
    {
        public override void Down()
        {
            Delete.Index("IX_Address_AcoesId").OnTable("Address");
            Delete.Table("Address");
        }

        public override void Up()
        {
            Create.Table("Address")
                  .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                  .WithColumn("Street").AsString().NotNullable()
                  .WithColumn("City").AsString().NotNullable()
                  .WithColumn("State").AsString().NotNullable()
                  .WithColumn("Zip").AsString().NotNullable()
                  .WithColumn("AcoesId").AsInt32().NotNullable().ForeignKey("Acoes", "Id");

            Create.Index("IX_Address_AcoesId")
                  .OnTable("Address")
                  .OnColumn("AcoesId")
                  .Ascending()
                  .WithOptions().NonClustered();
        }
    }
}
