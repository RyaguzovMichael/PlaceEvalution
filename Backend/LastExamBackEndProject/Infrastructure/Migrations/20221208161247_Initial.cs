using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LastExamBackEndProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "VARCHAR(100)", nullable: false, defaultValue: ""),
                    Password = table.Column<string>(type: "VARCHAR(100)", nullable: false, defaultValue: ""),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false, defaultValue: "Default"),
                    Surname = table.Column<string>(type: "VARCHAR(100)", nullable: false, defaultValue: "User"),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()"),
                    LastModifiedDate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
