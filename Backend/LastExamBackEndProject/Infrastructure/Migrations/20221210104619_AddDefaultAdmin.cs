using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LastExamBackEndProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "LastModifiedDate", "Login", "Name", "Password", "Role", "Surname" },
                values: new object[] { 1, new DateTime(2022, 12, 10, 16, 46, 19, 820, DateTimeKind.Local).AddTicks(2874), new DateTime(2022, 12, 10, 16, 46, 19, 820, DateTimeKind.Local).AddTicks(2883), "admin", "Admin", "AEKstK7/vilBN0wsQiRjnqfWUBHrcL04h5eubvDu/+HGs/Z8Hh4oKYuOTPngeebj1g==", 1, "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
