import { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import { patientService } from '../services/patientService';
import { Patient } from '../types/Patient';
import './PatientDetails.css';

const PatientDetails = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [patient, setPatient] = useState<Patient | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (id) {
      loadPatient(parseInt(id));
    }
  }, [id]);

  const loadPatient = async (patientId: number) => {
    try {
      setLoading(true);
      const data = await patientService.getPatientById(patientId);
      setPatient(data);
    } catch (error) {
      toast.error('Failed to load patient');
      navigate('/patients');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!patient || !window.confirm(`Are you sure you want to delete ${patient.fullName}?`)) {
      return;
    }

    try {
      await patientService.deletePatient(patient.id);
      toast.success('Patient deleted successfully');
      navigate('/patients');
    } catch (error) {
      toast.error('Failed to delete patient');
      console.error(error);
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  const formatDateTime = (dateString: string) => {
    return new Date(dateString).toLocaleString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  };

  if (loading) {
    return (
      <div className="loading">
        <div className="spinner"></div>
      </div>
    );
  }

  if (!patient) {
    return null;
  }

  return (
    <div className="patient-details-page">
      <div className="page-header">
        <div>
          <Link to="/patients" className="back-link">
            ← Back to Patients
          </Link>
          <h1 className="page-title">{patient.fullName}</h1>
          <p className="page-subtitle">
            Patient ID: {patient.id}
          </p>
        </div>
        <div className="header-actions">
          <Link to={`/patients/${patient.id}/edit`} className="btn btn-primary">
            Edit Patient
          </Link>
          <button onClick={handleDelete} className="btn btn-danger">
            Delete Patient
          </button>
        </div>
      </div>

      <div className="details-grid">
        <div className="card">
          <h2 className="section-title">Personal Information</h2>
          <div className="details-list">
            <div className="detail-row">
              <span className="detail-label">Full Name</span>
              <span className="detail-value">{patient.fullName}</span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Date of Birth</span>
              <span className="detail-value">{formatDate(patient.dateOfBirth)}</span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Age</span>
              <span className="detail-value">{patient.age} years</span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Gender</span>
              <span className="detail-value">{patient.gender}</span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Blood Group</span>
              <span className="detail-value">
                {patient.bloodGroup || 'Not specified'}
              </span>
            </div>
          </div>
        </div>

        <div className="card">
          <h2 className="section-title">Contact Information</h2>
          <div className="details-list">
            <div className="detail-row">
              <span className="detail-label">Email</span>
              <span className="detail-value">
                {patient.email ? (
                  <a href={`mailto:${patient.email}`}>{patient.email}</a>
                ) : (
                  'Not provided'
                )}
              </span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Phone Number</span>
              <span className="detail-value">
                <a href={`tel:${patient.phoneNumber}`}>{patient.phoneNumber}</a>
              </span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Address</span>
              <span className="detail-value">
                {patient.address || 'Not provided'}
              </span>
            </div>
            <div className="detail-row">
              <span className="detail-label">City</span>
              <span className="detail-value">{patient.city || 'Not provided'}</span>
            </div>
            <div className="detail-row">
              <span className="detail-label">State</span>
              <span className="detail-value">{patient.state || 'Not provided'}</span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Zip Code</span>
              <span className="detail-value">{patient.zipCode || 'Not provided'}</span>
            </div>
          </div>
        </div>

        <div className="card">
          <h2 className="section-title">Emergency Contact</h2>
          <div className="details-list">
            <div className="detail-row">
              <span className="detail-label">Contact Name</span>
              <span className="detail-value">
                {patient.emergencyContactName || 'Not provided'}
              </span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Contact Phone</span>
              <span className="detail-value">
                {patient.emergencyContactPhone ? (
                  <a href={`tel:${patient.emergencyContactPhone}`}>
                    {patient.emergencyContactPhone}
                  </a>
                ) : (
                  'Not provided'
                )}
              </span>
            </div>
          </div>
        </div>

        <div className="card">
          <h2 className="section-title">Medical Information</h2>
          <div className="details-list">
            <div className="detail-row">
              <span className="detail-label">Medical History</span>
              <span className="detail-value">
                {patient.medicalHistory || 'No medical history recorded'}
              </span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Allergies</span>
              <span className="detail-value">
                {patient.allergies || 'No known allergies'}
              </span>
            </div>
          </div>
        </div>

        <div className="card" style={{ gridColumn: '1 / -1' }}>
          <h2 className="section-title">Record Information</h2>
          <div className="details-list">
            <div className="detail-row">
              <span className="detail-label">Status</span>
              <span className="detail-value">
                <span className={`badge ${patient.isActive ? 'badge-success' : 'badge-danger'}`}>
                  {patient.isActive ? 'Active' : 'Inactive'}
                </span>
              </span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Created At</span>
              <span className="detail-value">{formatDateTime(patient.createdAt)}</span>
            </div>
            <div className="detail-row">
              <span className="detail-label">Last Updated</span>
              <span className="detail-value">{formatDateTime(patient.updatedAt)}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PatientDetails;

