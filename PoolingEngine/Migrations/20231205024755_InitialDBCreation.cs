using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolingEngine.Migrations
{
    public partial class InitialDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DataType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestItemId = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: true),
                    TagId = table.Column<int>(type: "INTEGER", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItems_DeviceItems_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "DeviceItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestItems_TagItems_TagId",
                        column: x => x.TagId,
                        principalTable: "TagItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_DeviceId",
                table: "RequestItems",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_TagId",
                table: "RequestItems",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestItems");

            migrationBuilder.DropTable(
                name: "TagValues");

            migrationBuilder.DropTable(
                name: "DeviceItems");

            migrationBuilder.DropTable(
                name: "TagItems");
        }
    }
}
