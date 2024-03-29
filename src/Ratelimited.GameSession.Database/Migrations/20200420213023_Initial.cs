﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Ratelimited.GameSession.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Auths",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: false),
                    Salt = table.Column<string>(maxLength: 500, nullable: false),
                    Roles = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Surname = table.Column<string>(maxLength: 200, nullable: true),
                    Email = table.Column<string>(maxLength: 300, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AuthId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Auths_AuthId",
                        column: x => x.AuthId,
                        principalSchema: "Auth",
                        principalTable: "Auths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Auths",
                columns: new[] { "Id", "Login", "Password", "Roles", "Salt" },
                values: new object[] { 1L, "admin", "Mai9hoshaiTei5eiEefuth4eTa2eiphuohTei6er", 3, "876BC27F-E2E2-4F2F-8436-4F8BD4890705" });

            migrationBuilder.InsertData(
                schema: "User",
                table: "Users",
                columns: new[] { "Id", "AuthId", "Status", "Email", "Name", "Surname" },
                values: new object[] { 1L, 1L, 1, "administrator@administrator.com", "Administrator", "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_Auths_Login",
                schema: "Auth",
                table: "Auths",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "User",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthId",
                schema: "User",
                table: "Users",
                column: "AuthId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Auths",
                schema: "Auth");
        }
    }
}
