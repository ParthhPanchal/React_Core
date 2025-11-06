using System;

namespace Hospital.Application.DTOs
{
    /// <summary>
    /// DTO for updating an existing Patient.
    /// Includes Id to identify the patient and only fields that can be modified via API.
    /// Excludes: CreatedAt (immutable), CreatedBy/UpdatedBy (internal audit fields)
    /// </summary>
    public class UpdatePatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
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
    }
}

