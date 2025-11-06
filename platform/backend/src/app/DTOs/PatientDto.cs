using System;

namespace Hospital.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for Patient data exposed via API.
    /// This DTO is separate from the Patient Entity to:
    /// 1. Hide internal audit fields (CreatedBy, UpdatedBy) from clients
    /// 2. Provide a stable API contract independent of domain model changes
    /// 3. Expose computed properties (FullName, Age) as plain values
    /// 4. Follow Clean Architecture principles (Domain vs Application layer separation)
    /// </summary>
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? BloodGroup { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

