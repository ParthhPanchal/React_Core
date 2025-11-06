# Hospital Patient Management System

A complete full-stack web application for managing patient information in a hospital environment.

## 🏥 Features

- **Patient Management**: Create, view, edit, and delete patient records
- **Search Functionality**: Find patients by name, email, or phone number
- **Responsive Design**: Works on desktop, tablet, and mobile devices
- **Modern UI**: Clean, professional interface with intuitive navigation
- **Data Validation**: Client and server-side validation for data integrity
- **Soft Delete**: Patient records are marked as inactive rather than permanently deleted

## 🛠️ Technology Stack

### Backend
- **.NET 8.0** - Modern, cross-platform framework
- **ASP.NET Core Web API** - RESTful API endpoints
- **Dapper** - High-performance data access with inline SQL queries
- **SQL Server** - Robust relational database
- **FluentValidation** - Input validation
- **Swagger/OpenAPI** - API documentation

### Frontend
- **React 18** - Modern UI library
- **TypeScript** - Type-safe development
- **Vite** - Fast build tool and dev server
- **React Router** - Client-side routing
- **React Hook Form** - Form management
- **Axios** - HTTP client for API calls

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK
- Node.js 18+ 
- SQL Server (LocalDB, Express, or Full)

### 1. Database Setup

Run the database scripts in order:

```sql
-- 1. Create database and tables
-- Execute: platform/db/migrations/001_create_patients_table.sql

-- 2. Add sample data (optional)
-- Execute: platform/db/seed/001_seed_patients.sql
```

### 2. Backend Setup

```bash
cd platform/backend
dotnet restore
dotnet build
cd src/api
dotnet run
```

The API will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger UI**: `http://localhost:5000` (in development)

### 3. Frontend Setup

```bash
cd platform/frontend/web
npm install
npm run dev
```

The application will be available at `http://localhost:3000`

## 📁 Project Structure

```
Hospital Patient Management/
├── platform/
│   ├── backend/                 # .NET Core API
│   │   ├── src/
│   │   │   ├── api/            # Web API layer
│   │   │   ├── app/            # Application layer
│   │   │   ├── domain/         # Domain entities
│   │   │   └── infra/          # Data access layer
│   │   └── Hospital.sln        # Visual Studio solution
│   ├── frontend/web/           # React application
│   │   ├── src/
│   │   │   ├── components/     # Reusable components
│   │   │   ├── pages/          # Page components
│   │   │   ├── services/       # API services
│   │   │   └── types/          # TypeScript types
│   │   └── package.json
│   ├── db/                     # Database scripts
│   │   ├── migrations/         # Schema creation
│   │   └── seed/              # Sample data
│   └── contracts/             # API specification
│       └── openapi.yaml
└── README.md
```

## 🔧 Configuration

### Database Connection

Update the connection string in `platform/backend/src/api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### API Endpoints

- `GET /api/patients` - Get all patients
- `GET /api/patients/{id}` - Get patient by ID
- `POST /api/patients` - Create new patient
- `PUT /api/patients/{id}` - Update patient
- `DELETE /api/patients/{id}` - Delete patient (soft delete)
- `GET /api/patients/search?query={term}` - Search patients

## 📊 Database Schema

### Patients Table

| Column | Type | Description |
|--------|------|-------------|
| Id | INT IDENTITY | Primary key (auto-increment) |
| FirstName | NVARCHAR(100) | Patient's first name |
| LastName | NVARCHAR(100) | Patient's last name |
| DateOfBirth | DATE | Date of birth |
| Gender | NVARCHAR(10) | Male, Female, or Other |
| Email | NVARCHAR(255) | Email address (optional) |
| PhoneNumber | NVARCHAR(20) | Contact phone number |
| Address | NVARCHAR(500) | Street address |
| City | NVARCHAR(100) | City |
| State | NVARCHAR(100) | State |
| ZipCode | NVARCHAR(10) | Zip code |
| BloodGroup | NVARCHAR(5) | Blood type (A+, A-, B+, etc.) |
| EmergencyContactName | NVARCHAR(200) | Emergency contact name |
| EmergencyContactPhone | NVARCHAR(20) | Emergency contact phone |
| MedicalHistory | NVARCHAR(MAX) | Medical history notes |
| Allergies | NVARCHAR(MAX) | Known allergies |
| IsActive | BIT | Soft delete flag |
| CreatedAt | DATETIME2 | Creation timestamp |
| UpdatedAt | DATETIME2 | Last update timestamp |

## 🎯 Usage

### Adding a Patient
1. Click "Add Patient" button
2. Fill in the required fields (marked with *)
3. Click "Create Patient"

### Editing a Patient
1. Click "Edit" on any patient card
2. Modify the information
3. Click "Update Patient"

### Searching Patients
1. Use the search bar at the top
2. Search by name, email, or phone number
3. Results update in real-time

### Viewing Patient Details
1. Click "View Details" on any patient card
2. See complete patient information
3. Access edit and delete options

## 🔒 Security Features

- Input validation on both client and server
- SQL injection prevention via parameterized queries
- CORS configuration for API access
- HTTPS support for production

## 🚀 Deployment

### Backend Deployment
```bash
cd platform/backend/src/api
dotnet publish -c Release -o ./publish
```

### Frontend Deployment
```bash
cd platform/frontend/web
npm run build
```

Built files will be in the `dist` folder.

## 📝 API Documentation

Interactive API documentation is available via Swagger UI when running in development mode at `http://localhost:5000`

## 🐛 Troubleshooting

### Common Issues

1. **Database Connection Failed**
   - Check connection string in `appsettings.json`
   - Ensure SQL Server is running
   - Verify database exists

2. **Frontend Can't Connect to API**
   - Ensure backend is running
   - Check API URL in `src/services/api.ts`
   - Verify CORS configuration

3. **Build Errors**
   - Run `dotnet clean` and `dotnet restore`
   - Run `npm install` in frontend directory

## 📞 Support

For technical support or questions about this application, please contact the development team.

## 📄 License

This project is proprietary software. All rights reserved.

---

**Version**: 1.0.0  
**Last Updated**: 2024  
**Architecture**: Clean Architecture with Repository Pattern