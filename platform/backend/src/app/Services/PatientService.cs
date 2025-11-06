using Hospital.Application.DTOs;
using Hospital.Application.Repositories;
using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _patientRepository.GetAllAsync();
            return patients.Select(MapToDto);
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            return patient != null ? MapToDto(patient) : null;
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createDto)
        {
            var patient = new Patient
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                DateOfBirth = createDto.DateOfBirth,
                Gender = createDto.Gender,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                Address = createDto.Address,
                City = createDto.City,
                State = createDto.State,
                ZipCode = createDto.ZipCode,
                BloodGroup = createDto.BloodGroup,
                EmergencyContactName = createDto.EmergencyContactName,
                EmergencyContactPhone = createDto.EmergencyContactPhone,
                MedicalHistory = createDto.MedicalHistory,
                Allergies = createDto.Allergies,
                IsActive = true
            };

            var createdPatient = await _patientRepository.CreateAsync(patient);
            return MapToDto(createdPatient);
        }

        public async Task<PatientDto> UpdatePatientAsync(UpdatePatientDto updateDto)
        {
            var existingPatient = await _patientRepository.GetByIdAsync(updateDto.Id);
            if (existingPatient == null)
                throw new KeyNotFoundException($"Patient with ID {updateDto.Id} not found");

            // Update only fields that can be modified via API
            existingPatient.FirstName = updateDto.FirstName;
            existingPatient.LastName = updateDto.LastName;
            existingPatient.DateOfBirth = updateDto.DateOfBirth;
            existingPatient.Gender = updateDto.Gender;
            existingPatient.Email = updateDto.Email;
            existingPatient.PhoneNumber = updateDto.PhoneNumber;
            existingPatient.Address = updateDto.Address;
            existingPatient.City = updateDto.City;
            existingPatient.State = updateDto.State;
            existingPatient.ZipCode = updateDto.ZipCode;
            existingPatient.BloodGroup = updateDto.BloodGroup;
            existingPatient.EmergencyContactName = updateDto.EmergencyContactName;
            existingPatient.EmergencyContactPhone = updateDto.EmergencyContactPhone;
            existingPatient.MedicalHistory = updateDto.MedicalHistory;
            existingPatient.Allergies = updateDto.Allergies;
            
            // Update timestamp (audit field - not in DTO, only in Entity)
            existingPatient.UpdatedAt = DateTime.UtcNow;
            // Note: UpdatedBy would be set here based on authenticated user context

            var updatedPatient = await _patientRepository.UpdateAsync(existingPatient);
            return MapToDto(updatedPatient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            return await _patientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm)
        {
            var patients = await _patientRepository.SearchAsync(searchTerm);
            return patients.Select(MapToDto);
        }

        /// <summary>
        /// Maps a Patient Entity to a PatientDto, ensuring:
        /// 1. Only safe fields are exposed (excludes CreatedBy, UpdatedBy)
        /// 2. Computed properties (FullName, Age) are converted to plain values
        /// 3. Internal audit fields remain hidden from API responses
        /// </summary>
        private static PatientDto MapToDto(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                // FullName: Computed property from Entity → Plain value in DTO
                FullName = patient.FullName,
                DateOfBirth = patient.DateOfBirth,
                // Age: Computed property from Entity → Plain value in DTO
                Age = patient.Age,
                Gender = patient.Gender,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                Address = patient.Address,
                City = patient.City,
                State = patient.State,
                ZipCode = patient.ZipCode,
                BloodGroup = patient.BloodGroup,
                EmergencyContactName = patient.EmergencyContactName,
                EmergencyContactPhone = patient.EmergencyContactPhone,
                MedicalHistory = patient.MedicalHistory,
                Allergies = patient.Allergies,
                IsActive = patient.IsActive,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt
                // Note: CreatedBy and UpdatedBy are intentionally excluded for security
            };
        }
    }
}

