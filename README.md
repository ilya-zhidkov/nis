# NIS

Výukový **N**emocenský **I**nformační **S**ystém pro Krajskou zdravotní a.s. v Ústí nad Labem.

---

## TABLE OF CONTENTS

* [Requirements](#requirements)
    + [Back-end](#back-end)
* [Setup](#setup)
    + [Core](#core)

---

## Requirements

### Back-end

- .NET 5.0 SDK (https://dotnet.microsoft.com/download/dotnet-core/5.0)
- SQLite (https://www.sqlite.org/download.html)
- _(optional)_ dotnet-ef (https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

---

## Setup

### Core

1. Apply existing database migrations

    ```bash
    Nis.Core> dotnet ef database update
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
