# EmuTarkov-Modules
Client-side runtime patches to alter the client's behaviour.

## Modules
- EmuTarkov.Common: utilities used across modules.
- EmuTarkov.Core: required patches for the game to run.

## Setup
All dependencies are provided, no additional setup required.

## Build
1. Visual Studio -> menubar -> rebuild.
2. Copy-paste `EmuTarkov.Common/bin/Debug/EmuTarkov.Common.dll` into `Build/`.
3. Copy-paste `EmuTarkov.Core/bin/Debug/EmuTarkov.Core.dll` into `Build/`.
4. Copy-paste `Shared/Nlog.config.nlog` into `Build/`.
5. Copy-paste all files inside `Shared/Dependencies/` into `Build/`.
6. Rename `Build/EmuTarkov.Common.dll` into `Build/NLog.EmuTarkov.Common.dll`.
7. Rename `Build/EmuTarkov.Core.dll` into `Build/NLog.EmuTarkov.Core.dll`.

## Run
1. Follow the build instructions
2. Copy-paste all files inside `Build/` into `EscapeFromTarkov_Data/Managed/`, overwrite when prompted.

## Credits
- InNoHurryToCode: initial code + refactoring into modules
- stx09: working patches
- ???: original custom asset bundle loading