using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cms_user_management");

            migrationBuilder.CreateTable(
                name: "membership",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_membership", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cms_admin_access = table.Column<bool>(type: "boolean", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    abouth = table.Column<string>(type: "text", nullable: false),
                    profile_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    prefer_nickname_over_name = table.Column<bool>(type: "boolean", nullable: false),
                    external_id = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_membership_history",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    membership_id = table.Column<Guid>(type: "uuid", nullable: false),
                    change_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_membership_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_membership_history_membership_membership_id",
                        column: x => x.membership_id,
                        principalSchema: "cms_user_management",
                        principalTable: "membership",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_membership_history_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "cms_user_management",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role_history",
                schema: "cms_user_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    function_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    function_end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_history_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "cms_user_management",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_history_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "cms_user_management",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "cms_user_management",
                table: "membership",
                columns: new[] { "id", "status" },
                values: new object[,]
                {
                    { new Guid("80126b05-9dab-4709-aa6a-39baa5bafe79"), "Active" },
                    { new Guid("a7c0bea2-2812-40b6-9836-d4b5accae95a"), "Archive" },
                    { new Guid("cfaeecff-d26b-44f2-bfa1-c80ab79983a9"), "Postponed" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_membership_id",
                schema: "cms_user_management",
                table: "membership",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_roles_id",
                schema: "cms_user_management",
                table: "roles",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_firstname_lastname_nickname",
                schema: "cms_user_management",
                table: "user",
                columns: new[] { "firstname", "lastname", "nickname" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_id",
                schema: "cms_user_management",
                table: "user",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_membership_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "membership_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_membership_history_user_id",
                schema: "cms_user_management",
                table: "user_membership_history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_role_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_history_user_id",
                schema: "cms_user_management",
                table: "user_role_history",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_membership_history",
                schema: "cms_user_management");

            migrationBuilder.DropTable(
                name: "user_role_history",
                schema: "cms_user_management");

            migrationBuilder.DropTable(
                name: "membership",
                schema: "cms_user_management");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "cms_user_management");

            migrationBuilder.DropTable(
                name: "user",
                schema: "cms_user_management");
        }
    }
}
