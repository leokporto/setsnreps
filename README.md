# Sets n' Reps 🏋️‍♂️📲

**SetsnReps** is a workout tracking app (inspired by Hevy), built using .NET 8 and Blazor WebAssembly.  
The entire project is being documented through a YouTube series on [@dotnetOnTap](https://www.youtube.com/@dotnetOnTap), where we combine .NET, coding, beer, fitness, and more.

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
├── frontend/
│   └── SetsnReps.Web/
├── shared/
│   └── SetsnReps.Core/
├── README.md
├── LICENSE
└── .gitignore
```

---

## 🧪 Running Locally

### Backend (API)

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

## 🎥 Follow the Series
📺 YouTube - [@dotnetOnTap](https://www.youtube.com/@dotnetOnTap)
Weekly updates, deep-dives, and hands-on development


*Built with .NET. Fueled by sets, reps, coffe, and sometimes beer.*
