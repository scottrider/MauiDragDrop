# MauiDragDrop

MauiDragDrop is a sample application exploring drag-and-drop support using .NET MAUI. It currently targets Windows through WinUI and demonstrates cross-platform UI capabilities.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or newer for building MAUI and WinUI projects.
- Visual Studio 2022 with the MAUI and WinUI workloads installed.
- [Node.js](https://nodejs.org/) if you plan to experiment with the planned Tauri desktop integration.

## Building the .NET MAUI Project

1. Restore dependencies and build the solution:

   ```bash
   dotnet build MauiDragDrop.sln
   ```

2. Run the MAUI app:

   ```bash
   dotnet build MauiDragDrop -t:Run -f net10.0
   ```

## Building the WinUI Project

1. Navigate to the WinUI project and build for Windows:

   ```bash
   dotnet build MauiDragDrop.WinUI/MauiDragDrop.WinUI.csproj -f net10.0-windows10.0.19041.0
   ```

2. To run the WinUI app, use Visual Studio or the `dotnet run` command with the desired runtime identifier.

## Planned Tauri Integration

There is ongoing work to explore running the MAUI UI inside a Tauri shell for a lightweight desktop deployment. This requires Node.js and the Tauri CLI installed globally. Instructions for building with Tauri will be added as the integration progresses.
