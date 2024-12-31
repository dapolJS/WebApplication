-- seed.sql
USE FirstWebApiNotes; -- your database name
GO

SELECT * FROM NotesAppSchema.Note

-- Example of inserting seed data into a table
INSERT INTO NotesAppSchema.Note (Id, Title, [Description], Done) VALUES ('2', 'SeedTitle', 'SeedDescription', 0);
INSERT INTO NotesAppSchema.Note (Id, Title, [Description], Done) VALUES ('6', 'SeedTitle', 'SeedDescription', 0);
