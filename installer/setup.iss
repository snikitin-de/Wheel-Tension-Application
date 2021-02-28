;------------------------------------------------------------------------------
;
;       Wheel Tension Applicatiion (c) snikitin-de
;
;------------------------------------------------------------------------------

;------------------------------------------------------------------------------
;  Constants
;------------------------------------------------------------------------------

; Application name
#define   ApplicationName            "Wheel Tension Application"
; Repository name
#define   RepositoryName             "Wheel-Tension-Application"
; Developer
#define   ApplicationPublisher       "snikitin-de"
; Developer site 
#define   ApplicationURL             "https://github.com/snikitin-de"
; Application Icon
#define   ApplicationIcon            "bicycle.ico"
; Executable module name
#define   ApplicationExeName         "Wheel Tension Application.exe"
; Application version
#define   ApplicationVersion         GetVersionNumbersString("..\" + ApplicationName + "\bin\" + ApplicationConfiguration + "\" + ApplicationExeName)

;------------------------------------------------------------------------------
;   Installation options 
;------------------------------------------------------------------------------
[Setup]

; Application info
; GUID (Tools -> Generate GUID)
AppId={{C9D33291-796A-4133-A21C-3B50D6B439AD}
AppName={#ApplicationName}
AppVersion={#ApplicationVersion}
AppPublisher={#ApplicationPublisher}
AppPublisherURL={#ApplicationURL}
AppSupportURL={#ApplicationURL}/{#RepositoryName}/issues
AppUpdatesURL={#ApplicationURL}/{#RepositoryName}/releases

; Control panel app icon
UninstallDisplayIcon={app}\{#ApplicationExeName}

; Default installation path
DefaultDirName={commonpf}\{#ApplicationName}

; Start menu group name
DefaultGroupName={#ApplicationName}

; Output directory name
OutputDir=..\{#ApplicationName}\bin\{#ApplicationConfiguration}\

; Output file name
OutputBaseFileName={#ApplicationName} {#ApplicationVersion} Setup

; Application icon file
SetupIconFile=..\{#ApplicationName}\{#ApplicationIcon}

; Compression options 
Compression=lzma
SolidCompression=yes

;------------------------------------------------------------------------------
;   Installing languages for the installation process 
;------------------------------------------------------------------------------
[Languages]

Name: "english"; MessagesFile: "compiler:Default.isl";
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl";

;------------------------------------------------------------------------------
;   Tasks
;------------------------------------------------------------------------------
[Tasks]

; Create Desktop icon
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

;------------------------------------------------------------------------------
;   Files
;------------------------------------------------------------------------------
[Files]

; Executable file
Source: "..\{#ApplicationName}\bin\{#ApplicationConfiguration}\{#ApplicationExeName}"; DestDir: "{app}"; Flags: ignoreversion

; Resources
Source: "..\{#ApplicationName}\bin\{#ApplicationConfiguration}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

;------------------------------------------------------------------------------
;   Icons
;------------------------------------------------------------------------------ 
[Icons]

Name: "{group}\{#ApplicationName}"; Filename: "{app}\{#ApplicationExeName}"
Name: "{commondesktop}\{#ApplicationName}"; Filename: "{app}\{#ApplicationExeName}"; Tasks: desktopicon
