import { ReactNode } from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Layout.css';

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  const location = useLocation();

  return (
    <div className="app">
      <header className="header">
        <div className="container">
          <div className="header-content">
            <Link to="/" className="logo">
              <span className="logo-icon">🏥</span>
              <span className="logo-text">Hospital Patient Management</span>
            </Link>
            <nav className="nav">
              <Link 
                to="/patients" 
                className={`nav-link ${location.pathname === '/patients' || location.pathname === '/' ? 'active' : ''}`}
              >
                Patients
              </Link>
              <Link 
                to="/patients/new" 
                className="btn btn-primary btn-sm"
              >
                + Add Patient
              </Link>
            </nav>
          </div>
        </div>
      </header>
      <main className="app-main">
        <div className="container">
          {children}
        </div>
      </main>
      <footer className="footer">
        <div className="container">
          <p className="text-center text-sm text-gray">
            &copy; 2024 Hospital Patient Management System. All rights reserved.
          </p>
        </div>
      </footer>
    </div>
  );
};

export default Layout;

