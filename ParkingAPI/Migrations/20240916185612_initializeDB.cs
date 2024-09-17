using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ParkingAPI.Migrations
{
    /// <inheritdoc />
    public partial class initializeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FamilyName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    AltPhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residents_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LicensePlate = table.Column<string>(type: "TEXT", nullable: false),
                    Make = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpotId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsTaken = table.Column<bool>(type: "INTEGER", nullable: false),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingSpaces_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FamilyName", "Phone" },
                values: new object[,]
                {
                    { 12, "hopeannabri@gmail.com", "Peterson Family", "111-222-3333" },
                    { 14, "FakeEmail@gmail.com", "Hanson Family", "333-444-5555" },
                    { 15, "FakeEmail2@gmail.com", "Tran Family", "777-888-9999" }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpaces",
                columns: new[] { "Id", "IsTaken", "VehicleId" },
                values: new object[] { 202, false, null });

            migrationBuilder.InsertData(
                table: "Residents",
                columns: new[] { "Id", "AccountId", "AltPhoneNumber", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 2, 12, "222-444-9999", "Jace", "Peterson" },
                    { 3, 14, null, "Anna", "Hanson" },
                    { 4, 15, null, "Sydney", "Tran" },
                    { 5, 15, "555-555-9999", "Emmanuel", "Rocha" },
                    { 8, 12, null, "Brianna", "Peterson" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "AccountId", "Color", "LicensePlate", "Make", "Model", "SpotId" },
                values: new object[,]
                {
                    { 1, 12, "Black", "", "Toyota", "Corolla", 111 },
                    { 2, 14, "Blue", "", "Honda", "Civic", 201 },
                    { 3, 15, "Red", "", "Honda", "Accord", 101 },
                    { 4, 15, "Silver", "", "Subaru", "Crosstrek", 113 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpaces",
                columns: new[] { "Id", "IsTaken", "VehicleId" },
                values: new object[,]
                {
                    { 101, true, 3 },
                    { 111, true, 1 },
                    { 113, true, 4 },
                    { 201, true, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpaces_VehicleId",
                table: "ParkingSpaces",
                column: "VehicleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Residents_AccountId",
                table: "Residents",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AccountId",
                table: "Vehicles",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingSpaces");

            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
