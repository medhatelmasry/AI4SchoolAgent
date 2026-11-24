using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AI4SchoolAgent.Data.Migrations;

/// <inheritdoc />
public partial class _20261116202248_M1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Students",
            columns: table => new
            {
                StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                FirstName = table.Column<string>(type: "TEXT", nullable: false),
                LastName = table.Column<string>(type: "TEXT", nullable: false),
                School = table.Column<string>(type: "TEXT", nullable: false),
                Gender = table.Column<string>(type: "TEXT", nullable: false),
                DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: true),
                Status = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Students", x => x.StudentId);
            });

        migrationBuilder.InsertData(
            table: "Students",
            columns: new[] { "StudentId", "DateOfBirth", "FirstName", "Gender", "LastName", "School", "Status" },
            values: new object[,]
            {
                { 1, new DateOnly(1998, 6, 12), "Tom", "Male", "Max", "Nursing", null },
                { 2, new DateOnly(2001, 9, 23), "Ann", "Female", "Fay", "Mining", null },
                { 3, new DateOnly(1995, 4, 17), "Joe", "Male", "Sun", "Nursing", null },
                { 4, new DateOnly(1997, 12, 5), "Sue", "Female", "Fox", "Computing", null },
                { 5, new DateOnly(1999, 7, 28), "Ben", "Male", "Ray", "Mining", null },
                { 6, new DateOnly(2002, 2, 15), "Zoe", "Female", "Cox", "Business", null },
                { 7, new DateOnly(2000, 10, 8), "Sam", "Male", "Ray", "Mining", null },
                { 8, new DateOnly(1994, 3, 19), "Dan", "Male", "Ash", "Medicine", null },
                { 9, new DateOnly(1996, 8, 30), "Pat", "Female", "Lee", "Computing", null },
                { 10, new DateOnly(1993, 11, 25), "Kim", "Female", "Day", "Nursing", null },
                { 11, new DateOnly(1999, 2, 14), "Tim", "Male", "Rex", "Computing", null },
                { 12, new DateOnly(2000, 5, 19), "Rob", "Male", "Ram", "Nursing", null },
                { 13, new DateOnly(1995, 8, 30), "Jan", "Female", "Fry", "Mining", null },
                { 14, new DateOnly(1998, 11, 9), "Jim", "Male", "Tex", "Nursing", null },
                { 15, new DateOnly(1997, 4, 23), "Ben", "Male", "Kid", "Business", null },
                { 16, new DateOnly(2001, 7, 15), "Mia", "Female", "Chu", "Medicine", null },
                { 17, new DateOnly(1996, 3, 11), "Ted", "Male", "Tao", "Computing", null },
                { 18, new DateOnly(1994, 9, 27), "Amy", "Female", "Day", "Nursing", null },
                { 19, new DateOnly(1995, 5, 22), "Ian", "Male", "Roy", "Nursing", null },
                { 20, new DateOnly(2002, 6, 30), "Liz", "Female", "Kit", "Nursing", null },
                { 21, new DateOnly(1997, 12, 10), "Mat", "Male", "Tan", "Medicine", null },
                { 22, new DateOnly(1996, 7, 21), "Deb", "Female", "Roy", "Medicine", null },
                { 23, new DateOnly(1998, 1, 9), "Ana", "Female", "Ray", "Mining", null },
                { 24, new DateOnly(1995, 4, 25), "Lyn", "Female", "Poe", "Computing", null },
                { 25, new DateOnly(2000, 9, 17), "Amy", "Female", "Raj", "Nursing", null },
                { 26, new DateOnly(1999, 8, 20), "Kim", "Female", "Ash", "Mining", null },
                { 27, new DateOnly(2001, 11, 4), "Bec", "Female", "Kid", "Nursing", null },
                { 28, new DateOnly(1997, 3, 12), "Eva", "Female", "Fry", "Computing", null },
                { 29, new DateOnly(1996, 5, 8), "Eli", "Male", "Lap", "Business", null },
                { 30, new DateOnly(2001, 7, 14), "Sam", "Male", "Yim", "Nursing", null },
                { 31, new DateOnly(1997, 5, 16), "Joe", "Male", "Hui", "Mining", null },
                { 32, new DateOnly(1998, 8, 21), "Liz", "Female", "Jin", "Nursing", null },
                { 33, new DateOnly(1996, 12, 10), "Ric", "Male", "Kuo", "Business", null },
                { 34, new DateOnly(1999, 2, 14), "Pam", "Female", "Mak", "Computing", null },
                { 35, new DateOnly(1994, 6, 28), "Cat", "Female", "Yao", "Medicine", null },
                { 36, new DateOnly(1995, 9, 3), "Lou", "Male", "Zhu", "Mining", null },
                { 37, new DateOnly(2000, 11, 22), "Tom", "Male", "Dag", "Business", null },
                { 38, new DateOnly(1996, 2, 7), "Stu", "Male", "Day", "Business", null },
                { 39, new DateOnly(1998, 3, 25), "Tom", "Male", "Gad", "Mining", null },
                { 40, new DateOnly(1997, 10, 30), "Bob", "Male", "Bee", "Business", null },
                { 41, new DateOnly(1995, 12, 19), "Jim", "Male", "Ots", "Business", null },
                { 42, new DateOnly(2001, 4, 5), "Tom", "Male", "Mag", "Business", null },
                { 43, new DateOnly(1999, 6, 17), "Hal", "Male", "Doe", "Mining", null },
                { 44, new DateOnly(2002, 5, 9), "Roy", "Male", "Kim", "Mining", null },
                { 45, new DateOnly(1993, 8, 27), "Vis", "Male", "Cox", "Nursing", null },
                { 46, new DateOnly(1994, 9, 11), "Kay", "Female", "Aga", "Nursing", null },
                { 47, new DateOnly(1995, 7, 29), "Reo", "Male", "Hui", "Nursing", null },
                { 48, new DateOnly(2001, 12, 23), "Bob", "Male", "Roe", "Mining", null },
                { 49, new DateOnly(1996, 11, 16), "Jay", "Male", "Eff", "Computing", null },
                { 50, new DateOnly(1997, 5, 21), "Eva", "Female", "Chu", "Business", null },
                { 51, new DateOnly(1997, 4, 18), "Lex", "Male", "Rae", "Nursing", null },
                { 52, new DateOnly(1995, 9, 25), "Lin", "Female", "Dex", "Mining", null },
                { 53, new DateOnly(1998, 11, 13), "Liz", "Female", "Dag", "Business", null },
                { 54, new DateOnly(1999, 6, 4), "Ben", "Male", "Shy", "Computing", null },
                { 55, new DateOnly(2000, 8, 27), "Rob", "Male", "Bos", "Nursing", null },
                { 56, new DateOnly(1996, 7, 20), "Ali", "Female", "Mac", "Business", null },
                { 57, new DateOnly(1994, 3, 15), "Edi", "Male", "Gee", "Computing", null },
                { 58, new DateOnly(1997, 12, 9), "Eva", "Female", "Cao", "Mining", null },
                { 59, new DateOnly(1995, 2, 21), "Jun", "Male", "Lam", "Computing", null },
                { 60, new DateOnly(2001, 6, 30), "Eli", "Male", "Tao", "Computing", null },
                { 61, new DateOnly(1998, 5, 17), "Ana", "Female", "Bay", "Computing", null },
                { 62, new DateOnly(1996, 11, 23), "Gil", "Male", "Tal", "Mining", null },
                { 63, new DateOnly(1999, 7, 28), "Wes", "Male", "Dey", "Nursing", null },
                { 64, new DateOnly(2000, 9, 12), "Nea", "Female", "Tan", "Computing", null },
                { 65, new DateOnly(1994, 10, 3), "Ava", "Female", "Day", "Nursing", null },
                { 66, new DateOnly(1998, 4, 27), "Rie", "Female", "Ray", "Business", null },
                { 67, new DateOnly(1995, 6, 22), "Ken", "Male", "Sim", "Nursing", null },
                { 68, new DateOnly(1997, 11, 16), "Liz", "Female", "Kim", "Nursing", null },
                { 69, new DateOnly(1996, 2, 10), "Max", "Male", "Fur", "Mining", null },
                { 70, new DateOnly(2001, 8, 8), "Edi", "Male", "Kai", "Mining", null },
                { 71, new DateOnly(1999, 3, 5), "Joe", "Male", "Sha", "Business", null },
                { 72, new DateOnly(1997, 9, 29), "Gem", "Female", "Lin", "Nursing", null },
                { 73, new DateOnly(1995, 12, 4), "Ama", "Female", "Kam", "Nursing", null },
                { 74, new DateOnly(1998, 7, 14), "Zac", "Male", "Der", "Business", null },
                { 75, new DateOnly(2000, 1, 23), "Liz", "Female", "Car", "Nursing", null },
                { 76, new DateOnly(1996, 5, 19), "Kit", "Male", "Ngo", "Nursing", null },
                { 77, new DateOnly(1997, 3, 31), "Tom", "Male", "Ren", "Computing", null },
                { 78, new DateOnly(1994, 9, 10), "Kal", "Male", "Hon", "Nursing", null },
                { 79, new DateOnly(2002, 4, 2), "Avi", "Male", "Gil", "Business", null },
                { 80, new DateOnly(1995, 11, 25), "Ada", "Female", "Dao", "Business", null },
                { 81, new DateOnly(1999, 12, 8), "Eva", "Female", "Kim", "Mining", null },
                { 82, new DateOnly(1997, 7, 30), "Amy", "Female", "Kam", "Computing", null },
                { 83, new DateOnly(1996, 3, 11), "Noa", "Male", "Sim", "Business", null },
                { 84, new DateOnly(1998, 5, 21), "Ben", "Male", "Kai", "Computing", null },
                { 85, new DateOnly(2000, 6, 18), "Mae", "Female", "Car", "Business", null },
                { 86, new DateOnly(1994, 8, 5), "Mae", "Female", "Der", "Mining", null },
                { 87, new DateOnly(2001, 2, 12), "Lee", "Male", "Hon", "Business", null },
                { 88, new DateOnly(1997, 9, 17), "Gus", "Male", "Lin", "Business", null },
                { 89, new DateOnly(1995, 4, 28), "Ava", "Female", "Bay", "Nursing", null },
                { 90, new DateOnly(2002, 7, 20), "Ari", "Male", "Sim", "Mining", null },
                { 91, new DateOnly(1996, 10, 9), "Kai", "Male", "Lee", "Business", null },
                { 92, new DateOnly(1999, 5, 31), "Ali", "Female", "Doe", "Business", null },
                { 93, new DateOnly(1994, 12, 21), "Ivy", "Female", "Kim", "Computing", null },
                { 94, new DateOnly(1998, 3, 7), "Mia", "Female", "Sun", "Nursing", null },
                { 95, new DateOnly(1996, 2, 14), "Lia", "Female", "Fox", "Mining", null },
                { 96, new DateOnly(1997, 6, 23), "Ian", "Male", "Day", "Business", null },
                { 97, new DateOnly(1999, 8, 29), "Bra", "Male", "Tan", "Computing", null },
                { 98, new DateOnly(2001, 5, 10), "Eli", "Male", "Chu", "Business", null },
                { 99, new DateOnly(1995, 11, 8), "Max", "Male", "Cox", "Nursing", null },
                { 100, new DateOnly(1994, 9, 4), "Ann", "Female", "Kuo", "Business", null },
                { 101, new DateOnly(1996, 3, 15), "Liv", "Female", "Kim", "Computing", null },
                { 102, new DateOnly(1998, 12, 27), "Jay", "Male", "Tan", "Mining", null },
                { 103, new DateOnly(1999, 7, 3), "Ace", "Male", "Sun", "Nursing", null },
                { 104, new DateOnly(2002, 1, 9), "Kit", "Male", "Doe", "Business", null },
                { 105, new DateOnly(1995, 4, 11), "Kim", "Female", "Kay", "Nursing", null },
                { 106, new DateOnly(1997, 6, 26), "Lyn", "Female", "Bar", "Business", null },
                { 107, new DateOnly(1996, 8, 22), "Tae", "Male", "Sae", "Computing", null },
                { 108, new DateOnly(1998, 12, 2), "Pri", "Female", "Day", "Nursing", null },
                { 109, new DateOnly(1989, 4, 7), "Naz", "Female", "Rai", "Nursing", null },
                { 110, new DateOnly(1999, 6, 21), "Jan", "Female", "Ojo", "Business", null },
                { 111, new DateOnly(2002, 7, 15), "Mac", "Male", "Mah", "Computing", null },
                { 112, new DateOnly(2004, 9, 19), "Tim", "Male", "Maw", "Nursing", null }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Students");
    }
}
