; Lightning SDK Setup ver 0.2.000.00003
; May 4, 2021

; v0.2.000.00003  May 16, 2021  Added INSTALLPACKAGE_INSTALL_SDK ifdef for Polaris
; v0.2.000.00002  May 4, 2021   First functional version
#define MyAppName "Lightning SDK"
#define MyAppVersion "0.2.XXX.XXXXX" ; set to version when a version is there
#define MyAppPublisher "starfrost/Lightning Dev Team"
#define MyAppExeName "Lightning.exe"
#define MyAppAssocName "Lightning Game Project"
#define MyAppAssocExt ".lgx"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

#define INSTALLPACKAGE_INSTALL_SDK

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{CDCF6F62-5171-4EC7-86E3-1CC9B13FD12B}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\Lightning SDK 
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=Lightning_Setup.exe
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\Lightning_builds\latest\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
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
Source: "D:\Lightning_builds\latest\Lightning.Core.NativeInterop.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.Core.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.Core.SDL2-CS.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.runtimeconfig.dev.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Lightning.Utilities.dll"; DestDir: "{app}"; Flags: ignoreversion
#ifdef INSTALLPACKAGE_INSTALL_SDK
Source: "D:\Lightning_builds\latest\Polaris.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Polaris.Core.dll"; DestDir: "{app}"; Flags: ignoreversion 
#endif
Source: "D:\Lightning_builds\latest\SDL2.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\SDL2_image-v2.0.5-x64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\SDL2_mixer-v2.0.4-x64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\SDL2_ttf-v2.0.15-x64.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\WritingTest.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\zlib1.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Lightning_builds\latest\Content\*"; DestDir: "{app}\Content"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

