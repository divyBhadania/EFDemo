﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF.Repository.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "IsActive", "Role_Name" },
                values: new object[,]
                {
                    { 1, true, "Admin" },
                    { 2, true, "Member" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Active", "CreatedOn", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Admin", "admin@123" },
                    { 2, true, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "member@gmail.com", "member", "member@123" }
                });

            migrationBuilder.InsertData(
                table: "UserRoleMapping",
                columns: new[] { "Id", "IsActive", "Role_Id", "User_Id" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 2, 1 },
                    { 3, true, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoleMapping",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoleMapping",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoleMapping",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
