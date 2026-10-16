using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPRevision.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionColonnesFantomes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_produit_pro_t_e_marque_pro_MarqueIdMarque",
                table: "t_e_produit_pro");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_produit_pro_t_e_typeproduit_pro_TypeProduitIdTypeProduit",
                table: "t_e_produit_pro");

            migrationBuilder.DropIndex(
                name: "IX_t_e_produit_pro_MarqueIdMarque",
                table: "t_e_produit_pro");

            migrationBuilder.DropIndex(
                name: "IX_t_e_produit_pro_TypeProduitIdTypeProduit",
                table: "t_e_produit_pro");

            migrationBuilder.DropColumn(
                name: "MarqueIdMarque",
                table: "t_e_produit_pro");

            migrationBuilder.DropColumn(
                name: "TypeProduitIdTypeProduit",
                table: "t_e_produit_pro");

            migrationBuilder.DropColumn(
                name: "idMarque1",
                table: "t_e_produit_pro");

            migrationBuilder.DropColumn(
                name: "idTypeProduit1",
                table: "t_e_produit_pro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarqueIdMarque",
                table: "t_e_produit_pro",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeProduitIdTypeProduit",
                table: "t_e_produit_pro",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idMarque1",
                table: "t_e_produit_pro",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idTypeProduit1",
                table: "t_e_produit_pro",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pro_MarqueIdMarque",
                table: "t_e_produit_pro",
                column: "MarqueIdMarque");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_produit_pro_TypeProduitIdTypeProduit",
                table: "t_e_produit_pro",
                column: "TypeProduitIdTypeProduit");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_produit_pro_t_e_marque_pro_MarqueIdMarque",
                table: "t_e_produit_pro",
                column: "MarqueIdMarque",
                principalTable: "t_e_marque_pro",
                principalColumn: "idMarque");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_produit_pro_t_e_typeproduit_pro_TypeProduitIdTypeProduit",
                table: "t_e_produit_pro",
                column: "TypeProduitIdTypeProduit",
                principalTable: "t_e_typeproduit_pro",
                principalColumn: "idTypeProduit");
        }
    }
}
