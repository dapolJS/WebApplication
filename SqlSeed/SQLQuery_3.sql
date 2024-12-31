-- seed.sql
USE FirstWebApiNotes; -- your database name
GO

-- Enable IDENTITY_INSERT to allow explicit values to be inserted into the identity column
SET IDENTITY_INSERT NotesAppSchema.Note ON;
GO

-- Insert your seed data with explicit Id values
INSERT INTO NotesAppSchema.Note (Id, Title, [Description], Done) 
VALUES (2, 'SeedTitle', 'SeedDescription', 0);

INSERT INTO NotesAppSchema.Note (Id, Title, [Description], Done) 
VALUES (6, 'SeedTitle', 'SeedDescription', 0);

-- Disable IDENTITY_INSERT after inserting the data
SET IDENTITY_INSERT NotesAppSchema.Note OFF;
GO