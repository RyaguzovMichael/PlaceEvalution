using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LastExamBackEndProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "VARCHAR(100)", nullable: false, defaultValue: ""),
                    Description = table.Column<string>(type: "VARCHAR(300)", nullable: false, defaultValue: ""),
                    TitlePhotoLink = table.Column<string>(type: "VARCHAR(100)", nullable: false, defaultValue: ""),
                    Rate = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    Photos = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()"),
                    LastModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rate = table.Column<decimal>(type: "numeric(5,2)", nullable: false, defaultValue: 0m),
                    ReviewText = table.Column<string>(type: "VARCHAR(300)", nullable: false, defaultValue: ""),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()"),
                    PlaceId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()"),
                    LastModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PlaceId",
                table: "Reviews",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
