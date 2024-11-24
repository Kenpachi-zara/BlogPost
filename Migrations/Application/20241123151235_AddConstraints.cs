using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogPost.Migrations.Application
{
    /// <inheritdoc />
    public partial class AddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("006a8560-e434-4579-b207-57280e22894c"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("0c9744c3-2732-49de-8308-5d80c33d7833"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("330a6ed8-79a6-4897-ad82-2c9dd6742fc4"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("56e385a4-3eca-4dd6-9544-f4b96d269380"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("5bc55cd3-b149-44cb-b5cf-bef7a5f132b1"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("7c38a8eb-6629-4464-8b58-c560bd793945"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Post",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("0cc0f4e3-281b-4331-9e64-86e03243e682"), "Science" },
                    { new Guid("2a630afa-3f9c-4363-a884-b3822e010133"), "Tech" },
                    { new Guid("57d3f271-9c64-4bf0-9306-b63b6e22db5e"), "Religion" },
                    { new Guid("60266022-840e-48cd-a425-01c22950e8b5"), "Natural" },
                    { new Guid("6e9735f7-6c7e-48b1-bafd-21d786ed83d4"), "Health" },
                    { new Guid("776ff9ad-f168-4552-892d-fb75c6958c3c"), "Life" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_Title",
                table: "Post",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Post_Title",
                table: "Post");

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("0cc0f4e3-281b-4331-9e64-86e03243e682"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("2a630afa-3f9c-4363-a884-b3822e010133"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("57d3f271-9c64-4bf0-9306-b63b6e22db5e"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("60266022-840e-48cd-a425-01c22950e8b5"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("6e9735f7-6c7e-48b1-bafd-21d786ed83d4"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("776ff9ad-f168-4552-892d-fb75c6958c3c"));

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Post");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("006a8560-e434-4579-b207-57280e22894c"), "Science" },
                    { new Guid("0c9744c3-2732-49de-8308-5d80c33d7833"), "Tech" },
                    { new Guid("330a6ed8-79a6-4897-ad82-2c9dd6742fc4"), "Natural" },
                    { new Guid("56e385a4-3eca-4dd6-9544-f4b96d269380"), "Health" },
                    { new Guid("5bc55cd3-b149-44cb-b5cf-bef7a5f132b1"), "Religion" },
                    { new Guid("7c38a8eb-6629-4464-8b58-c560bd793945"), "Life" }
                });
        }
    }
}
