# NIS

Výukový **N**emocniční **I**nformační **S**ystém pro fakultu zdravotnických studií v Ústí nad Labem.

---

## TABLE OF CONTENTS

* [Requirements](#requirements)
    + [Back End](#back-end)
    + [Front End](#front-end)
* [Setup](#setup)
    + [Core](#core)
    + [Web API](#web-api)
    + [WPF](#wpf)
    + [Tests](#tests)

---

## Requirements

### Back End

- .NET 6.0 SDK (https://dotnet.microsoft.com/download/dotnet-core/6.0)
- SQLite (https://www.sqlite.org/download.html)
- _(optional)_ dotnet-ef (https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

### Front End

- .NET 6.0 SDK (https://dotnet.microsoft.com/download/dotnet-core/6.0)

---

## Setup

### Core

1. Apply existing database migrations

    ```bash
    Nis.Core> dotnet ef database update
    ```

2. Locate database on disk

    ```bash
   # Change directory to user's folder
   > cd C:\Users\<username>\AppData\Roaming
   
   # Enter into SQLite environment
   > C:\Users\<username>\AppData\Roaming sqlite3
   
   # Preview database schema in SQLite
   sqlite> .open nis.db
   ```

### Web API

1. Listen for changes on https://localhost:5001

    ```bash
    Nis.Api> dotnet watch run
    ```

2. Inspect Swagger Open API Specification

    ```bash
    https://localhost:5001/swagger
    ```

### WPF

1. Set **Nis.WpfApp** as a startup project (if not already).
2. Press `F5` to run in debug mode or `Ctrl + F5` to start without debugging.

### Tests

   ```bash
   # Switch to test environment
   tests> setx ASPNETCORE_ENVIRONMENT "Test"
   
   # IMPORTANT! Reload the command-line window.
   
   # Check if the environment variable has been set.
   tests> set ASPNETCORE_ENVIRONMENT
   ```
