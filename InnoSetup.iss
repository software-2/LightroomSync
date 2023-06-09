; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "LightroomSync"
#define MyAppVersion "1.0"
#define MyAppPublisher "Anthony Bryan"
#define MyAppURL "https://github.com/software-2/LightroomSync"
#define MyAppExeName "LightroomSync.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{E517626F-1634-44A5-A111-1BB07C55E499}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=C:\Git\LightroomSync\LICENSE
; Remove the following line to run in administrative install mode (install for all users.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=LightroomSync Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Git\LightroomSync\LightroomSync\bin\Release\net6.0-windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Git\LightroomSync\LightroomSync\bin\Release\net6.0-windows\LightroomSync.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Git\LightroomSync\LightroomSync\bin\Release\net6.0-windows\LightroomSync.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Git\LightroomSync\LightroomSync\bin\Release\net6.0-windows\LightroomSync.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Git\LightroomSync\LightroomSync\bin\Release\net6.0-windows\LightroomSync.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Git\LightroomSync\LightroomSync\bin\Release\net6.0-windows\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

