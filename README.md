# EnhancePointerPrecisionSwitch

Simple lightweight utility to enable/disable/toggle Mouse Enhance Pointer Precision setting

Requires .NET Framework 4.0 or higher

## Build
Download EnhancePointerPrecisionSwitch.cs and compile.bat<br>
Run compile.bat to build source with your local CS compiler

## Command line arguments:
**on** - enables Enhance Pointer Precision setting<br>
**off** - disables setting<br>
**toggle** - inverse setting<br>
**permanent** - keeps setting after reboot (should be used along with one of other settings)

## Example:

If you'd like to turn "Enhance Pointer Precision" off while playing and set it back after,
create bat file with text like:
```
C:\Programs\EnhancePointerPrecisionSwitch\EnhancePointerPrecisionSwitch.exe off
START /WAIT C:\games\Quake3\quake3.exe +connect server:port
C:\Programs\EnhancePointerPrecisionSwitch\EnhancePointerPrecisionSwitch.exe on
```