using Microsoft.EntityFrameworkCore.Migrations;

namespace _13_ManyToMany.Migrations
{
    public partial class ModifyBindingTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Students_PostsPostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagsTagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostTagBinding");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagsTagId",
                table: "PostTagBinding",
                newName: "IX_PostTagBinding_TagsTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTagBinding",
                table: "PostTagBinding",
                columns: new[] { "PostsPostId", "TagsTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTagBinding_Students_PostsPostId",
                table: "PostTagBinding",
                column: "PostsPostId",
                principalTable: "Students",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTagBinding_Tags_TagsTagId",
                table: "PostTagBinding",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTagBinding_Students_PostsPostId",
                table: "PostTagBinding");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTagBinding_Tags_TagsTagId",
                table: "PostTagBinding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTagBinding",
                table: "PostTagBinding");

            migrationBuilder.RenameTable(
                name: "PostTagBinding",
                newName: "PostTag");

            migrationBuilder.RenameIndex(
                name: "IX_PostTagBinding_TagsTagId",
                table: "PostTag",
                newName: "IX_PostTag_TagsTagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                columns: new[] { "PostsPostId", "TagsTagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Students_PostsPostId",
                table: "PostTag",
                column: "PostsPostId",
                principalTable: "Students",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagsTagId",
                table: "PostTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
