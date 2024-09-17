using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAPI.Migrations
{
    public partial class productsSeed : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)"
                + "Values('Starter motor Honda civic 2006', 100, 'Starter motor for Honda Civic 2006 1.8 Autom', 10, 'starter.jpg', 1)");
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)"
                + "Values('Fuel Pump', 35, 'Fuel pump for VW Golf 2.2 Manual', 10, 'golfPump.jpg', 1)");
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)"
                + "Values('Brake Pads Peugeot', 12, 'Brake pads for peugeot from 2001 to 2009', 10, 'pads.jpg', 2)");
            mb.Sql("Insert into Products(Name, Price, Description, Stock, ImageURL, CategoryId)"
                + "Values('ABS Sensor BMW E46', 24.99, 'ABS sensor for BMW E46 2010', 10, 'AbsBmw.jpg', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from Products");
        }
    }
}
