using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoolingEngine.DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagDef_DeviceItems_DeviceItemId",
                table: "TagDef");

            migrationBuilder.DropForeignKey(
                name: "FK_TagDef_TagItems_TagItemId",
                table: "TagDef");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagDef",
                table: "TagDef");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "TagItems");

            migrationBuilder.RenameTable(
                name: "TagDef",
                newName: "TagDefs");

            migrationBuilder.RenameIndex(
                name: "IX_TagDef_TagItemId",
                table: "TagDefs",
                newName: "IX_TagDefs_TagItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TagDef_DeviceItemId",
                table: "TagDefs",
                newName: "IX_TagDefs_DeviceItemId");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceItemId",
                table: "RequestItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagDefs",
                table: "TagDefs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagDefs_DeviceItems_DeviceItemId",
                table: "TagDefs",
                column: "DeviceItemId",
                principalTable: "DeviceItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagDefs_TagItems_TagItemId",
                table: "TagDefs",
                column: "TagItemId",
                principalTable: "TagItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagDefs_DeviceItems_DeviceItemId",
                table: "TagDefs");

            migrationBuilder.DropForeignKey(
                name: "FK_TagDefs_TagItems_TagItemId",
                table: "TagDefs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagDefs",
                table: "TagDefs");

            migrationBuilder.RenameTable(
                name: "TagDefs",
                newName: "TagDef");

            migrationBuilder.RenameIndex(
                name: "IX_TagDefs_TagItemId",
                table: "TagDef",
                newName: "IX_TagDef_TagItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TagDefs_DeviceItemId",
                table: "TagDef",
                newName: "IX_TagDef_DeviceItemId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "TagItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeviceItemId",
                table: "RequestItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagDef",
                table: "TagDef",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagDef_DeviceItems_DeviceItemId",
                table: "TagDef",
                column: "DeviceItemId",
                principalTable: "DeviceItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagDef_TagItems_TagItemId",
                table: "TagDef",
                column: "TagItemId",
                principalTable: "TagItems",
                principalColumn: "Id");
        }
    }
}
