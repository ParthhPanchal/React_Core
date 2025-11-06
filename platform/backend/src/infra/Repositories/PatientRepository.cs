using Dapper;
using Hospital.Application.Repositories;
using Hospital.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;

        public PatientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            using var connection = CreateConnection();
            
            var sql = @"
                SELECT 
                    Id, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber,
                    Address, City, State, ZipCode, BloodGroup,
                    EmergencyContactName, EmergencyContactPhone,
                    MedicalHistory, Allergies, IsActive,
                    CreatedAt, UpdatedAt, CreatedBy, UpdatedBy
                FROM Patients
                WHERE IsActive = 1
                ORDER BY CreatedAt DESC";

            var patients = await connection.QueryAsync<Patient>(sql);
            return patients;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            
            var sql = @"
                SELECT 
                    Id, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber,
                    Address, City, State, ZipCode, BloodGroup,
                    EmergencyContactName, EmergencyContactPhone,
                    MedicalHistory, Allergies, IsActive,
                    CreatedAt, UpdatedAt, CreatedBy, UpdatedBy
                FROM Patients
                WHERE Id = @Id";

            var patient = await connection.QueryFirstOrDefaultAsync<Patient>(sql, new { Id = id });
            return patient;
        }

        public async Task<Patient> CreateAsync(Patient patient)
        {
            using var connection = CreateConnection();
            
            var sql = @"
                INSERT INTO Patients (
                    FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber,
                    Address, City, State, ZipCode, BloodGroup,
                    EmergencyContactName, EmergencyContactPhone,
                    MedicalHistory, Allergies, IsActive, CreatedAt, UpdatedAt, CreatedBy, UpdatedBy
                )
                VALUES (
                    @FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber,
                    @Address, @City, @State, @ZipCode, @BloodGroup,
                    @EmergencyContactName, @EmergencyContactPhone,
                    @MedicalHistory, @Allergies, @IsActive, @CreatedAt, @UpdatedAt, @CreatedBy, @UpdatedBy
                );
                
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            patient.CreatedAt = DateTime.UtcNow;
            patient.UpdatedAt = DateTime.UtcNow;
            patient.IsActive = true;

            var id = await connection.ExecuteScalarAsync<int>(sql, patient);
            patient.Id = id;

            return patient;
        }

        public async Task<Patient> UpdateAsync(Patient patient)
        {
            using var connection = CreateConnection();
            
            var sql = @"
                UPDATE Patients
                SET 
                    FirstName = @FirstName,
                    LastName = @LastName,
                    DateOfBirth = @DateOfBirth,
                    Gender = @Gender,
                    Email = @Email,
                    PhoneNumber = @PhoneNumber,
                    Address = @Address,
                    City = @City,
                    State = @State,
                    ZipCode = @ZipCode,
                    BloodGroup = @BloodGroup,
                    EmergencyContactName = @EmergencyContactName,
                    EmergencyContactPhone = @EmergencyContactPhone,
                    MedicalHistory = @MedicalHistory,
                    Allergies = @Allergies,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE Id = @Id";

            patient.UpdatedAt = DateTime.UtcNow;
            
            await connection.ExecuteAsync(sql, patient);
            
            return patient;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            
            var sql = @"
                UPDATE Patients
                SET 
                    IsActive = 0,
                    UpdatedAt = @UpdatedAt
                WHERE Id = @Id";

            var rowsAffected = await connection.ExecuteAsync(sql, new 
            { 
                Id = id, 
                UpdatedAt = DateTime.UtcNow 
            });

            return rowsAffected > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            using var connection = CreateConnection();
            
            var sql = "SELECT COUNT(1) FROM Patients WHERE Id = @Id";
            
            var count = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
            
            return count > 0;
        }

        public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm)
        {
            using var connection = CreateConnection();

            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            var sql = @"
                SELECT 
                    Id, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber,
                    Address, City, State, ZipCode, BloodGroup,
                    EmergencyContactName, EmergencyContactPhone,
                    MedicalHistory, Allergies, IsActive,
                    CreatedAt, UpdatedAt, CreatedBy, UpdatedBy
                FROM Patients
                WHERE IsActive = 1
                    AND (
                        FirstName LIKE @SearchTerm
                        OR LastName LIKE @SearchTerm
                        OR Email LIKE @SearchTerm
                        OR PhoneNumber LIKE @SearchTerm
                    )
                ORDER BY CreatedAt DESC";

            var searchPattern = $"%{searchTerm}%";
            var patients = await connection.QueryAsync<Patient>(sql, new { SearchTerm = searchPattern });
            
            return patients;
        }
    }
}
