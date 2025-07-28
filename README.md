# MauiDragDrop

This repository contains a basic .NET MAUI sample demonstrating drag-and-drop functionality. The `tauri` folder includes a minimal Tauri project that can either launch the MAUI application or host a simple web-based drag-and-drop interface.

## Running the MAUI app directly

```bash
# from the repository root
dotnet run --project MauiDragDrop
```

## Using Tauri (desktop bundle)

1. Ensure you have **Rust**, **Cargo**, and **Node.js** installed.
2. The Tauri project lives in the `tauri` directory. It is configured to run the MAUI project during development and package a small web interface by default.

```bash
cd tauri
# build and run in dev mode
cargo run
```

When executed, Tauri will run `dotnet run --project ../MauiDragDrop` as specified in `src-tauri/tauri.conf.json` and open a window.
The `web` folder contains a simple `index.html` used when packaging the app.
