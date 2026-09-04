using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TPRevision.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_marque_pro",
                columns: table => new
                {
                    idMarque = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomMarque = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("t_e_marque_pro_pkey", x => x.idMarque);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeproduit_pro",
                columns: table => new
                {
                    idTypeProduit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomTypeProduit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("t_e_typeproduit_pro_pkey", x => x.idTypeProduit);
                });

            migrationBuilder.CreateTable(
                name: "t_e_produit_pro",
                columns: table => new
                {
                    idProduit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomProduit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    nomPhoto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    uriPhoto = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    idMarque = table.Column<int>(type: "integer", nullable: false),
                    idTypeProduit = table.Column<int>(type: "integer", nullable: false),
                    StockReel = table.Column<int>(type: "integer", nullable: true),
                    StockMin = table.Column<int>(type: "integer", nullable: true),
                    StockMax = table.Column<int>(type: "integer", nullable: true),
                    MarqueIdMarque = table.Column<int>(type: "integer", nullable: true),
                    TypeProduitIdTypeProduit = table.Column<int>(type: "integer", nullable: true),
                    idMarque1 = table.Column<int>(type: "integer", nullable: false),
                    idTypeProduit1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("t_e_produit_pro_pkey", x => x.idProduit);
                    table.ForeignKey(
                        name: "FK_t_e_produit_pro_t_e_marque_pro_MarqueIdMarque",
                        column: x => x.MarqueIdMarque,
                        principalTable: "t_e_marque_pro",
                        principalColumn: "idMarque");
                    table.ForeignKey(
                        name: "FK_t_e_produit_pro_t_e_typeproduit_pro_TypeProduitIdTypeProduit",
                        column: x => x.TypeProduitIdTypeProduit,
                        principalTable: "t_e_typeproduit_pro",
                        principalColumn: "idTypeProduit");
                    table.ForeignKey(
                        name: "fk_marque",
                        column: x => x.idMarque,
                        principalTable: "t_e_marque_pro",
                        principalColumn: "idMarque");
                    table.ForeignKey(
                        name: "fk_typeproduit",
                        column: x => x.idTypeProduit,
                        principalTable: "t_e_typeproduit_pro",
                        principalColumn: "idTypeProduit");
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pro_idMarque",
                table: "t_e_produit_pro",
                column: "idMarque");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pro_idTypeProduit",
                table: "t_e_produit_pro",
                column: "idTypeProduit");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pro_MarqueIdMarque",
                table: "t_e_produit_pro",
                column: "MarqueIdMarque");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pro_TypeProduitIdTypeProduit",
                table: "t_e_produit_pro",
                column: "TypeProduitIdTypeProduit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_produit_pro");

            migrationBuilder.DropTable(
                name: "t_e_marque_pro");

            migrationBuilder.DropTable(
                name: "t_e_typeproduit_pro");
        }
    }
}
