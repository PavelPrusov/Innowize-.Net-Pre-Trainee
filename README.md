# .NET Pre-Trainee Solutions Repository

## Project structure

- **[Calculator](./1%20-%20Calculator/)** - Console calculator
- **[AsyncWorkDemonstration](./2%20-%20AsyncWorkDemonstration/)** - A brief overview of how asynchrony works  
- **[TaskManagement](./3%20-%20TaskManagement/)** - Console app 'task management' 
- **[LibraryManagementAPI](./Library.API/)** - Library WEB API

---

## To run migrations in Library API:

Run all EF Core migration commands from the Library.DataAccess folder,
since LibraryDbContextFactory is defined there.

```bash
cd Library.DataAccess

# Add migration
dotnet ef migrations add <Name>

# Apply migration
dotnet ef database update
```