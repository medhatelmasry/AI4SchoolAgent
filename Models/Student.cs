using System;
using System.ComponentModel.DataAnnotations;

namespace AI4SchoolAgent.Models;

public class Student
{
    public int StudentId { get; set; }

    [Required]
    public String? FirstName { get; set; }

    [Required]
    public String? LastName { get; set; }

    [Required]
    public String? School { get; set; }

    [Required]
    public String? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }
    public String? Status { get; set; }
}

