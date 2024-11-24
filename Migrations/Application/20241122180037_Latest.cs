using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogPost.Migrations.Application
{
    /// <inheritdoc />
    public partial class Latest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TopicsPosts_PostId",
                table: "TopicsPosts");

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("515e8f2f-a46d-4eb6-ad12-b4d8b4523ca7"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("65fc0014-b968-4be5-8619-55d81543f355"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("86f02aef-f65d-4bed-b587-600c7340a487"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("989c8e71-eb07-4da6-bb78-1b05b7833c9b"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("b37d995d-93a9-42d9-bcb8-771142ea3e5a"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("c9556c1a-c241-4f68-aa9f-430fc0196867"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Topic",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_TopicsPosts_PostId_TopicId",
                table: "TopicsPosts",
                columns: new[] { "PostId", "TopicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topic_Name",
                table: "Topic",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TopicsPosts_PostId_TopicId",
                table: "TopicsPosts");

            migrationBuilder.DropIndex(
                name: "IX_Topic_Name",
                table: "Topic");

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
                name: "Name",
                table: "Topic",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("515e8f2f-a46d-4eb6-ad12-b4d8b4523ca7"), "Life" },
                    { new Guid("65fc0014-b968-4be5-8619-55d81543f355"), "Health" },
                    { new Guid("86f02aef-f65d-4bed-b587-600c7340a487"), "Religion" },
                    { new Guid("989c8e71-eb07-4da6-bb78-1b05b7833c9b"), "Tech" },
                    { new Guid("b37d995d-93a9-42d9-bcb8-771142ea3e5a"), "Science" },
                    { new Guid("c9556c1a-c241-4f68-aa9f-430fc0196867"), "Natural" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicsPosts_PostId",
                table: "TopicsPosts",
                column: "PostId");
        }
    }
}
