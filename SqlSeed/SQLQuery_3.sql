-- seed.sql
USE FirstWebApiNotes; -- your database name
GO

-- Insert your seed data with explicit Id values
INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);

INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);

-- Insert your seed data with explicit Id values
INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);

INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);-- Insert your seed data with explicit Id values

INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);

INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);-- Insert your seed data with explicit Id values

INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);

INSERT INTO NotesAppSchema.Note (Title, [Description], Done) 
VALUES ('SeedTitle', 'SeedDescription', 0);

SELECT * FROM NotesAppSchema.Note

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription');

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription');

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription', 0);

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription', 0);

SELECT * FROM NotesAppSchema.Notebook

GO