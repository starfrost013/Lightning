; Lightning SDK Setup ver 0.3.006.00013
; August 15, 2021

; v0.3.006.00013  Aug 15, 2021  Split SDK/runtime based on INSTALLPACKAGE_INSTALL_SDK, define output filename based on appname, setupversion, and if SDK/Runtime
; v0.3.005.00012  Aug 15, 2021  Add Lua files, remove PDBs, add documentation, add platform defines, increment version to 0.3 
; v0.2.004.00011  May 24, 2021  Added Polaris files so that it actually works properly
; v0.2.003.00010  May 22, 2021  Added Polaris.dll...*facepalm*
; v0.2.002.00009  May 21, 2021  Modified Polaris shortcut name 
; v0.2.002.00008  May 21, 2021  Moved Start Menu icons into a folder
; v0.2.002.00007  May 21, 2021  Added Polaris shortcut
; v0.2.001.00006  May 18, 2021  Added setup version def
; v0.2.001.00005  May 18, 2021  Added ifdef checks so that fgx files launch Polaris
; v0.2.000.00004  May 18, 2021  Added Polaris.UI
; v0.2.000.00003  May 16, 2021  Added INSTALLPACKAGE_INSTALL_SDK ifdef for Polaris
; v0.2.000.00002  May 4, 2021   First functional version


; Comment out this line to build a runtime installer (TODO: User-selectable omponents)
#define INSTALLPACKAGE_INSTALL_SDK

; Basic definitions
#ifdef INSTALLPACKAGE_INSTALL_SDK
#define MyAppName "Lightning SDK"
#define PolarisAppName "Lightning SDK - Polaris Development Environment"
#else
#define MyAppName "Lightning Runtime"
#define PolarisAppName "THIS SHOULD NOT HAVE BEEN INSTALLED - THERE IS A BUG WITH THE INSTALLER"
#endif
#define MyAppVersion "0.3.XXX.XXXXX" ; set to version when a version is there
#define MyAppPublisher "starfrost/Lightning Dev Team"
#define MyAppExeName "Lightning.exe"
#define PolarisAppExeName "Polaris.exe"
#define MyAppAssocName "Lightning Game Project"
#define MyAppAssocExt ".lgx"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt


; Platform definitions
#define LIGHTNING_WIN64
;#define LIGHTNING_WIN32 - almost certainly cut, but there's a chance of it being required in the future 
;#define LIGHTNING_WINARM64 - investigate XTAJIT64 (Windows 10 Cobalt 21277+ and 11 ONLY)
;#define LIGHTNING_MACOS64
;#define LIGHTNING_MACOSARM64
;#define LIGHTNING_LINUX32
;#define LIGHTNING_LINUX64

; Setup version define
#define SETUP_VERSION "0.3.006.00013"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{CDCF6F62-5171-4EC7-86E3-1CC9B13FD12B}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
#ifdef INSTALLPACKAGE_INSTALL_SDK
DefaultDirName={autopf}\Lightning SDK
#else
DefaultDirName={autopf}\Lightning Runtime
#endif 
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
#ifdef INSTALLPACKAGE_INSTALL_SDK
OutputBaseFilename=Lightning_Setup_{#SETUP_VERSION}_{#MyAppVersion}_SDK
#else
OutputBaseFilename=Lightning_Setup_{#SETUP_VERSION}_{#MyAppVersion}_Runtime
#endif
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\Lightning_builds\latest\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\KeraLua.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libFLAC-8.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libfreetype-6.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libjpeg-9.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libmodplug-1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libmpg123-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libogg-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libopus-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libopusfile-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libpng16-16.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libtiff-5.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libvorbis-0.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libvorbisfile-3.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\libwebp-7.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.Core.NativeInterop.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.Core.SDL2-CS.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.runtimeconfig.dev.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.Utilities.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\NLua.dll"; DestDir: "{app}"; Flags: ignoreversion
#ifdef INSTALLPACKAGE_INSTALL_SDK
Source: "D:\Lightning_builds\latest\{#PolarisAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Polaris.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Polaris.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Polaris.runtimeconfig.dev.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Polaris.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Polaris.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Polaris.UI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Documentation\*"; DestDir: "{app}\Documentation"; Flags: ignoreversion recursesubdirs createallsubdirs  
#endif
Source: "D:\Lightning_builds\latest\SDL2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\SDL2_image-v2.0.5-x64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\SDL2_mixer-v2.0.4-x64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\SDL2_ttf-v2.0.15-x64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\zlib1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Content\*"; DestDir: "{app}\Content"; Flags: ignoreversion recursesubdirs createallsubdirs
#ifdef LIGHTNING_WIN64
Source: "D:\Lightning_builds\latest\runtimes\win-x64\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs
#elif LIGHTNING_MACOS64
Source: "D:\Lightning_builds\latest\runtimes\osx\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs
#elif LIGHTNING_LINUX64
Source: "D:\Lightning_builds\latest\runtimes\linux-x64\*"; DestDir: "{app}\runtimes"; Flags: ignoreversion recursesubdirs createallsubdirs
#endif

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
#ifdef INSTALLPACKAGE_INSTALL_SDK
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#PolarisAppExeName}"" ""%1"""
#else 
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
#endif

Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
#ifdef INSTALLPACKAGE_INSTALL_SDK
Name: "{autoprograms}\{#MyAppName}\{#PolarisAppName}"; Filename: "{app}\{#PolarisAppExeName}"
#endif
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
#ifdef INSTALLPACKAGE_INSTALL_SDK
Filename: "{app}\{#PolarisAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
#else
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
#endif
