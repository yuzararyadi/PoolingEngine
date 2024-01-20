using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolingEngine.DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestPoolingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    DeviceItemId = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceItemId = table.Column<int>(type: "int", nullable: false),
                    TagItemId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceItemTagGroup",
                columns: table => new
                {
                    TagGroupsId = table.Column<int>(type: "int", nullable: false),
                    deviceItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceItemTagGroup", x => new { x.TagGroupsId, x.deviceItemsId });
                    table.ForeignKey(
                        name: "FK_DeviceItemTagGroup_DeviceItems_deviceItemsId",
                        column: x => x.deviceItemsId,
                        principalTable: "DeviceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceItemTagGroup_TagGroups_TagGroupsId",
                        column: x => x.TagGroupsId,
                        principalTable: "TagGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "TagDefs",
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
                    table.PrimaryKey("PK_TagDefs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagDefs_DeviceItems_DeviceItemId",
                        column: x => x.DeviceItemId,
                        principalTable: "DeviceItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TagDefs_TagItems_TagItemId",
                        column: x => x.TagItemId,
                        principalTable: "TagItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagGroupTagItem",
                columns: table => new
                {
                    TagGroupsId = table.Column<int>(type: "int", nullable: false),
                    TagItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagGroupTagItem", x => new { x.TagGroupsId, x.TagItemsId });
                    table.ForeignKey(
                        name: "FK_TagGroupTagItem_TagGroups_TagGroupsId",
                        column: x => x.TagGroupsId,
                        principalTable: "TagGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagGroupTagItem_TagItems_TagItemsId",
                        column: x => x.TagItemsId,
                        principalTable: "TagItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceItemTagGroup_deviceItemsId",
                table: "DeviceItemTagGroup",
                column: "deviceItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItemTagGroup_TagGroupsId",
                table: "RequestItemTagGroup",
                column: "TagGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDefs_DeviceItemId",
                table: "TagDefs",
                column: "DeviceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TagDefs_TagItemId",
                table: "TagDefs",
                column: "TagItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupTagItem_TagItemsId",
                table: "TagGroupTagItem",
                column: "TagItemsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceItemTagGroup");

            migrationBuilder.DropTable(
                name: "RequestItemTagGroup");

            migrationBuilder.DropTable(
                name: "TagDefs");

            migrationBuilder.DropTable(
                name: "TagGroupTagItem");

            migrationBuilder.DropTable(
                name: "TagValues");

            migrationBuilder.DropTable(
                name: "RequestItem");

            migrationBuilder.DropTable(
                name: "DeviceItems");

            migrationBuilder.DropTable(
                name: "TagGroups");

            migrationBuilder.DropTable(
                name: "TagItems");
        }
    }
}
