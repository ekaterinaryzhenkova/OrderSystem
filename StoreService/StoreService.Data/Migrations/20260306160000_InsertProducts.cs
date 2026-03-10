using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreService.Data.Migrations
{
    [DbContext(typeof(StoreServiceContext))]
    [Migration("20260306160000_InsertProducts")]
    public class InsertProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Quantity" },
                columnTypes: new string[] { "uniqueidentifier", "nvarchar(max)", "int" },
                values: new object[,]
                {
                    { Guid.Parse("7f679a99-6b3e-4007-8b8b-5d86bfa73902"), "Puzzle", 10 },
                    { Guid.Parse("43c23e52-5f01-467b-8bd0-0de7a8ae4b13"), "Socks", 70 },
                    { Guid.Parse("252e194d-675e-4614-8707-d15be5bf58a4"), "Cup", 100 },
                    { Guid.Parse("702dad07-4da0-4d8f-aafd-2db41413683f"), "Notebook", 1000 },
                    { Guid.Parse("778f30ae-9b8a-40ff-9c21-2cc7df884f80"), "Pot", 20 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}