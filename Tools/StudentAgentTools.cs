using System;
using System.ComponentModel;
using System.Text;
using AI4SchoolAgent.Models;
using Microsoft.EntityFrameworkCore;

namespace AI4SchoolAgent.Tools;

public class StudentsAgentTools
{
    private readonly DbContext _dbContext;

    public StudentsAgentTools(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> GetStudentDetailsByIdAsync(int studentId)
    {
        // Query the database for student information
        var student = await _dbContext.Set<Student>().FindAsync(studentId);
        return student != null
            ? $"Student {studentId}: {student.FirstName} {student.LastName}, School: {student.School}, Gender: {student.Gender}, Status: {student.Status ?? "not recorded"}"
            : "Student not found.";
    }

    public async Task<string> GetStudentsByGenderAsync(string gender)
    {
        // Query the database for student information by gender
        var students = await _dbContext.Set<Student>().Where(s => s.Gender == gender).ToListAsync();
        if (students.Count == 0)
            return "No students found.";
        var result = new StringBuilder();
        foreach (var student in students)
        {
            result.AppendLine($"Student {student.StudentId}: {student.FirstName} {student.LastName}, School: {student.School}, Gender: {student.Gender}, Status: {student.Status ?? "not recorded"}");
        }
        return result.ToString();
    }

    public async Task<string> GetStudentsByAgeAsync()
    {
        // Query the database for student information by school
        var students = await _dbContext.Set<Student>().ToListAsync();
        if (students.Count == 0)
            return "No students found.";
        var result = new StringBuilder();
        foreach (var student in students)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = student.DateOfBirth is { } dateOfBirth
                ? today.Year - dateOfBirth.Year - (today < dateOfBirth.AddYears(today.Year - dateOfBirth.Year) ? 1 : 0)
                : (int?)null;
            result.AppendLine($"Student {student.StudentId}: {student.FirstName} {student.LastName}, School: {student.School}, Gender: {student.Gender}, Born: {student.DateOfBirth?.ToString("yyyy-MM-dd") ?? "not recorded"}, Age: {age?.ToString() ?? "not recorded"}");
        }
        return result.ToString();
    }

    public async Task<string> GetAllStudentsAsync()
    {
        // Query the database for all students
        var students = await _dbContext.Set<Student>().ToListAsync();
        if (students.Count == 0)
            return "No students found.";
        var result = new StringBuilder();
        foreach (var student in students)
        {
            result.AppendLine($"Student {student.StudentId}: {student.FirstName} {student.LastName}, School: {student.School}, Gender: {student.Gender}, Status: {student.Status ?? "not recorded"}");
        }
        return result.ToString();
    }

    /// <summary>Finds students using optional filters combined with AND; name matches first OR last name.</summary>
    /// <param name="name">An exact or partial first or last name.</param>
    /// <param name="firstName">An exact or partial first name.</param>
    /// <param name="lastName">An exact or partial last name.</param>
    /// <param name="school">An exact or partial stored school name.</param>
    /// <param name="birthYear">The recorded birth year, from 1 through 9999.</param>
    /// <param name="matchMode">Either exact (default) or contains, applied to all text filters.</param>
    /// <returns>Student IDs, names, schools, birth dates, genders, and statuses sorted by name and ID,
    /// or an explicit validation or no-match message.</returns>
    public async Task<string> FindStudentsByAnyFilterAsync(
        [Description("Matches either first OR last name.")] string? name = null,
        [Description("Matches first name only.")] string? firstName = null,
        [Description("Matches last name only.")] string? lastName = null,
        [Description("Matches the stored school name, e.g. Computing.")] string? school = null,
        [Description("Recorded year of birth, from 1 to 9999.")] int? birthYear = null,
        [Description("exact (default) or contains; applies to every text filter. Matching is case-insensitive.")] string matchMode = "exact")
    {
        name = string.IsNullOrWhiteSpace(name) ? null : name.Trim().ToLowerInvariant();
        firstName = string.IsNullOrWhiteSpace(firstName) ? null : firstName.Trim().ToLowerInvariant();
        lastName = string.IsNullOrWhiteSpace(lastName) ? null : lastName.Trim().ToLowerInvariant();
        school = string.IsNullOrWhiteSpace(school) ? null : school.Trim().ToLowerInvariant();

        if (matchMode is not ("exact" or "contains"))
            return "Invalid match mode. Use \"exact\" or \"contains\".";
        if (birthYear is < 1 or > 9999)
            return "Birth year must be between 1 and 9999.";
        if (name == null && firstName == null && lastName == null && school == null && birthYear == null)
            return "Please provide at least one search filter.";

        var contains = matchMode == "contains";
        var query = _dbContext.Set<Student>().AsNoTracking();

        if (name != null)
            query = query.Where(s =>
                (s.FirstName != null && (contains ? s.FirstName.ToLower().Contains(name) : s.FirstName.ToLower() == name)) ||
                (s.LastName != null && (contains ? s.LastName.ToLower().Contains(name) : s.LastName.ToLower() == name)));
        if (firstName != null)
            query = query.Where(s => s.FirstName != null &&
                (contains ? s.FirstName.ToLower().Contains(firstName) : s.FirstName.ToLower() == firstName));
        if (lastName != null)
            query = query.Where(s => s.LastName != null &&
                (contains ? s.LastName.ToLower().Contains(lastName) : s.LastName.ToLower() == lastName));
        if (school != null)
            query = query.Where(s => s.School != null &&
                (contains ? s.School.ToLower().Contains(school) : s.School.ToLower() == school));
        if (birthYear != null)
            query = query.Where(s => s.DateOfBirth.HasValue && s.DateOfBirth.Value.Year == birthYear);

        var students = await query.OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
            .ThenBy(s => s.StudentId).ToListAsync();
        if (students.Count == 0)
            return "No students match the supplied filters.";

        var result = new StringBuilder();
        foreach (var student in students)
        {
            result.AppendLine($"Student {student.StudentId}: {DisplayValue(student.FirstName)} {DisplayValue(student.LastName)}, School: {DisplayValue(student.School)}, Born: {student.DateOfBirth?.ToString("yyyy-MM-dd") ?? "not recorded"}, Gender: {DisplayValue(student.Gender)}, Status: {DisplayValue(student.Status)}");
        }
        return result.ToString();
    }

    private static string DisplayValue(string? value) =>
        string.IsNullOrWhiteSpace(value) ? "not recorded" : value;

    public async Task<string> ProcessWithdrawalAsync(int studentId, string reason)
    {
        // Business logic to process the withdrawal
        var student = await _dbContext.Set<Student>().FindAsync(studentId);
        if (student == null)
            return "Student not found.";

        // Assuming we have a property to track return status for the student
        student.Status = "Withdrawal Initiated";
        await _dbContext.SaveChangesAsync();
        return $"Withdrawal initiated for student {studentId}.";
    }
}

