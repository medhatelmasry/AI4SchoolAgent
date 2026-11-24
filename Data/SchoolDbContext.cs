using System;
using Microsoft.EntityFrameworkCore;
using AI4SchoolAgent.Models;
namespace AI4SchoolAgent.Data;

public class SchoolDbContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Student>().HasData(GetStudents());
    }

    private static IEnumerable<Student> GetStudents()
    {
        const string studentsUrl =
          "https://gist.githubusercontent.com/medhatelmasry/2ae7a36d5392f862b0d1039b2a52b9b4/raw/45533eda6c43ce439dbc65c37a7fbdef01546c2d/students.json";

        using var httpClient = new HttpClient();

        var json = httpClient.GetStringAsync(studentsUrl).GetAwaiter().GetResult();

        return System.Text.Json.JsonSerializer.Deserialize<List<Student>>(json)
          ?? throw new InvalidOperationException("The students JSON returned no data.");
    }
}

