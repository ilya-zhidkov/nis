# NIS

Výukový **N**emocniční **I**nformační **S**ystém pro fakultu zdravotnických studií v Ústí nad Labem.

---

## TABLE OF CONTENTS

* [Requirements](#requirements)
    + [Back-end](#back-end)
    + [Front-end](#front-end)
* [Setup](#setup)
    + [Core](#core)
    + [Web API](#web-api)
    + [WPF](#wpf)
    + [Tests](#tests)

---

## Requirements

### Back-end

- .NET 5.0 SDK (https://dotnet.microsoft.com/download/dotnet-core/5.0)
- SQLite (https://www.sqlite.org/download.html)
- _(optional)_ dotnet-ef (https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

### Front-end

- .NET Framework 4.7.2 (https://dotnet.microsoft.com/download/dotnet-framework/net472)

---

## Setup

### Core

1. Apply existing database migrations

    ```bash
    # Default project: src\Nis.Core
    PM> Update-Database
    ```

2. Find database on disk

    ```bash
   # Change directory to user's folder
   > cd C:\Users\<username>\AppData\Roaming
   
   # Enter into SQLite environment
   > C:\Users\<username>\AppData\Roaming sqlite3
   
   # Preview database schema in SQLite
   sqlite> .open nis_development.db
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
