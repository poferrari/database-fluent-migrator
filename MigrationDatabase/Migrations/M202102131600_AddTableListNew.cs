using FluentMigrator;

namespace MigrationDatabase.Migrations
{
    [Migration(202102131600, "Add Address List New")]
    public class M202102131600_AddTableListNew : Migration
    {
        public override void Down()
        {
            Delete.Table("ListNew");
        }

        public override void Up()
        {
            Create.Table("ListNew")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable();
        }
    }
}
