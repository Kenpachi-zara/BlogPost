using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogPost.Migrations.Application
{
    /// <inheritdoc />
    public partial class TopicInitAndColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Topic",
                newName: "Name");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Topic",
                newName: "name");
        }
    }
}
