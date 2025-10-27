# Bin Pack problem

A Blazor Server App in which users have to try to put all vessels into an anchorage if it is possible.

## Overview

The Bin Pack Problem is a challenge where the users need to put all vessels inside a defined anchorage zone. Vessels can be dragged, dropped, and rotated to find the optimal arrangement. Anchorage and vessel sizes are fetched from an API. 

Opted for creating a Blazor Server App instead of Blazor WASM in order to avoid the CORS issue which occurs when trying to fetch data from the API. A proxy would have
been needed to make the API calls if we were to use a Blazor WASM for the task.

##  Features

- **Interactive Drag & Drop**: Click and drag vessels from the right side into the anchorage
- **Vessel Rotation**: Double-click any vessel to rotate it 90 degrees clockwise
- **Collision Detection**: Vessels cannot overlap with each other
- **Boundary Validation**: Vessels must be completely inside the anchorage to count
- **Progress Tracking**: Real-time counter showing how many vessels of each type are correctly placed

## 📁 Project Structure

```
instech-blazor-coding-task/
├── Components/
│   ├── App.razor                 # Root application component
│   └── Routes.razor              # Route configuration
├── Models/
│   └── Models.cs                 # Data models (Vessel, Anchorage, Fleet)
├── Pages/
│   ├── Home.razor                # Main Bin Pack Problem page
│   ├── Home.razor.css            # Page-specific styles
│   └── Error.razor               # Error page
├── Services/
│   ├── ApiService/               # API integration
│   ├── DragService/              # Drag and drop logic
│   ├── PositionService/          # Collision detection
│   ├── RotationService/          # Vessel rotation logic
│   ├── VesselLayoutService/      # Initial vessel positioning
│   └── VesselTrackingService/    # Progress tracking
├── Shared/
│   ├── AnchorageBox.razor        # Anchorage container component
│   ├── PageHeader.razor          # Header with "Try again" button
│   ├── SuccessMessage.razor      # Win condition overlay
│   ├── VesselItem.razor          # Individual vessel component
│   └── VesselTracker.razor       # Progress tracker component
├── Tests/
│   └── Services/                 # Unit tests for all services
├── wwwroot/
│   ├── app.css                   # Global styles
│   └── lib/                      # Bootstrap libraries
├── appsettings.json              # Configuration
├── appsettings.Development.json  # Development configuration
└── Program.cs                    # Application startup

```

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later

### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/instech-blazor-coding-task.git
cd instech-blazor-coding-task
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

4. Open your browser and navigate to:
```
https://localhost:5001
```

## 🧪 Running Tests

Run all unit tests:
```bash
cd Tests
dotnet test
```

Run with detailed output:
```bash
dotnet test --verbosity normal
```

## ⚙️ Configuration

The API base URL can be configured in `appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://esa.instech.no/"
  }
}
```

You can override this in different environments or via environment variables:
```bash
ApiSettings__BaseUrl=https://your-api-url.com/
```

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

Stefan Dzaleski
