using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Redi.Infrastructure.Persistence.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Stake = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocalStake = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Container_Container_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Container",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyCompanyProfitEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCompanyProfitEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyStakerProfitEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    StakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stake = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyStakerProfitEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StakerMembership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stake = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocalStake = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StakerMembership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StakerMembership_Container_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Container",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Container",
                columns: new[] { "Id", "LocalStake", "Name", "ParentId", "Stake", "Weight" },
                values: new object[] { new Guid("42cbb7d1-f9e6-481f-a157-a34ba1f4681b"), 1m, "ROOT", null, 1m, 1 });

            migrationBuilder.InsertData(
                table: "Staker",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "Role" },
                values: new object[] { new Guid("abf6128c-4ba0-4042-9fa1-8e0a7cda37a4"), "admin@domain.com", "system", "admin", "$2a$11$S.06zalTVFkc/h/zqXiKOOxWMgghVgILozOnliTcObRY/y0JhzEhC", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Container_ParentId",
                table: "Container",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyCompanyProfitEntry_Date",
                table: "DailyCompanyProfitEntry",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyStakerProfitEntry_Date",
                table: "DailyStakerProfitEntry",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_StakerMembership_ContainerId",
                table: "StakerMembership",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_StakerMembership_StakerId",
                table: "StakerMembership",
                column: "StakerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyCompanyProfitEntry");

            migrationBuilder.DropTable(
                name: "DailyStakerProfitEntry");

            migrationBuilder.DropTable(
                name: "Staker");

            migrationBuilder.DropTable(
                name: "StakerMembership");

            migrationBuilder.DropTable(
                name: "Container");
        }
    }
}
