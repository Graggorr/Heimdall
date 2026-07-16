using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heimdall.Modules.Users.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HashedPassword = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Salt = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Profile_UserName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Profile_FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Profile_LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Profile_DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Profile_Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    Profile_PassportNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Address_MainAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Address_SecondaryAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Address_PostalCode = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Address_CountryCode = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    Address_City = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Profile_Email",
                schema: "users",
                table: "Users",
                column: "Profile_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Profile_UserName",
                schema: "users",
                table: "Users",
                column: "Profile_UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "users");
        }
    }
}
