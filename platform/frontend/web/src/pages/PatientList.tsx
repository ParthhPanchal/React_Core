import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import { patientService } from '../services/patientService';
import { Patient } from '../types/Patient';
import './PatientList.css';

const PatientList = () => {
  const [patients, setPatients] = useState<Patient[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchQuery, setSearchQuery] = useState('');

  useEffect(() => {
    loadPatients();
  }, []);

  const loadPatients = async () => {
    try {
      setLoading(true);
      const data = await patientService.getAllPatients();
      setPatients(data);
    } catch (error) {
      toast.error('Failed to load patients');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleSearch = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!searchQuery.trim()) {
      loadPatients();
      return;
    }

    try {
      setLoading(true);
      const data = await patientService.searchPatients(searchQuery);
      setPatients(data);
    } catch (error) {
      toast.error('Failed to search patients');
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: number, name: string) => {
    if (!window.confirm(`Are you sure you want to delete ${name}?`)) {
      return;
    }

    try {
      await patientService.deletePatient(id);
      toast.success('Patient deleted successfully');
      loadPatients();
    } catch (error) {
      toast.error('Failed to delete patient');
      console.error(error);
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
    });
  };

  if (loading && patients.length === 0) {
    return (
      <div className="loading">
        <div className="spinner"></div>
      </div>
    );
  }

  return (
    <div className="patient-list">
      <div className="page-header">
        <h1 className="page-title">Patients</h1>
        <p className="page-subtitle">Manage patient information</p>
      </div>

      <div className="card">
        <form onSubmit={handleSearch} className="search-form">
          <input
            type="text"
            placeholder="Search by name, email, or phone..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            className="form-control"
          />
          <button type="submit" className="btn btn-primary">
            Search
          </button>
          {searchQuery && (
            <button
              type="button"
              onClick={() => {
                setSearchQuery('');
                loadPatients();
              }}
              className="btn btn-secondary"
            >
              Clear
            </button>
          )}
        </form>
      </div>

      {patients.length === 0 ? (
        <div className="card text-center">
          <p className="text-gray">No patients found</p>
          <Link to="/patients/new" className="btn btn-primary mt-4">
            Add Your First Patient
          </Link>
        </div>
      ) : (
        <div className="patients-grid">
          {patients.map((patient) => (
            <div key={patient.id} className="patient-card card">
              <div className="patient-card-header">
                <div>
                  <h3 className="patient-name">{patient.fullName}</h3>
                  <p className="patient-info">
                    {patient.age} years • {patient.gender}
                  </p>
                </div>
                {patient.bloodGroup && (
                  <span className="badge badge-info">{patient.bloodGroup}</span>
                )}
              </div>

              <div className="patient-details">
                <div className="patient-detail-item">
                  <span className="detail-label">📧 Email:</span>
                  <span className="detail-value">{patient.email || 'N/A'}</span>
                </div>
                <div className="patient-detail-item">
                  <span className="detail-label">📞 Phone:</span>
                  <span className="detail-value">{patient.phoneNumber}</span>
                </div>
                <div className="patient-detail-item">
                  <span className="detail-label">📍 Location:</span>
                  <span className="detail-value">
                    {patient.city && patient.state
                      ? `${patient.city}, ${patient.state}`
                      : 'N/A'}
                  </span>
                </div>
                <div className="patient-detail-item">
                  <span className="detail-label">📅 DOB:</span>
                  <span className="detail-value">
                    {formatDate(patient.dateOfBirth)}
                  </span>
                </div>
              </div>

              <div className="patient-card-actions">
                <Link
                  to={`/patients/${patient.id}`}
                  className="btn btn-outline"
                >
                  View Details
                </Link>
                <Link
                  to={`/patients/${patient.id}/edit`}
                  className="btn btn-secondary"
                >
                  Edit
                </Link>
                <button
                  onClick={() => handleDelete(patient.id, patient.fullName)}
                  className="btn btn-danger"
                >
                  Delete
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default PatientList;

