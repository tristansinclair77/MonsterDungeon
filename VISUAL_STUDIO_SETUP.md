# Visual Studio Setup Instructions

## Complete These Steps in Visual Studio to Finish Setup

### Step 1: Add All New Files to Project

1. In **Solution Explorer**, right-click on **MonsterDungeon** project
2. Select **Add** ? **Existing Item**
3. Navigate to each folder and add all `.cs` files:

**Or use this faster method:**
1. In Solution Explorer, click "Show All Files" button (top of Solution Explorer)
2. You'll see all folders with files in gray
3. Right-click each folder (Domain, Application, Presentation) and select "Include In Project"
4. Right-click each `.cs` file and select "Include In Project"

### Step 2: Install NuGet Packages

1. Right-click on the **MonsterDungeon** project
2. Select **Manage NuGet Packages...**
3. Click the **Browse** tab
4. Search for and install each package:

   #### Required Packages:
   - **CommunityToolkit.Mvvm** (v8.2.2 or later)
     - Used for MVVM implementation with source generators
   
   - **Microsoft.Extensions.DependencyInjection** (v8.0.0 or later)
- Core DI container
   
   - **Microsoft.Extensions.Hosting** (v8.0.0 or later)
     - Application hosting and DI configuration
   
   - **System.Text.Json** (v8.0.0 or later)
     - JSON serialization for save/load

   #### Optional but Recommended:
   - **MahApps.Metro** (v2.4.10 or later)
     - Modern WPF UI framework
   
   - **FontAwesome.Sharp** (v6.3.0 or later)
     - Icon library for UI

5. Accept all dependencies and license agreements

### Step 3: Update App.xaml.cs

After packages are installed, edit **App.xaml.cs**:

1. **Uncomment** these using statements at the top:
   ```csharp
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.Extensions.Hosting;
   ```

2. **Uncomment** the `AppHost` property:
   ```csharp
   public static IHost AppHost { get; private set; }
   ```

3. **Uncomment** the DI configuration in the constructor:
   ```csharp
   AppHost = Host.CreateDefaultBuilder()
     .ConfigureServices((context, services) =>
     {
           // ... all service registrations
       })
       .Build();
   ```

4. **Replace** the temporary OnStartup method with the commented DI version:
   ```csharp
   protected override async void OnStartup(StartupEventArgs e)
   {
       await AppHost.StartAsync();
       var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
       mainWindow.Show();
     base.OnStartup(e);
   }
   ```

5. **Uncomment** the OnExit method:
   ```csharp
   protected override void OnExit(ExitEventArgs e)
   {
       AppHost?.Dispose();
       base.OnExit(e);
   }
   ```

### Step 4: Update MainViewModel.cs

Edit **Application/ViewModels/MainViewModel.cs**:

1. **Replace the entire file** with the commented version at the bottom:
   ```csharp
   using CommunityToolkit.Mvvm.ComponentModel;

   namespace MonsterDungeon.Application.ViewModels
   {
       public partial class MainViewModel : ObservableObject
       {
 [ObservableProperty]
   private string title = "Monster Dungeon";

           [ObservableProperty]
         private string version = "v0.1.0-alpha";
  }
   }
   ```

### Step 5: Update MainWindow.xaml.cs

Edit **Presentation/Views/MainWindow.xaml.cs**:

1. **Replace** the constructor to use DI:
```csharp
   public MainWindow(MainViewModel vm)
   {
       InitializeComponent();
       DataContext = vm;
   }
   ```

2. **Remove** the temporary `new MainViewModel()` line

### Step 6: Build the Project

1. Press **Ctrl+Shift+B** or select **Build** ? **Build Solution**
2. Check the **Output** window for any errors
3. Common issues and fixes:
   - **"Type or namespace not found"**: Make sure all files are included in project
   - **"Package not found"**: Restore NuGet packages (right-click solution ? Restore NuGet Packages)
   - **"Ambiguous reference"**: Check for duplicate using statements

### Step 7: Run the Application

1. Press **F5** or click **Start Debugging**
2. You should see the MainWindow with:
   - Title: "Monster Dungeon"
   - Version: "v0.1.0-alpha" in the header
   - Dark themed window (background #1a1a2e)
 - Red/orange accent colors

### Step 8: Verify Everything Works

? Application launches without errors
? Window displays title and version from ViewModel
? No console errors or exceptions
? Window is responsive and can be moved/resized

---

## Troubleshooting

### Build Errors

**Error: CS0234 "Namespace does not exist"**
- Solution: Make sure all `.cs` files are included in the project
- Check: Solution Explorer should show all files, not grayed out

**Error: CS0246 "Type or namespace name could not be found"**
- Solution: Right-click project ? Manage NuGet Packages ? Click "Installed" ? Verify packages are installed
- Try: Right-click solution ? Restore NuGet Packages

**Error: CS0103 "The name '...' does not exist in the current context"**
- Solution: Check that all `using` statements are uncommented in App.xaml.cs

### Runtime Errors

**Exception: "No service for type MainWindow"**
- Solution: Make sure `services.AddSingleton<MainWindow>()` is uncommented in App.xaml.cs

**Exception: "Object reference not set to an instance"**
- Solution: Make sure all services are registered in the DI container

**Application doesn't start**
- Check: App.xaml StartupUri is set to "Presentation/Views/MainWindow.xaml"
- Check: MainWindow.xaml x:Class is "MonsterDungeon.Presentation.Views.MainWindow"

### Package Installation Issues

**"Package is not compatible with net472"**
- Solution: Use an older version of the package
- For CommunityToolkit.Mvvm, try v8.2.2
- For Microsoft.Extensions.*, try v7.0.0 if v8.0.0 doesn't work

**"Unable to resolve dependency"**
- Solution: Update all packages to compatible versions
- Close Visual Studio and delete `bin/` and `obj/` folders, then reopen and rebuild

---

## After Successful Launch

Once everything is working:

1. **Commit to Git** (if using version control):
   ```bash
   git add .
   git commit -m "Initial Monster Dungeon project setup complete"
   ```

2. **Review Project Structure** in Solution Explorer:
 - All folders should be visible and organized
   - All `.cs` files should have proper icons (not generic file icons)

3. **Start Development**:
   - Begin implementing game UI in Presentation/Views/
   - Add more ViewModels as needed
   - Implement Commands for user actions
   - Add asset files to Presentation/Assets/

4. **Next Development Tasks** (in recommended order):
   1. Create GameView.xaml for main game screen
   2. Add grid visualization (8×10 tile display)
   3. Implement player movement input
   4. Add enemy rendering
 5. Create combat UI
   6. Implement inventory screen

---

## Quick Reference

**Project Structure:**
- `Domain/` - Game logic, no dependencies
- `Application/` - ViewModels, Services, orchestration
- `Infrastructure/` - File I/O, persistence, assets
- `Presentation/` - WPF UI, Views, Controls

**Key Classes:**
- `GameFlowService` - Main game loop
- `GridService` - Grid and movement
- `EnemyFactory` - Enemy creation
- `ThemeManager` - Visual themes
- `MainViewModel` - Main window ViewModel

**Namespaces:**
- `MonsterDungeon.Domain.*`
- `MonsterDungeon.Application.*`
- `MonsterDungeon.Infrastructure.*`
- `MonsterDungeon.Presentation.*`

---

**?? Happy Coding! Your Monster Dungeon adventure begins now!**
