using Hospital.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<PatientDto> CreatePatientAsync(CreatePatientDto createDto);
        Task<PatientDto> UpdatePatientAsync(UpdatePatientDto updateDto);
        Task<bool> DeletePatientAsync(int id);
        Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm);
    }
}

