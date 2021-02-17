using FluentMigrator;

namespace CmdLine.DataAccess.Migrations
{
    [Migration(201109100935, "Add Address List")]
    public class M201109100935_AddTableList : Migration
    {
        public override void Down()
        {
            Delete.Table("List");
        }

        public override void Up()
        {
            Create.Table("List")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString().NotNullable();
        }
    }
}
