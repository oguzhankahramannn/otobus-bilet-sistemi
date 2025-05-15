using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtobusBiletiApp.Migrations
{
    /// <inheritdoc />
    public partial class IlkKurulum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bus_company",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    c_name = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    c_telno = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus_company", x => x.company_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_processing",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cvv_no = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    card_no = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_processing", x => x.payment_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    p_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    surname = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.p_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bus",
                columns: table => new
                {
                    b_plaka = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    model = table.Column<string>(type: "varchar(50)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    seat_capacity = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus", x => x.b_plaka);
                    table.ForeignKey(
                        name: "FK_bus_bus_company_company_id",
                        column: x => x.company_id,
                        principalTable: "bus_company",
                        principalColumn: "company_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "company_tel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    company_id = table.Column<int>(type: "int", nullable: false),
                    tel_no = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_tel", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_tel_bus_company_company_id",
                        column: x => x.company_id,
                        principalTable: "bus_company",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    p_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Personp_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.p_id);
                    table.ForeignKey(
                        name: "FK_admin_Person_Personp_id",
                        column: x => x.Personp_id,
                        principalTable: "Person",
                        principalColumn: "p_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bus_feature",
                columns: table => new
                {
                    b_plaka = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    feature_name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus_feature", x => new { x.b_plaka, x.feature_name });
                    table.ForeignKey(
                        name: "FK_bus_feature_bus_b_plaka",
                        column: x => x.b_plaka,
                        principalTable: "bus",
                        principalColumn: "b_plaka",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trip",
                columns: table => new
                {
                    trip_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    startpoint = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    end_point = table.Column<string>(type: "varchar(100)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    end_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    b_plaka = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    p_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip", x => x.trip_id);
                    table.ForeignKey(
                        name: "FK_trip_Person_p_id",
                        column: x => x.p_id,
                        principalTable: "Person",
                        principalColumn: "p_id");
                    table.ForeignKey(
                        name: "FK_trip_bus_b_plaka",
                        column: x => x.b_plaka,
                        principalTable: "bus",
                        principalColumn: "b_plaka");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    pnr_no = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    trip_id = table.Column<int>(type: "int", nullable: true),
                    p_id = table.Column<int>(type: "int", nullable: true),
                    payment_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.pnr_no);
                    table.ForeignKey(
                        name: "FK_ticket_Person_p_id",
                        column: x => x.p_id,
                        principalTable: "Person",
                        principalColumn: "p_id");
                    table.ForeignKey(
                        name: "FK_ticket_payment_processing_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payment_processing",
                        principalColumn: "payment_id");
                    table.ForeignKey(
                        name: "FK_ticket_trip_trip_id",
                        column: x => x.trip_id,
                        principalTable: "trip",
                        principalColumn: "trip_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "seat",
                columns: table => new
                {
                    seat_no = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    b_plaka = table.Column<string>(type: "varchar(20)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_available = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    pnr_no = table.Column<int>(type: "int", nullable: true),
                    p_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seat", x => x.seat_no);
                    table.ForeignKey(
                        name: "FK_seat_Person_p_id",
                        column: x => x.p_id,
                        principalTable: "Person",
                        principalColumn: "p_id");
                    table.ForeignKey(
                        name: "FK_seat_bus_b_plaka",
                        column: x => x.b_plaka,
                        principalTable: "bus",
                        principalColumn: "b_plaka");
                    table.ForeignKey(
                        name: "FK_seat_ticket_pnr_no",
                        column: x => x.pnr_no,
                        principalTable: "ticket",
                        principalColumn: "pnr_no");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_seat",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    pnr_no = table.Column<int>(type: "int", nullable: false),
                    seat_no = table.Column<int>(type: "int", nullable: false),
                    b_plaka = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_seat", x => x.id);
                    table.ForeignKey(
                        name: "FK_ticket_seat_bus_b_plaka",
                        column: x => x.b_plaka,
                        principalTable: "bus",
                        principalColumn: "b_plaka",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_seat_seat_seat_no",
                        column: x => x.seat_no,
                        principalTable: "seat",
                        principalColumn: "seat_no",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ticket_seat_ticket_pnr_no",
                        column: x => x.pnr_no,
                        principalTable: "ticket",
                        principalColumn: "pnr_no",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_admin_Personp_id",
                table: "admin",
                column: "Personp_id");

            migrationBuilder.CreateIndex(
                name: "IX_bus_company_id",
                table: "bus",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_company_tel_company_id",
                table: "company_tel",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_seat_b_plaka",
                table: "seat",
                column: "b_plaka");

            migrationBuilder.CreateIndex(
                name: "IX_seat_p_id",
                table: "seat",
                column: "p_id");

            migrationBuilder.CreateIndex(
                name: "IX_seat_pnr_no",
                table: "seat",
                column: "pnr_no");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_p_id",
                table: "ticket",
                column: "p_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_payment_id",
                table: "ticket",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_trip_id",
                table: "ticket",
                column: "trip_id");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_seat_b_plaka",
                table: "ticket_seat",
                column: "b_plaka");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_seat_pnr_no",
                table: "ticket_seat",
                column: "pnr_no");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_seat_seat_no",
                table: "ticket_seat",
                column: "seat_no");

            migrationBuilder.CreateIndex(
                name: "IX_trip_b_plaka",
                table: "trip",
                column: "b_plaka");

            migrationBuilder.CreateIndex(
                name: "IX_trip_p_id",
                table: "trip",
                column: "p_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "bus_feature");

            migrationBuilder.DropTable(
                name: "company_tel");

            migrationBuilder.DropTable(
                name: "ticket_seat");

            migrationBuilder.DropTable(
                name: "seat");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "payment_processing");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "bus");

            migrationBuilder.DropTable(
                name: "bus_company");
        }
    }
}
