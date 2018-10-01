using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Salon.Data.Migrations
{
    public partial class AddcolumnStartEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Worker",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartEvent",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Worker");

            migrationBuilder.DropColumn(
                name: "StartEvent",
                table: "Event");
        }
    }
}
