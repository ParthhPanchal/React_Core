using Hospital.Application.DTOs;
using Hospital.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Api.Controllers
{
    /// <summary>
    /// API Controller for Patient operations.
    /// 
    /// IMPORTANT: This controller uses DTOs (Data Transfer Objects) for all input/output,
    /// NOT the Patient Entity directly. This ensures:
    /// - Security: Internal audit fields (CreatedBy, UpdatedBy) are never exposed
    /// - Stability: API contract remains independent of domain model changes
    /// - Clean Architecture: Separation between Domain and Application layers
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientDto>), 200)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        /// <summary>
        /// Get a patient by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatientDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PatientDto>> GetPatientById(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            
            if (patient == null)
                return NotFound(new { message = $"Patient with ID {id} not found" });
            
            return Ok(patient);
        }

        /// <summary>
        /// Create a new patient
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PatientDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PatientDto>> CreatePatient([FromBody] CreatePatientDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await _patientService.CreatePatientAsync(createDto);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Update an existing patient
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PatientDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PatientDto>> UpdatePatient(int id, [FromBody] UpdatePatientDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest(new { message = "ID in URL does not match ID in request body" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var patient = await _patientService.UpdatePatientAsync(updateDto);
                return Ok(patient);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Delete a patient (soft delete)
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            
            if (!result)
                return NotFound(new { message = $"Patient with ID {id} not found" });
            
            return NoContent();
        }

        /// <summary>
        /// Search patients by name, email, or phone
        /// </summary>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PatientDto>), 200)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> SearchPatients([FromQuery] string query)
        {
            var patients = await _patientService.SearchPatientsAsync(query);
            return Ok(patients);
        }
    }
}

