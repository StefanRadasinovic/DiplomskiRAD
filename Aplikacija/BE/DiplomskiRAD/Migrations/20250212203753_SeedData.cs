using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

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

            var equipGuids = new[]{
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                };

            migrationBuilder.Sql(@"INSERT INTO ""Equipments""  VALUES
                    ('" + equipGuids[0] + @"', 'Kaciga', 'oprema1.jpg', 0, 1),
                    ('" + equipGuids[1] + @"', 'Rukavice', 'oprema2.jpg', 1, 5),
                    ('" + equipGuids[2] + @"', 'Jakna', 'oprema3.jpg', 0, 1),
                    ('" + equipGuids[3] + @"', 'Zadnje svetlo', 'oprema4.jpg', 0, 2),
                    ('" + equipGuids[4] + @"', 'Kaciga', 'oprema5.jpg', 1, 2)");


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

            var motorGuids = new[]{
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                };

            migrationBuilder.Sql(@"INSERT INTO ""Motorcycles""  VALUES
                    ('" + motorGuids[0] + @"', 'YZF', 'motor3.jpg', 0, 2025, 0, 3, 0),
                    ('" + motorGuids[1] + @"', 'RSV4', 'motor4.jpg', 2200, 2020, 1, 2, 0),
                    ('" + motorGuids[2] + @"', 'ATV', 'skuter1.jpg', 570, 2023, 1, 2, 2),
                    ('" + motorGuids[3] + @"', 'eFLUX', 'elektricni1.jpg', 0, 2024, 0, 5, 3),
                    ('" + motorGuids[4] + @"', 'COBRA', 'quad1.jpg', 2000, 2022, 1, 2, 4),
                    ('" + motorGuids[5] + @"', 'NINJA', 'motor5.jpg', 1000, 2023, 1, 2, 0),
                    ('" + motorGuids[6] + @"', 'CYBER', 'elektricni2.jpg', 0, 2025, 0, 4, 3),
                    ('" + motorGuids[7] + @"', 'R9', 'motor7.jpg', 1500, 2024, 1, 4, 3)");


            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                });

            var priceListGuids = new[] {
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),
                Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()
            };

            migrationBuilder.Sql(@"INSERT INTO ""PriceLists""  VALUES
                    ('" + priceListGuids[0] + @"', 10000, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[1] + @"', 17000, '2025-06-11 01:00:00 +01', '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[2] + @"', 14000, '2025-12-12 01:00:00 +01', '2026-02-12 01:00:00 +01'),                   
                    ('" + priceListGuids[3] + @"', 13000, '2025-02-10 01:00:00 +01','2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[4] + @"', 20000, '2025-06-11 01:00:00 +01', '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[5] + @"', 17000, '2025-12-12 01:00:00 +01', '2026-02-12 01:00:00 +01'),            
                    ('" + priceListGuids[6] + @"', 6700,  '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[7] + @"', 6700,  '2025-06-11 01:00:00 +01',  '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[8] + @"', 4500,  '2025-12-12 01:00:00 +01',  '2026-02-12 01:00:00 +01'),              
                    ('" + priceListGuids[9] + @"', 9500,  '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[10] + @"', 7600, '2025-06-11 01:00:00 +01',  '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[11] + @"', 8500, '2025-12-12 01:00:00 +01',  '2026-02-12 01:00:00 +01'),    
                    ('" + priceListGuids[12] + @"', 6500, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[13] + @"', 4700, '2025-06-11 01:00:00 +01',  '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[14] + @"', 5500, '2025-12-12 01:00:00 +01',  '2026-02-12 01:00:00 +01'),               
                    ('" + priceListGuids[15] + @"', 7100, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[16] + @"', 8200, '2025-06-11 01:00:00 +01',  '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[17] + @"', 6600, '2025-12-12 01:00:00 +01',  '2026-02-12 01:00:00 +01'),             
                    ('" + priceListGuids[18] + @"', 6500, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[19] + @"', 7300, '2025-06-11 01:00:00 +01',  '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[20] + @"', 6900, '2025-12-12 01:00:00 +01',  '2026-02-12 01:00:00 +01'),             
                    ('" + priceListGuids[21] + @"', 10000, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[22] + @"', 11000, '2025-06-11 01:00:00 +01', '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[23] + @"', 9000,  '2025-12-12 01:00:00 +01', '2026-02-12 01:00:00 +01'),
                    ('" + priceListGuids[24] + @"', 200,   '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[25] + @"', 150,   '2025-06-11 01:00:00 +01', '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[26] + @"', 170,   '2025-12-12 01:00:00 +01', '2026-02-12 01:00:00 +01'),        
                    ('" + priceListGuids[27] + @"', 100,   '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[28] + @"', 120,   '2025-06-11 01:00:00 +01', '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[29] + @"', 80,    '2025-12-12 01:00:00 +01', '2026-02-12 01:00:00 +01'),
                    ('" + priceListGuids[30] + @"', 350, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[31] + @"', 320, '2025-06-11 01:00:00 +01', '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[32] + @"', 270, '2025-12-12 01:00:00 +01', '2026-02-12 01:00:00 +01'),
                    ('" + priceListGuids[33] + @"', 30, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[34] + @"', 32, '2025-06-11 01:00:00 +01', '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[35] + @"', 27, '2025-12-12 01:00:00 +01', '2026-02-12 01:00:00 +01'),                        
                    ('" + priceListGuids[36] + @"', 80, '2025-02-10 01:00:00 +01', '2025-06-10 01:00:00 +01'),
                    ('" + priceListGuids[37] + @"', 70, '2025-06-11 01:00:00 +01',  '2025-12-11 01:00:00 +01'),
                    ('" + priceListGuids[38] + @"', 50, '2025-06-11 01:00:00 +01',  '2025-12-11 01:00:00 +01')");

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            var producerGuids = new[]{
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                };

            migrationBuilder.Sql(@"INSERT INTO ""Producers""  VALUES
                    ('" + producerGuids[0] + @"', 'Yamaha', 'Potpuno nov, full oprema'), 
                    ('" + producerGuids[1] + @"', 'Elite', 'Brz i lak za odrzavanje'),
                    ('" + producerGuids[2] + @"', 'MS Energy', 'Potpuno nov, 2025 godiste'),
                    ('" + producerGuids[3] + @"', 'Aprilia', '217 konjskih snaga sa poboljšanim obrtnim momentom'),
                    ('" + producerGuids[4] + @"', 'Nitro', 'Garancija, servis,sa rikvercom od 6"" gume'),
                    ('" + producerGuids[5] + @"', 'Kawasaki', 'Sportska kontrola proklizavanja, quick shifter'),
                    ('" + producerGuids[6] + @"', 'Harley', 'Moguce eksterno punjenje baterije,uklonjiva baterija' ),
                    ('" + producerGuids[12] + @"', 'Yamaha', 'Garazirana i dobro stanju'),

                    ('" + producerGuids[7] + @"', 'Thunder', 'Prozirni vizir protiv grebanja, dodatni otvori za vazduh' ),
                    ('" + producerGuids[8] + @"', 'Star', 'Unutrasnji suncani vizir' ),
                    ('" + producerGuids[9] + @"', 'Alpinestars', ' Kozna jakna, dostupna u M-L-XL-XXL velicinama' ),
                    ('" + producerGuids[10] + @"', 'Harley', 'Vodootporne, rastegljive na zglobovima' ),
                    ('" + producerGuids[11] + @"', 'Inparts', 'LED Stop svetlo sa pozicijom')");


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
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                };

            migrationBuilder.Sql(@"INSERT INTO ""Users""  VALUES
                    ('" + userGuids[0] + @"', 'direktor', 'direktor', 'direktor', '" + HashPassword("direktor") + @"', 0, 0, 999, 0),
                    ('" + userGuids[1] + @"', 'radnik1', 'radnik1', 'radnik1', '" + HashPassword("radnik1") + @"', 1, 0, 111, 0),
                    ('" + userGuids[2] + @"', 'radnik2', 'radnik2', 'radnik2', '" + HashPassword("radnik2") + @"', 1, 0, 222, 0),
                    ('" + userGuids[3] + @"', 'radnik3', 'radnik3', 'radnik3', '" + HashPassword("radnik3") + @"', 1, 0, 333, 0),
                    ('" + userGuids[4] + @"', 'radnik4', 'radnik4', 'radnik4', '" + HashPassword("radnik4") + @"', 1, 0, 444, 0),
                    ('" + userGuids[5] + @"', 'radnik5', 'radnik5', 'radnik5', '" + HashPassword("radnik5") + @"', 1, 0, 555, 0),
                    ('" + userGuids[6] + @"', 'klijent1', 'klijent1', 'klijent1', '" + HashPassword("klijent1") + @"', 2, 0, null, 0),
                    ('" + userGuids[7] + @"', 'klijent2', 'klijent2', 'klijent2', '" + HashPassword("klijent2") + @"', 2, 0, null, 0)");


            migrationBuilder.CreateTable(
                name: "EquipmentPriceList",
                columns: table => new
                {
                    EquipmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceListsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPriceList", x => new { x.EquipmentsId, x.PriceListsId });
                    table.ForeignKey(
                        name: "FK_EquipmentPriceList_Equipments_EquipmentsId",
                        column: x => x.EquipmentsId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentPriceList_PriceLists_PriceListsId",
                        column: x => x.PriceListsId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorcyclePriceList",
                columns: table => new
                {
                    MotorcyclesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceListsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcyclePriceList", x => new { x.MotorcyclesId, x.PriceListsId });
                    table.ForeignKey(
                        name: "FK_MotorcyclePriceList_Motorcycles_MotorcyclesId",
                        column: x => x.MotorcyclesId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorcyclePriceList_PriceLists_PriceListsId",
                        column: x => x.PriceListsId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentProducer",
                columns: table => new
                {
                    EquipmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProducersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentProducer", x => new { x.EquipmentsId, x.ProducersId });
                    table.ForeignKey(
                        name: "FK_EquipmentProducer_Equipments_EquipmentsId",
                        column: x => x.EquipmentsId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentProducer_Producers_ProducersId",
                        column: x => x.ProducersId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleProducer",
                columns: table => new
                {
                    MotorcyclesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProducersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleProducer", x => new { x.MotorcyclesId, x.ProducersId });
                    table.ForeignKey(
                        name: "FK_MotorcycleProducer_Motorcycles_MotorcyclesId",
                        column: x => x.MotorcyclesId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorcycleProducer_Producers_ProducersId",
                        column: x => x.ProducersId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderAmount = table.Column<double>(type: "double precision", nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceDescription = table.Column<string>(type: "text", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    StartDateService = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateService = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ServiceStatus = table.Column<int>(type: "integer", nullable: false),
                    razlogOdbijanja = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentOrder",
                columns: table => new
                {
                    EquipmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentOrder", x => new { x.EquipmentsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_EquipmentOrder_Equipments_EquipmentsId",
                        column: x => x.EquipmentsId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorcycleOrder",
                columns: table => new
                {
                    MotorcyclesId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleOrder", x => new { x.MotorcyclesId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_MotorcycleOrder_Motorcycles_MotorcyclesId",
                        column: x => x.MotorcyclesId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorcycleOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskDescription = table.Column<string>(type: "text", nullable: false),
                    EndDateTask = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    razlogOdbijanja = table.Column<string>(type: "text", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskServices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    IsSpartPartUsed = table.Column<int>(type: "integer", nullable: false),
                    TaskServiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpareParts_TaskServices_TaskServiceId",
                        column: x => x.TaskServiceId,
                        principalTable: "TaskServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentOrder_OrdersId",
                table: "EquipmentOrder",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPriceList_PriceListsId",
                table: "EquipmentPriceList",
                column: "PriceListsId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentProducer_ProducersId",
                table: "EquipmentProducer",
                column: "ProducersId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorcycleOrder_OrdersId",
                table: "MotorcycleOrder",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorcyclePriceList_PriceListsId",
                table: "MotorcyclePriceList",
                column: "PriceListsId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorcycleProducer_ProducersId",
                table: "MotorcycleProducer",
                column: "ProducersId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ServiceId",
                table: "Reviews",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_TaskServiceId",
                table: "SpareParts",
                column: "TaskServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskServices_ServiceId",
                table: "TaskServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskServices_UserId",
                table: "TaskServices",
                column: "UserId");

            migrationBuilder.Sql(@"INSERT INTO ""MotorcycleProducer""  VALUES
                    ('" + motorGuids[0] + @"', '" + producerGuids[0] + @"'),
                    ('" + motorGuids[1] + @"', '" + producerGuids[3] + @"'),
                    ('" + motorGuids[2] + @"', '" + producerGuids[1] + @"'),
                    ('" + motorGuids[3] + @"', '" + producerGuids[6] + @"'),
                    ('" + motorGuids[4] + @"', '" + producerGuids[4] + @"'),
                    ('" + motorGuids[5] + @"', '" + producerGuids[5] + @"'),
                    ('" + motorGuids[6] + @"', '" + producerGuids[2] + @"'),
                    ('" + motorGuids[7] + @"', '" + producerGuids[12] + @"')");

            migrationBuilder.Sql(@"INSERT INTO ""EquipmentProducer""  VALUES
                    ('" + equipGuids[0] + @"', '" + producerGuids[7] + @"'),
                    ('" + equipGuids[1] + @"', '" + producerGuids[10] + @"'),
                    ('" + equipGuids[2] + @"', '" + producerGuids[9] + @"'),
                    ('" + equipGuids[3] + @"', '" + producerGuids[11] + @"'),
                    ('" + equipGuids[4] + @"', '" + producerGuids[8] + @"')");


            migrationBuilder.Sql(@"INSERT INTO ""MotorcyclePriceList""  VALUES
                    ('" + motorGuids[0] + @"', '" + priceListGuids[0] + @"'),
                    ('" + motorGuids[0] + @"', '" + priceListGuids[1] + @"'),
                    ('" + motorGuids[0] + @"', '" + priceListGuids[2] + @"'),
                    ('" + motorGuids[1] + @"', '" + priceListGuids[3] + @"'),
                    ('" + motorGuids[1] + @"', '" + priceListGuids[4] + @"'),
                    ('" + motorGuids[1] + @"', '" + priceListGuids[5] + @"'),          
                    ('" + motorGuids[2] + @"', '" + priceListGuids[6] + @"'),
                    ('" + motorGuids[2] + @"', '" + priceListGuids[7] + @"'),
                    ('" + motorGuids[2] + @"', '" + priceListGuids[8] + @"'),
                    ('" + motorGuids[3] + @"', '" + priceListGuids[9] + @"'),
                    ('" + motorGuids[3] + @"', '" + priceListGuids[10] + @"'),
                    ('" + motorGuids[3] + @"', '" + priceListGuids[11] + @"'),
                    ('" + motorGuids[4] + @"', '" + priceListGuids[12] + @"'),
                    ('" + motorGuids[4] + @"', '" + priceListGuids[13] + @"'),
                    ('" + motorGuids[4] + @"', '" + priceListGuids[14] + @"'),
                    ('" + motorGuids[5] + @"', '" + priceListGuids[15] + @"'),
                    ('" + motorGuids[5] + @"', '" + priceListGuids[16] + @"'),
                    ('" + motorGuids[5] + @"', '" + priceListGuids[17] + @"'),
                    ('" + motorGuids[6] + @"', '" + priceListGuids[18] + @"'),
                    ('" + motorGuids[6] + @"', '" + priceListGuids[19] + @"'),
                    ('" + motorGuids[6] + @"', '" + priceListGuids[20] + @"'),
                    ('" + motorGuids[7] + @"', '" + priceListGuids[21] + @"'),
                    ('" + motorGuids[7] + @"', '" + priceListGuids[22] + @"'),
                    ('" + motorGuids[7] + @"', '" + priceListGuids[23] + @"')");


            migrationBuilder.Sql(@"INSERT INTO ""EquipmentPriceList""  VALUES
                    ('" + equipGuids[0] + @"', '" + priceListGuids[24] + @"'),
                    ('" + equipGuids[0] + @"', '" + priceListGuids[25] + @"'),
                    ('" + equipGuids[0] + @"', '" + priceListGuids[26] + @"'),
                    ('" + equipGuids[1] + @"', '" + priceListGuids[27] + @"'),
                    ('" + equipGuids[1] + @"', '" + priceListGuids[28] + @"'),
                    ('" + equipGuids[1] + @"', '" + priceListGuids[29] + @"'),
                    ('" + equipGuids[2] + @"', '" + priceListGuids[30] + @"'),
                    ('" + equipGuids[2] + @"', '" + priceListGuids[31] + @"'),
                    ('" + equipGuids[2] + @"', '" + priceListGuids[32] + @"'),
                    ('" + equipGuids[3] + @"', '" + priceListGuids[33] + @"'),
                    ('" + equipGuids[3] + @"', '" + priceListGuids[34] + @"'),
                    ('" + equipGuids[3] + @"', '" + priceListGuids[35] + @"'),
                    ('" + equipGuids[4] + @"', '" + priceListGuids[36] + @"'),
                    ('" + equipGuids[4] + @"', '" + priceListGuids[37] + @"'),
                    ('" + equipGuids[4] + @"', '" + priceListGuids[38] + @"')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentOrder");

            migrationBuilder.DropTable(
                name: "EquipmentPriceList");

            migrationBuilder.DropTable(
                name: "EquipmentProducer");

            migrationBuilder.DropTable(
                name: "MotorcycleOrder");

            migrationBuilder.DropTable(
                name: "MotorcyclePriceList");

            migrationBuilder.DropTable(
                name: "MotorcycleProducer");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PriceLists");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "TaskServices");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
