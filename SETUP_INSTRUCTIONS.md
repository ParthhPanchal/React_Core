# Setup Instructions

## Prerequisites

Before setting up the Hospital Patient Management System, ensure you have the following installed:

- **.NET 8.0 SDK** - Download from [Microsoft](https://dotnet.microsoft.com/download)
- **Node.js 18+** - Download from [Node.js](https://nodejs.org/)
- **SQL Server** - Any edition (LocalDB, Express, or Full)
- **Visual Studio 2022** or **VS Code** (recommended)

## Step-by-Step Setup

### 1. Database Setup

#### Option A: Using SQL Server Management Studio (SSMS)

1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Open the file: `platform/db/migrations/001_create_patients_table.sql`
4. Execute the script (F5)
5. (Optional) Open the file: `platform/db/seed/001_seed_patients.sql`
6. Execute the script to add sample data

#### Option B: Using Command Line

```bash
# Navigate to migrations folder
cd platform/db/migrations

# Run the migration script
sqlcmd -S localhost -i 001_create_patients_table.sql

# Navigate to seed folder
cd ../seed

# Run the seed script (optional)
sqlcmd -S localhost -i 001_seed_patients.sql
```

### 2. Configure Database Connection

Edit `platform/backend/src/api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name (e.g., `localhost`, `(localdb)\MSSQLLocalDB`, `.\SQLEXPRESS`)

### 3. Backend Setup

#### Using Visual Studio
1. Open `platform/backend/Hospital.sln`
2. Set `Hospital.Api` as the startup project
3. Press F5 to run

#### Using Command Line
```bash
cd platform/backend
dotnet restore
dotnet build
cd src/api
dotnet run
```

**Expected Output:**
```
Now listening on: http://localhost:5000
Now listening on: https://localhost:5001
```

### 4. Frontend Setup

```bash
cd platform/frontend/web
npm install
npm run dev
```

**Expected Output:**
```
VITE v5.x.x  ready in xxx ms

➜  Local:   http://localhost:3000/
➜  Network: use --host to expose
```

### 5. Verify Installation

1. **Backend**: Open `http://localhost:5000` - You should see Swagger UI
2. **Frontend**: Open `http://localhost:3000` - You should see the patient management interface
3. **Test API**: In Swagger UI, try GET `/api/patients` - Should return patient data

## Configuration Options

### Changing Ports

#### Backend Port
Edit `platform/backend/src/api/Properties/launchSettings.json`:
```json
{
  "profiles": {
    "Hospital.Api": {
      "applicationUrl": "https://localhost:5001;http://localhost:5000"
    }
  }
}
```

#### Frontend Port
Edit `platform/frontend/web/vite.config.ts`:
```typescript
export default defineConfig({
  server: {
    port: 3000,  // Change this number
  },
})
```

### API URL Configuration

If your backend runs on a different port, update `platform/frontend/web/src/services/api.ts`:

```typescript
const API_BASE_URL = 'https://localhost:YOUR_PORT/api';
```

## Troubleshooting

### Database Issues

**Error**: "Cannot connect to database"
- Check SQL Server is running
- Verify connection string
- Ensure database exists

**Error**: "Login failed for user"
- Use Windows Authentication: `Trusted_Connection=True`
- Or use SQL Authentication: `User Id=username;Password=password`

### Backend Issues

**Error**: "Port already in use"
- Change port in `launchSettings.json`
- Or kill process using the port

**Error**: "Build failed"
- Run `dotnet clean`
- Run `dotnet restore`
- Check .NET 8.0 SDK is installed

### Frontend Issues

**Error**: "Cannot connect to API"
- Ensure backend is running
- Check API URL in `api.ts`
- Verify CORS settings

**Error**: "Module not found"
- Run `npm install`
- Clear cache: `npm cache clean --force`

## Production Deployment

### Backend
```bash
cd platform/backend/src/api
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
cd platform/frontend/web
npm run build
```

## Support

If you encounter issues during setup:

1. Check the troubleshooting section above
2. Verify all prerequisites are installed
3. Ensure all services are running
4. Check firewall and antivirus settings

For additional support, contact the development team.
