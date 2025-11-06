# Quick Start Guide

Get the Hospital Patient Management System up and running in minutes!

## Prerequisites Checklist

- [ ] .NET 8.0 SDK installed
- [ ] Node.js 18+ installed  
- [ ] SQL Server (LocalDB, Express, or Full) installed
- [ ] Git installed (optional)

## 🚀 5-Minute Setup

### Step 1: Setup Database (2 minutes)

**Option A: Using SQL Server Management Studio (SSMS)**
1. Open SSMS and connect to your SQL Server instance
2. Open `platform/db/migrations/001_create_patients_table.sql`
3. Execute the script (F5)
4. Open `platform/db/seed/001_seed_patients.sql`
5. Execute the script (F5)

**Option B: Using Command Line**
```bash
cd platform/db/migrations
sqlcmd -S localhost -i 001_create_patients_table.sql

cd ../seed
sqlcmd -S localhost -i 001_seed_patients.sql
```

**Verify Database:**
```sql
USE HospitalDB;
SELECT COUNT(*) FROM Patients;  -- Should return 6
```

### Step 2: Start Backend (1 minute)

```bash
cd platform/backend/src/api
dotnet run
```

Wait for: `Now listening on: http://localhost:5000`

**Test Backend:**
Open browser to `http://localhost:5000` - You should see Swagger UI

### Step 3: Start Frontend (2 minutes)

Open a new terminal:

```bash
cd platform/frontend/web
npm install
npm run dev
```

Wait for: `Local: http://localhost:3000`

**Test Frontend:**
Open browser to `http://localhost:3000` - You should see the patient list!

## 🎉 You're Done!

You should now see:
- 6 sample patients in the list
- Fully functional CRUD operations
- Search functionality
- Responsive UI

## Common Issues & Fixes

### Database Connection Failed

**Error:** `Cannot connect to database`

**Fix:** Update connection string in `platform/backend/src/api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Common server names:
- `localhost`
- `(localdb)\MSSQLLocalDB` (for LocalDB)
- `.\SQLEXPRESS` (for SQL Express)

**Test Connection:**
```bash
sqlcmd -S YOUR_SERVER_NAME -Q "SELECT @@VERSION"
```

### Backend Port Already in Use

**Error:** `Address already in use`

**Fix:** Change port in `platform/backend/src/api/Properties/launchSettings.json` or kill the process using port 5000:

```bash
# Windows
netstat -ano | findstr :5000
taskkill /PID <PID> /F

# Linux/Mac
lsof -i :5000
kill -9 <PID>
```

### Frontend Can't Connect to API

**Error:** `Network Error` in browser console

**Fix:** 
1. Ensure backend is running on `http://localhost:5000`
2. Check `platform/frontend/web/.env`:
   ```
   VITE_API_BASE_URL=http://localhost:5000/api
   ```
3. Restart frontend dev server

### npm install Fails

**Error:** Various npm errors

**Fix:**
```bash
# Clear cache and reinstall
rm -rf node_modules package-lock.json
npm cache clean --force
npm install
```

### .NET SDK Not Found

**Error:** `The command dotnet could not be found`

**Fix:** Install .NET 8.0 SDK from https://dotnet.microsoft.com/download

**Verify:**
```bash
dotnet --version  # Should show 8.0.x
```

## Next Steps

### Explore the Application

1. **View Patient List** - See all patients at `http://localhost:3000`
2. **Add Patient** - Click "Add Patient" button
3. **Edit Patient** - Click "Edit" on any patient card
4. **View Details** - Click "View Details" to see full patient info
5. **Search** - Use search bar to find patients
6. **Delete Patient** - Click "Delete" (soft delete)

### Explore the API

1. **Swagger UI** - Open `http://localhost:5000`
2. **Try Endpoints** - Use "Try it out" to test API calls
3. **View Schemas** - See request/response models

### Check the Code

**Backend Structure:**
```
platform/backend/src/
├── api/          # Controllers, startup
├── app/          # Services, DTOs, validators  
├── domain/       # Entities, enums
└── infra/        # DbContext, repositories
```

**Frontend Structure:**
```
platform/frontend/web/src/
├── components/   # Reusable components
├── pages/        # Page components
├── services/     # API calls
└── types/        # TypeScript types
```

## Development Workflow

### Making Changes

**Backend Changes:**
1. Edit code in `platform/backend/src/`
2. Stop backend (Ctrl+C)
3. Run `dotnet run` to restart
4. Changes are applied

**Frontend Changes:**
1. Edit code in `platform/frontend/web/src/`
2. Vite will auto-reload
3. No restart needed!

### Adding a New Patient Field

**Example: Add "Insurance Provider" field**

1. **Database:**
   ```sql
   ALTER TABLE Patients ADD InsuranceProvider NVARCHAR(100) NULL;
   ```

2. **Domain Entity** (`domain/Entities/Patient.cs`):
   ```csharp
   public string? InsuranceProvider { get; set; }
   ```

3. **DTOs** (`app/DTOs/...Dto.cs`):
   ```csharp
   public string? InsuranceProvider { get; set; }
   ```

4. **Service** (`app/Services/PatientService.cs`):
   ```csharp
   InsuranceProvider = createDto.InsuranceProvider
   ```

5. **Frontend Type** (`types/Patient.ts`):
   ```typescript
   insuranceProvider?: string;
   ```

6. **Frontend Form** (`pages/PatientForm.tsx`):
   ```tsx
   <input {...register('insuranceProvider')} />
   ```

## Testing Your Changes

### Test Backend
```bash
cd platform/backend/tests
dotnet test
```

### Test Frontend
```bash
cd platform/frontend/web
npm test
```

## Building for Production

### Backend
```bash
cd platform/backend/src/api
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
cd platform/frontend/web
npm run build
# Output in dist/ folder
```

## Getting Help

- **Documentation**: See `README.md` for detailed docs
- **Architecture**: See `docs/architecture/ARCHITECTURE.md`
- **API Contract**: See `platform/contracts/openapi.yaml`
- **Issues**: Check console logs and error messages

## Useful Commands

### Database
```bash
# Connect to SQL Server
sqlcmd -S localhost

# List databases
SELECT name FROM sys.databases;

# Use database
USE HospitalDB;

# List tables
SELECT * FROM INFORMATION_SCHEMA.TABLES;

# View patient count
SELECT COUNT(*) FROM Patients;
```

### Backend
```bash
# Restore packages
dotnet restore

# Build solution
dotnet build

# Run with watch (auto-reload)
dotnet watch run

# Clean build artifacts
dotnet clean
```

### Frontend
```bash
# Install packages
npm install

# Run dev server
npm run dev

# Build for production
npm run build

# Preview production build
npm run preview

# Lint code
npm run lint
```

## Configuration

### Environment Variables

**Backend** (`appsettings.json`):
- Connection strings
- Logging levels
- CORS origins

**Frontend** (`.env`):
- API base URL
- Other config

### Default Ports

- Frontend: `3000`
- Backend API: `5000`
- Backend HTTPS: `5001`
- SQL Server: `1433`

## Sample Credentials

This demo doesn't have authentication, but here are sample patients created by seed script:

1. John Doe - Male, A+
2. Sarah Smith - Female, B+
3. Michael Johnson - Male, O+
4. Emily Williams - Female, AB+
5. Robert Brown - Male, A-
6. Lisa Davis - Female, O-

## What's Next?

Check out the full `README.md` for:
- Detailed architecture
- Deployment guides
- Advanced features
- Security considerations
- Performance optimization

Happy coding! 🎉

