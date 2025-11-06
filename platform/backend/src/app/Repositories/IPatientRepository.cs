using Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Application.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> CreateAsync(Patient patient);
        Task<Patient> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Patient>> SearchAsync(string searchTerm);
    }
}

