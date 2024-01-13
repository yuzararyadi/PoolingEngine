using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolingEngine.DataAccess.Context.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "TagItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestPoolingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    DeviceItemId = table.Column<int>(type: "int", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagDef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceItemId = table.Column<int>(type: "int", nullable: true),
                    TagItemId = table.Column<int>(type: "int", nullable: true),
                    MapAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagDef_DeviceItems_DeviceItemId",
                        column: x => x.DeviceItemId,
                        principalTable: "DeviceItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TagDef_TagItems_TagItemId",
                        column: x => x.TagItemId,
                        principalTable: "TagItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestItemTagGroup",
                columns: table => new
                {
                    RequestItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItemTagGroup", x => new { x.RequestItemsId, x.TagGroupsId });
                    table.ForeignKey(
                        name: "FK_RequestItemTagGroup_RequestItem_RequestItemsId",
                        column: x => x.RequestItemsId,
                        principalTable: "RequestItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestItemTagGroup_TagGroups_TagGroupsId",
                        column: x => x.TagGroupsId,
                        principalTable: "TagGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestItemTagGroup_TagGroupsId",
                table: "RequestItemTagGroup",
                column: "TagGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDef_DeviceItemId",
                table: "TagDef",
                column: "DeviceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDef_TagItemId",
                table: "TagDef",
                column: "TagItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestItemTagGroup");

            migrationBuilder.DropTable(
                name: "TagDef");

            migrationBuilder.DropTable(
                name: "RequestItem");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "TagItems");
        }
    }
}
