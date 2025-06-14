# Sets n' Reps 🏋️‍♂️📲

**SetsnReps** is a workout tracking app (inspired by Hevy), built using .NET 8 and Blazor WebAssembly.

---

## ⚙️ Tech Stack

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Minimal APIs
- Entity Framework Core
- PostgreSQL
- Blazor WebAssembly
- MudBlazor (UI Components)
- JWT Authentication
- Docker (in progress)

---

## 🎯 MVP Goals

- ✅ Create and manage exercises
- ✅ Log workouts (sets, reps, weight, notes)
- ✅ View workout history
- 🚧 User authentication
- 🚧 Sync with mobile (future with .NET MAUI)
- 🚧 Export data (CSV, PDF)

---

## 📁 Project Structure

```plaintext
/setsnreps
├── backend/
│   └── SetsnReps.Api/
│   └── SetsnReps.Domain/
├── frontend/
│   └── SetsnReps.Web/
├── shared/
│   └── SetsnReps.Core/
├── zDocker/
├── README.md
├── LICENSE
└── .gitignore
```

---

## 🧪 Running Locally

### PgSql no docker (development and tests)
1. Create a `.env` file in the docker directory with the following content:
   ```plaintext
   POSTGRES_PASSWORD=<YourPassword>
   POSTGRES_DB=setsnreps
   ```
   
2. Start PostgreSQL, with the `.env` file created.
   ```bash
   docker compose -p <project_name> --env-file setsnreps.env up
   ```

### Backend (API)

1. Create a user secrets file (secrets.json) on SetsNReps.API project, with information regarding the database connection string, using the same info written on `setsnreps.env` file.
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=setsnreps;Username=postgres;Password=<YourPassword>"
     }
   }
   ```

2. Run the migrations to create the database schema.
   ```bash
    cd backend/SetsnReps.Api
    dotnet ef database update
    ```

3. On a database tool (like Azure Data Studio), run the `schema_and_import.sql` file to create the tables and import the initial data. The script can be found at `/tmp/setsnreps.sql` in the docker container.  
 
4. Run the API:

```bash
cd backend/SetsnReps.Api
dotnet run
```

Make sure PostgreSQL is running and the connection string is configured in appsettings.json.

### Frontend (Blazor)

```bash
cd frontend/SetsnReps.Web
dotnet run
```

---


*Built with .NET. Fueled by sets, reps, coffe, and sometimes beer.*
