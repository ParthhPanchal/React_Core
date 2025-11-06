import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { toast } from 'react-toastify';
import { patientService } from '../services/patientService';
import { CreatePatientDto } from '../types/Patient';
import './PatientForm.css';

const PatientForm = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [initialLoading, setInitialLoading] = useState(!!id);

  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<CreatePatientDto>();

  useEffect(() => {
    if (id) {
      loadPatient(parseInt(id));
    }
  }, [id]);

  const loadPatient = async (patientId: number) => {
    try {
      setInitialLoading(true);
      const patient = await patientService.getPatientById(patientId);
      reset({
        firstName: patient.firstName,
        lastName: patient.lastName,
        dateOfBirth: patient.dateOfBirth.split('T')[0],
        gender: patient.gender,
        email: patient.email,
        phoneNumber: patient.phoneNumber,
        address: patient.address,
        city: patient.city,
        state: patient.state,
        zipCode: patient.zipCode,
        bloodGroup: patient.bloodGroup,
        emergencyContactName: patient.emergencyContactName,
        emergencyContactPhone: patient.emergencyContactPhone,
        medicalHistory: patient.medicalHistory,
        allergies: patient.allergies,
      });
    } catch (error) {
      toast.error('Failed to load patient');
      navigate('/patients');
    } finally {
      setInitialLoading(false);
    }
  };

  const onSubmit = async (data: CreatePatientDto) => {
    try {
      setLoading(true);
      if (id) {
        await patientService.updatePatient(parseInt(id), { ...data, id: parseInt(id) });
        toast.success('Patient updated successfully');
      } else {
        await patientService.createPatient(data);
        toast.success('Patient created successfully');
      }
      navigate('/patients');
    } catch (error) {
      toast.error(id ? 'Failed to update patient' : 'Failed to create patient');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  if (initialLoading) {
    return (
      <div className="loading">
        <div className="spinner"></div>
      </div>
    );
  }

  return (
    <div className="patient-form-page">
      <div className="page-header">
        <h1 className="page-title">{id ? 'Edit Patient' : 'Add New Patient'}</h1>
        <p className="page-subtitle">
          {id ? 'Update patient information' : 'Enter patient information'}
        </p>
      </div>

      <div className="card">
        <form onSubmit={handleSubmit(onSubmit)} className="patient-form">
          <div className="form-section">
            <h2 className="form-section-title">Personal Information</h2>
            <div className="grid grid-cols-2">
              <div className="form-group">
                <label className="form-label required">First Name</label>
                <input
                  type="text"
                  className={`form-control ${errors.firstName ? 'error' : ''}`}
                  {...register('firstName', {
                    required: 'First name is required',
                    maxLength: {
                      value: 100,
                      message: 'First name cannot exceed 100 characters',
                    },
                  })}
                />
                {errors.firstName && (
                  <p className="form-error">{errors.firstName.message}</p>
                )}
              </div>

              <div className="form-group">
                <label className="form-label required">Last Name</label>
                <input
                  type="text"
                  className={`form-control ${errors.lastName ? 'error' : ''}`}
                  {...register('lastName', {
                    required: 'Last name is required',
                    maxLength: {
                      value: 100,
                      message: 'Last name cannot exceed 100 characters',
                    },
                  })}
                />
                {errors.lastName && (
                  <p className="form-error">{errors.lastName.message}</p>
                )}
              </div>

              <div className="form-group">
                <label className="form-label required">Date of Birth</label>
                <input
                  type="date"
                  className={`form-control ${errors.dateOfBirth ? 'error' : ''}`}
                  {...register('dateOfBirth', {
                    required: 'Date of birth is required',
                  })}
                />
                {errors.dateOfBirth && (
                  <p className="form-error">{errors.dateOfBirth.message}</p>
                )}
              </div>

              <div className="form-group">
                <label className="form-label required">Gender</label>
                <select
                  className={`form-control ${errors.gender ? 'error' : ''}`}
                  {...register('gender', { required: 'Gender is required' })}
                >
                  <option value="">Select Gender</option>
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                  <option value="Other">Other</option>
                </select>
                {errors.gender && (
                  <p className="form-error">{errors.gender.message}</p>
                )}
              </div>

              <div className="form-group">
                <label className="form-label">Blood Group</label>
                <select className="form-control" {...register('bloodGroup')}>
                  <option value="">Select Blood Group</option>
                  <option value="A+">A+</option>
                  <option value="A-">A-</option>
                  <option value="B+">B+</option>
                  <option value="B-">B-</option>
                  <option value="AB+">AB+</option>
                  <option value="AB-">AB-</option>
                  <option value="O+">O+</option>
                  <option value="O-">O-</option>
                </select>
              </div>
            </div>
          </div>

          <div className="form-section">
            <h2 className="form-section-title">Contact Information</h2>
            <div className="grid grid-cols-2">
              <div className="form-group">
                <label className="form-label">Email</label>
                <input
                  type="email"
                  className={`form-control ${errors.email ? 'error' : ''}`}
                  {...register('email', {
                    pattern: {
                      value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                      message: 'Invalid email address',
                    },
                  })}
                />
                {errors.email && (
                  <p className="form-error">{errors.email.message}</p>
                )}
              </div>

              <div className="form-group">
                <label className="form-label required">Phone Number</label>
                <input
                  type="tel"
                  className={`form-control ${errors.phoneNumber ? 'error' : ''}`}
                  {...register('phoneNumber', {
                    required: 'Phone number is required',
                    maxLength: {
                      value: 20,
                      message: 'Phone number cannot exceed 20 characters',
                    },
                  })}
                />
                {errors.phoneNumber && (
                  <p className="form-error">{errors.phoneNumber.message}</p>
                )}
              </div>

              <div className="form-group" style={{ gridColumn: '1 / -1' }}>
                <label className="form-label">Address</label>
                <input
                  type="text"
                  className="form-control"
                  {...register('address')}
                />
              </div>

              <div className="form-group">
                <label className="form-label">City</label>
                <input
                  type="text"
                  className="form-control"
                  {...register('city')}
                />
              </div>

              <div className="form-group">
                <label className="form-label">State</label>
                <input
                  type="text"
                  className="form-control"
                  {...register('state')}
                />
              </div>

              <div className="form-group">
                <label className="form-label">Zip Code</label>
                <input
                  type="text"
                  className="form-control"
                  {...register('zipCode')}
                />
              </div>
            </div>
          </div>

          <div className="form-section">
            <h2 className="form-section-title">Emergency Contact</h2>
            <div className="grid grid-cols-2">
              <div className="form-group">
                <label className="form-label">Emergency Contact Name</label>
                <input
                  type="text"
                  className="form-control"
                  {...register('emergencyContactName')}
                />
              </div>

              <div className="form-group">
                <label className="form-label">Emergency Contact Phone</label>
                <input
                  type="tel"
                  className="form-control"
                  {...register('emergencyContactPhone')}
                />
              </div>
            </div>
          </div>

          <div className="form-section">
            <h2 className="form-section-title">Medical Information</h2>
            <div className="grid grid-cols-1">
              <div className="form-group">
                <label className="form-label">Medical History</label>
                <textarea
                  className="form-control"
                  rows={4}
                  {...register('medicalHistory')}
                />
              </div>

              <div className="form-group">
                <label className="form-label">Allergies</label>
                <textarea
                  className="form-control"
                  rows={3}
                  {...register('allergies')}
                />
              </div>
            </div>
          </div>

          <div className="form-actions">
            <button
              type="button"
              onClick={() => navigate('/patients')}
              className="btn btn-secondary"
              disabled={loading}
            >
              Cancel
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Saving...' : id ? 'Update Patient' : 'Create Patient'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default PatientForm;

