# WebApplication

Web API's for NOTES with CRUD operations using EF
Idea for notes to be shared between clients.

1. DONE === I need to create some special token(room)
2. DONE === If notebook exists do not allow to create new one
3. DONE === Each note should be easily assigned to notebook without new notebook being created
4. DONE === Each note should be easy to edit if not entering any values or leaving empty strings
5. DONE === Throw bad requests instead of new errors
7. DONE === Fix note.Done issue that is note.Done not provided it autofills as false even if value existed
8. DONE === Try to init DTO's for notebook perhaps ? and others if possible
9. DONE === Separate Notebook controllers and Room controllers from Notes controlles
10. DONE === GetNotebooks endpoint should have functionality to filter data by partial title and return single notebook by id
12. DONE === Test all endpoits thoroughly!
13. DONE === GetNotes endpoint should have functionality to filter notes by partial title or partial description
14. DONE === GetNotes endpoint should have functionality to retrieve also single note by id
18. DONE === Implement github actions to run unittests
19. DONE === Implement connection to MSSQL
20. DONE === Implement unit tests solution
21. Implement real unit tests for models, controllers, services
22. Implement API smoke tests
23. DONE === Implement entityFramework with MSSQL for all endpoints instead of inmemory database
24. DONE === Implement database for testing
25. Implement Users who create entities
26. Implement timestamps
27. Add AUTH based on token with expiration time ?
28. DONE === Implement delete endpoint to delete 1 and delete all notes
29. DONE === Implement DeleteNotebook by id and delete all notebooks
30. DONE === Implement DeleteRoom by id or all rooms
31. Implement so that rooms with data can be viewed only if you have unique key
32. Implement to delete rooms notes and notebooks only by the creators
33. Test APP
34. Deploy to AWS or Azure
