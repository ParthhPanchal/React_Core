-- =============================================
-- Seed Sample Patient Data
-- =============================================

-- Clear existing data (for demo purposes)
DELETE FROM [dbo].[Patients];
DBCC CHECKIDENT ('Patients', RESEED, 0);
GO

-- Insert sample patients
INSERT INTO [dbo].[Patients] (
    [FirstName], [LastName], [DateOfBirth], [Gender], [Email], [PhoneNumber],
    [Address], [City], [State], [ZipCode], [BloodGroup],
    [EmergencyContactName], [EmergencyContactPhone], [MedicalHistory], [Allergies], [IsActive]
)
VALUES
    (
        'John',
        'Doe',
        '1985-03-15',
        'Male',
        'john.doe@email.com',
        '+1-555-0101',
        '123 Main Street',
        'New York',
        'NY',
        '10001',
        'A+',
        'Jane Doe',
        '+1-555-0102',
        'Hypertension diagnosed in 2020',
        'Penicillin',
        1
    ),
    (
        'Sarah',
        'Smith',
        '1990-07-22',
        'Female',
        'sarah.smith@email.com',
        '+1-555-0201',
        '456 Oak Avenue',
        'Los Angeles',
        'CA',
        '90001',
        'B+',
        'Michael Smith',
        '+1-555-0202',
        'Asthma since childhood',
        'Aspirin, Peanuts',
        1
    ),
    (
        'Michael',
        'Johnson',
        '1978-11-08',
        'Male',
        'michael.j@email.com',
        '+1-555-0301',
        '789 Pine Road',
        'Chicago',
        'IL',
        '60601',
        'O+',
        'Emily Johnson',
        '+1-555-0302',
        'Type 2 Diabetes, managed with medication',
        'None',
        1
    ),
    (
        'Emily',
        'Williams',
        '1995-05-30',
        'Female',
        'emily.williams@email.com',
        '+1-555-0401',
        '321 Elm Street',
        'Houston',
        'TX',
        '77001',
        'AB+',
        'Robert Williams',
        '+1-555-0402',
        'No significant medical history',
        'Latex',
        1
    ),
    (
        'Robert',
        'Brown',
        '1982-09-12',
        'Male',
        'robert.brown@email.com',
        '+1-555-0501',
        '654 Maple Drive',
        'Phoenix',
        'AZ',
        '85001',
        'A-',
        'Lisa Brown',
        '+1-555-0502',
        'Previous surgery: Appendectomy (2015)',
        'Sulfa drugs',
        1
    ),
    (
        'Lisa',
        'Davis',
        '1988-12-25',
        'Female',
        'lisa.davis@email.com',
        '+1-555-0601',
        '987 Cedar Lane',
        'Philadelphia',
        'PA',
        '19101',
        'O-',
        'David Davis',
        '+1-555-0602',
        'Migraine headaches, monthly episodes',
        'None',
        1
    );

PRINT 'Sample patient data inserted successfully.';
PRINT 'Total patients: ' + CAST(@@ROWCOUNT AS NVARCHAR(10));
GO

-- Verify the data
SELECT 
    COUNT(*) as TotalPatients,
    SUM(CASE WHEN Gender = 'Male' THEN 1 ELSE 0 END) as MaleCount,
    SUM(CASE WHEN Gender = 'Female' THEN 1 ELSE 0 END) as FemaleCount
FROM [dbo].[Patients];
GO

