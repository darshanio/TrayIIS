# Tray IIS

Tray IIS is a lightweight Windows system tray application that allows you to quickly view and recycle IIS Application Pools directly from your desktop. Built with .NET 9 and Windows Forms, it provides a convenient way for developers and administrators to manage IIS without opening IIS Manager.

## Features

- **View all IIS Application Pools**  
  See a list of all application pools configured on your local IIS instance.

- **Recycle Application Pools**  
  Recycle any application pool with a single click from the tray menu.

- **Show Only Running App Pools**  
  Optionally filter the menu to display only running application pools.

- **Quick Access**  
  Access all features directly from the Windows system tray.

## Requirements

- Windows with IIS installed
- .NET 9.0 Runtime or SDK
- Administrator privileges (required to manage IIS)

## Installation

1. **Build from Source**
   - Clone the repository:  
     `git clone https://github.com/darshanio/TrayIIS.git`
   - Open the solution in Visual Studio 2022 or later.
   - Build the project (`TrayIIS`).

2. **Run the Application**
   - Run the compiled executable (`TrayIIS.exe`).
   - The application will appear as an icon in your system tray.

## Usage

1. **Accessing the Tray Menu**
   - Right-click the Tray IIS icon in the system tray.
   - The context menu will display:
     - A list of IIS Application Pools (with a recycle icon).
     - Option to show only running app pools.

2. **Recycling an Application Pool**
   - Click on any application pool name in the menu to recycle it.
   - The pool will be recycled immediately.

3. **Show Only Running App Pools**
   - Toggle the "Show only running App Pools" checkbox to filter the list.
   - The setting is saved and persists between sessions.

## Notes

- The application uses the `Microsoft.Web.Administration` library to interact with IIS.
- Administrator rights are required to recycle application pools.

## License

See [LICENSE.txt](LICENSE.txt) for license information.

Made with <3 by [Darshan Panchal](https://github.com/darshanio) from [Enqbator, LLC](https://www.enqbator.com).