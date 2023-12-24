using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolingEngine.DataAccess.Context.Migrations
{
    public partial class InitialDbCreation : Migration
    {
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
                name: "TagValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    DeviceItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItems_DeviceItems_DeviceItemId",
                        column: x => x.DeviceItemId,
                        principalTable: "DeviceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "TagItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    RequestItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagItems_RequestItems_RequestItemId",
                        column: x => x.RequestItemId,
                        principalTable: "RequestItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeviceItemTagItem",
                columns: table => new
                {
                    DeviceItemsId = table.Column<int>(type: "int", nullable: false),
                    TagItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceItemTagItem", x => new { x.DeviceItemsId, x.TagItemsId });
                    table.ForeignKey(
                        name: "FK_DeviceItemTagItem_DeviceItems_DeviceItemsId",
                        column: x => x.DeviceItemsId,
                        principalTable: "DeviceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceItemTagItem_TagItems_TagItemsId",
                        column: x => x.TagItemsId,
                        principalTable: "TagItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_DeviceItemTagItem_TagItemsId",
                table: "DeviceItemTagItem",
                column: "TagItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_DeviceItemId",
                table: "RequestItems",
                column: "DeviceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupTagItem_TagItemsId",
                table: "TagGroupTagItem",
                column: "TagItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_TagItems_RequestItemId",
                table: "TagItems",
                column: "RequestItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceItemTagGroup");

            migrationBuilder.DropTable(
                name: "DeviceItemTagItem");

            migrationBuilder.DropTable(
                name: "TagGroupTagItem");

            migrationBuilder.DropTable(
                name: "TagValues");

            migrationBuilder.DropTable(
                name: "TagGroups");

            migrationBuilder.DropTable(
                name: "TagItems");

            migrationBuilder.DropTable(
                name: "RequestItems");

            migrationBuilder.DropTable(
                name: "DeviceItems");
        }
    }
}
