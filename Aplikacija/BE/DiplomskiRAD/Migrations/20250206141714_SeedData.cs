using System;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Cryptography;

#nullable disable

namespace DiplomskiRAD.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        /// 
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slika = table.Column<string>(type: "text", nullable: true),
                    EquipmentState = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slika = table.Column<string>(type: "text", nullable: true),
                    Kilometraza = table.Column<double>(type: "double precision", nullable: false),
                    YearOfProduction = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleState = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    MotorcycleType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    numOfPurchases = table.Column<int>(type: "integer", nullable: false),
                    Salary = table.Column<double>(type: "double precision", nullable: true),
                    numOfTasks = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            var userGuids = new[]{
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
                };

            migrationBuilder.Sql(@"INSERT INTO ""Users""  VALUES
                    ('" + userGuids[0] + @"', 'direktor', 'direktor', 'direktor', '" + HashPassword("direktor") + @"', 0, 0, 999, 0),
                    ('" + userGuids[1] + @"', 'radnik1', 'radnik1', 'radnik1', '" + HashPassword("radnik1") + @"', 1, 0, 111, 0),
                    ('" + userGuids[2] + @"', 'radnik2', 'radnik2', 'radnik2', '" + HashPassword("radnik2") + @"', 1, 0, 222, 0),
                    ('" + userGuids[3] + @"', 'radnik3', 'radnik3', 'radnik3', '" + HashPassword("radnik3") + @"', 1, 0, 333, 0),
                    ('" + userGuids[4] + @"', 'klijent1', 'klijent1', 'klijent1', '" + HashPassword("klijent1") + @"', 2, 0, null, 0),
                    ('" + userGuids[5] + @"', 'klijent2', 'klijent2', 'klijent2', '" + HashPassword("klijent2") + @"', 2, 0, null, 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
