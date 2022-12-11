using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LastExamBackEndProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeInPlaceFieldsNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "Photos",
                table: "Places",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate", "Password" },
                values: new object[] { new DateTime(2022, 12, 11, 18, 54, 28, 615, DateTimeKind.Local).AddTicks(7889), new DateTime(2022, 12, 11, 18, 54, 28, 615, DateTimeKind.Local).AddTicks(7901), "AGfxHpE9T44aBAz7Ob8y2BNTrEy/ObPPQOtTZVYokpW5zjHbhom3CSjcG74yC6LurQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "Photos",
                table: "Places",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate", "Password" },
                values: new object[] { new DateTime(2022, 12, 10, 16, 46, 19, 820, DateTimeKind.Local).AddTicks(2874), new DateTime(2022, 12, 10, 16, 46, 19, 820, DateTimeKind.Local).AddTicks(2883), "AEKstK7/vilBN0wsQiRjnqfWUBHrcL04h5eubvDu/+HGs/Z8Hh4oKYuOTPngeebj1g==" });
        }
    }
}
