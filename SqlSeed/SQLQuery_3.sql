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

GO

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription');

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription');

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription');

INSERT INTO NotesAppSchema.Notebook (Title, UniqueKey) 
VALUES ('SeedTitle', 'SeedDescription');

GO

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey-1');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey-2');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey-3');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey-4');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey-5');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey-6');

DELETE FROM NotesAppSchema.Room


SELECT * FROM NotesAppSchema.Notebook
SELECT * FROM NotesAppSchema.Note
SELECT * FROm NotesAppSchema.Room
GO