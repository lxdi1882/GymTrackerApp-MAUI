# Setup

## Prerequisites

- **.NET 10 SDK** — https://dotnet.microsoft.com/download/dotnet/10.0
- **MAUI workload**
  ```powershell
  dotnet workload install maui
  ```
- **Java SDK (JDK)** — Microsoft OpenJDK build:
  https://learn.microsoft.com/en-us/dotnet/android/getting-started/installation/java-jdk
- **Android SDK API level 36** — install via:
  ```powershell
  dotnet build -t:InstallAndroidDependencies -f net10.0-android "-p:AndroidSdkDirectory=<path-to-Android-Sdk>" -p:AcceptAndroidSdkLicenses=true
  ```

## Clone and build

```powershell
git clone <repo-url>
cd GymTrackerApp-MAUI/MauiApp2
dotnet workload restore
dotnet build
```

## Troubleshooting

**`the term 'dotnet' is not recognized`**
.NET SDK not installed, or terminal opened before install finished. Install, then open a new terminal.

**`XA5300: Java SDK directory could not be found`**
Install the JDK above. If still not detected, add to `MauiApp2.csproj`:
```xml
<JavaSdkDirectory>C:\Program Files\Microsoft\jdk-17.x.x.x-hotspot</JavaSdkDirectory>
```

**`XA5207: Could not find android.jar for API level 36`**
Run the `InstallAndroidDependencies` command above.

**`CreateProcessW failed: GetLastError() returned 4551`**
Windows Smart App Control blocked a tool. Check **Windows Security → App & browser control → Smart App Control**. Disabling it can only be undone via a full Windows reinstall.
