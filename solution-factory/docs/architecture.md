# Architecture

Monorepo with React frontend and ASP.NET Core Minimal API backend (REST).
Target hosting is Azure App Service.
Storage abstraction is repository interfaces with in-memory implementation, designed to swap to Azure Storage/database later.
Configuration uses appsettings + env vars; Key Vault planned.
