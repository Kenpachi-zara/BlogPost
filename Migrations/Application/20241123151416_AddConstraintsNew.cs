using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogPost.Migrations.Application
{
    /// <inheritdoc />
    public partial class AddConstraintsNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Topic",
                columns: new[] { "TopicId", "Name" },
                values: new object[,]
                {
                    { new Guid("255e4b03-dbf0-4d1b-b6d4-4f894c785ea8"), "Tech" },
                    { new Guid("30077f43-2d78-470d-844b-03d8dfe07c84"), "Science" },
                    { new Guid("6f7fd266-af92-4281-bf37-c2db7166e596"), "Natural" },
                    { new Guid("a4d23bb2-8be8-4ef4-891b-91fbb88b9356"), "Religion" },
                    { new Guid("e532cc4b-44be-4ff3-a948-b5488dc2b15d"), "Life" },
                    { new Guid("e60e65d0-9f58-4996-9d25-bd24be81354a"), "Health" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("255e4b03-dbf0-4d1b-b6d4-4f894c785ea8"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("30077f43-2d78-470d-844b-03d8dfe07c84"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("6f7fd266-af92-4281-bf37-c2db7166e596"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("a4d23bb2-8be8-4ef4-891b-91fbb88b9356"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("e532cc4b-44be-4ff3-a948-b5488dc2b15d"));

            migrationBuilder.DeleteData(
                table: "Topic",
                keyColumn: "TopicId",
                keyValue: new Guid("e60e65d0-9f58-4996-9d25-bd24be81354a"));

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
        }
    }
}
