# Dynu IP Updater

A lightweight Windows desktop application for automatically updating your [Dynu.com](https://www.dynu.com) Dynamic DNS records.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-blue)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)
![License](https://img.shields.io/badge/License-MIT-green)
![Built with Claude](https://img.shields.io/badge/Built%20with-Claude%20Opus%204.5-blueviolet)

## Features

- **Automatic IP Updates** - Detects your public IP and updates Dynu DNS records automatically
- **Multiple Update Modes** - Update all hostnames, a specific group, or individual hostnames
- **Encrypted Credentials** - Passwords stored securely using Windows DPAPI encryption
- **Scheduled Task Integration** - Set up Windows Scheduled Task for background updates
- **IPv4 & IPv6 Support** - Update both A and AAAA records
- **Reliable IP Detection** - Uses multiple fallback services for IP detection
- **Logging** - Comprehensive logging with automatic rotation

## Screenshots

*Coming soon*

## Requirements

- Windows 7 or later
- .NET Framework 4.8
- Dynu.com account with Dynamic DNS service

## Installation

### Option 1: Download Release

1. Download the latest release from the [Releases](../../releases) page
2. Extract to your preferred location
3. Run `DynuIpUpdater.exe`

### Option 2: Build from Source

1. Clone the repository
   ```
   git clone https://github.com/user/DynuIpUpdater.git
   ```
2. Open `DynuIpUpdater.sln` in Visual Studio 2019 or later
3. Build the solution (F6)
4. Run from `bin\Debug\` or `bin\Release\`

## Usage

### GUI Mode

Simply run `DynuIpUpdater.exe` to launch the graphical interface.

1. Enter your Dynu username and update password
2. Select your update target (all hostnames, group, or specific hostnames)
3. Configure update interval
4. Click **Save Settings**
5. Click **Install Scheduled Task** for automatic background updates

### Command Line

```
DynuIpUpdater.exe              # Launch GUI
DynuIpUpdater.exe --silent     # Silent update (for scheduled task)
DynuIpUpdater.exe -s           # Same as --silent
DynuIpUpdater.exe --help       # Show help
```

## Configuration

### Update Targets

| Mode | Description |
|------|-------------|
| All Hostnames | Updates all DNS records in your Dynu account |
| Group | Updates all hostnames in a specific group (requires group password) |
| Specific Hostnames | Updates only the specified hostname(s), comma-separated |

### Security

- Credentials are encrypted using Windows Data Protection API (DPAPI)
- Machine-specific encryption key ensures credentials only work on the original computer
- Option to send SHA256 hash of password instead of plain text

### Logging

Logs are stored in `log.txt` in the application directory with automatic rotation at 5MB (keeps 5 backups).

## Dynu API

This application uses the [Dynu IP Update Protocol](https://www.dynu.com/DynamicDNS/IP-Update-Protocol). You'll need:

- Your Dynu account username
- Your IP Update Password (set in Dynu Control Panel → DDNS Services → IP Update Password)

## Building

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.8 SDK

### Dependencies

All dependencies are from the .NET Framework GAC - no NuGet packages required:

- System.Web.Extensions (for JSON serialization)
- System.Security (for DPAPI encryption)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [Dynu.com](https://www.dynu.com) for providing free Dynamic DNS services
- IP detection services: [ipify](https://www.ipify.org), [icanhazip](https://icanhazip.com), [my-ip.io](https://www.my-ip.io)
- Developed in collaboration with Claude Opus 4.5

## Disclaimer

This project is not affiliated with, endorsed by, or sponsored by Dynu Systems Inc. Dynu is a trademark of Dynu Systems Inc.
