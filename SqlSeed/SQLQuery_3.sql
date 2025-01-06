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
VALUES ('SeedUniqueKey');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey');

INSERT INTO NotesAppSchema.Room (UniqueKey) 
VALUES ('SeedUniqueKey');


SELECT * FROM NotesAppSchema.Notebook
SELECT * FROM NotesAppSchema.Note
SELECT * FROm NotesAppSchema.Room
GO