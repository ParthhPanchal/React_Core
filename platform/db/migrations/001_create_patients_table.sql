-- =============================================
-- Patient Information Management System
-- Migration: 001 - Create Patients Table
-- =============================================

-- Create Patients table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Patients]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Patients] (
        [Id] INT PRIMARY KEY IDENTITY(1,1),
        [FirstName] NVARCHAR(100) NOT NULL,
        [LastName] NVARCHAR(100) NOT NULL,
        [DateOfBirth] DATE NOT NULL,
        [Gender] NVARCHAR(10) NOT NULL CHECK (Gender IN ('Male', 'Female', 'Other')),
        [Email] NVARCHAR(255) NULL,
        [PhoneNumber] NVARCHAR(20) NOT NULL,
        [Address] NVARCHAR(500) NULL,
        [City] NVARCHAR(100) NULL,
        [State] NVARCHAR(100) NULL,
        [ZipCode] NVARCHAR(10) NULL,
        [BloodGroup] NVARCHAR(5) NULL CHECK (BloodGroup IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-')),
        [EmergencyContactName] NVARCHAR(200) NULL,
        [EmergencyContactPhone] NVARCHAR(20) NULL,
        [MedicalHistory] NVARCHAR(MAX) NULL,
        [Allergies] NVARCHAR(MAX) NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        [CreatedBy] NVARCHAR(100) NULL,
        [UpdatedBy] NVARCHAR(100) NULL
    );
    
    -- Create indexes for better performance
    CREATE INDEX IX_Patients_LastName ON [dbo].[Patients] (LastName);
    CREATE INDEX IX_Patients_Email ON [dbo].[Patients] (Email);
    CREATE INDEX IX_Patients_PhoneNumber ON [dbo].[Patients] (PhoneNumber);
    CREATE INDEX IX_Patients_IsActive ON [dbo].[Patients] (IsActive);
    CREATE INDEX IX_Patients_CreatedAt ON [dbo].[Patients] (CreatedAt);
    
    PRINT 'Patients table created successfully.';
END
ELSE
BEGIN
    PRINT 'Patients table already exists.';
END
GO

-- Create trigger for automatic UpdatedAt timestamp
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'TR_Patients_UpdatedAt')
BEGIN
    DROP TRIGGER TR_Patients_UpdatedAt;
END
GO

CREATE TRIGGER TR_Patients_UpdatedAt
ON [dbo].[Patients]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Patients]
    SET UpdatedAt = GETUTCDATE()
    FROM [dbo].[Patients] p
    INNER JOIN inserted i ON p.Id = i.Id;
END
GO

PRINT 'Migration 001 completed successfully.';
GO

