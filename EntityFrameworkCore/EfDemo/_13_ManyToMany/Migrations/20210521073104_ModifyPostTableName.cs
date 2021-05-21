using Microsoft.EntityFrameworkCore.Migrations;

namespace _13_ManyToMany.Migrations
{
    public partial class ModifyPostTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTagBinding_Students_PostsPostId",
                table: "PostTagBinding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Posts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTagBinding_Posts_PostsPostId",
                table: "PostTagBinding",
                column: "PostsPostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTagBinding_Posts_PostsPostId",
                table: "PostTagBinding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Students");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTagBinding_Students_PostsPostId",
                table: "PostTagBinding",
                column: "PostsPostId",
                principalTable: "Students",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
