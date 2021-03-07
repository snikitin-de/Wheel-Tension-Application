;------------------------------------------------------------------------------
;
;       Wheel Tension Applicatiion (c) snikitin-de
;
;------------------------------------------------------------------------------

;------------------------------------------------------------------------------
;  Constants
;------------------------------------------------------------------------------

; Application name
#define   ApplicationName                "Wheel Tension Application"
; Repository name
#define   RepositoryName                 "Wheel-Tension-Application"
; Developer
#define   ApplicationPublisher           "snikitin-de"
; Developer site 
#define   ApplicationURL                 "https://github.com/snikitin-de"
; Application Icon
#define   ApplicationIcon                "bicycle.ico"
; Executable module name
#define   ApplicationExeName             "Wheel Tension Application.exe"
; Application version
#define   ApplicationVersion             GetVersionNumbersString("..\" + ApplicationName + "\bin\" + ApplicationConfiguration + "\" + ApplicationExeName)
; .NET Framework version
#define   DotNetFrameworkVersion         "v4.8"
; .NET Framework download link
#define   DotNetFrameworkDownloadLink    "https://go.microsoft.com/fwlink/?linkid=2088631"

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
OutputDir=..\{#ApplicationName}\installer\{#ApplicationConfiguration}

; Output file name
OutputBaseFileName={#ApplicationName} {#ApplicationVersion} Setup

; Application icon file
SetupIconFile=..\{#ApplicationName}\{#ApplicationIcon}

; Compression options 
Compression=lzma
SolidCompression=yes

; Path to Inno Download Plugin 
#include "plugins\idp\idp.iss"

;------------------------------------------------------------------------------
;   Installing languages for the installation process 
;------------------------------------------------------------------------------
[Languages]

Name: "english"; MessagesFile: "compiler:Default.isl";
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl";

; Path to Inno Download Plugin localization file
#include "plugins\idp\unicode\idplang\Russian.iss"

; Paths to custom messages localization files
#include "custom messages\Default.iss"
#include "custom messages\Russian.iss"

;------------------------------------------------------------------------------
;   Tasks
;------------------------------------------------------------------------------
[Tasks]

; Create Desktop icon
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
; Install .NET Framework
Name: "dotnetframeworkinstall"; Description: "{cm:DotNetFrameworkTaskDescription}"; GroupDescription: ".NET Framework:"; Flags: unchecked

;------------------------------------------------------------------------------
;   Files
;------------------------------------------------------------------------------
[Files]

; Executable file
Source: "..\{#ApplicationName}\bin\{#ApplicationConfiguration}\{#ApplicationExeName}"; DestDir: "{app}"; Flags: ignoreversion

; Resources
Source: "..\{#ApplicationName}\bin\{#ApplicationConfiguration}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; AfterInstall: InitializeWizard; Check: IsRequiredDotNetDetected

;------------------------------------------------------------------------------
;   Icons
;------------------------------------------------------------------------------ 
[Icons]

Name: "{group}\{#ApplicationName}"; Filename: "{app}\{#ApplicationExeName}"
Name: "{commondesktop}\{#ApplicationName}"; Filename: "{app}\{#ApplicationExeName}"; Tasks: desktopicon

;------------------------------------------------------------------------------
;   Code section included from a separate file 
;------------------------------------------------------------------------------
[Code]
var
  requiresRestart: boolean; // Need restart
  ignoreInstallDotNetFramework: boolean; // Ignore install .NET Framework
  dotNetFrameworkStatusLabel: TLabel; // Status label of .NET Framework installation

//-----------------------------------------------------------------------------
//  Need restart
//-----------------------------------------------------------------------------
function NeedRestart(): Boolean;
begin
  Result := requiresRestart;
end;

//-----------------------------------------------------------------------------
//  Checking for the necessary framework 
//-----------------------------------------------------------------------------
function IsDotNetDetected(version: string; release: cardinal): boolean;
var 
  reg_key: string; // Viewed subkey of the system registry
  success: boolean; // Flag for the presence of the requested .NET version
  releaseFromRegistry: cardinal; // Release number for version
  sub_key: string;
begin
  success := false;
  reg_key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\';

  if Pos('{#DotNetFrameworkVersion}', version) = 1 then
    begin
      sub_key := 'v4\Full';
      reg_key := reg_key + sub_key;
      success := RegQueryDWordValue(HKLM, reg_key, 'Release', releaseFromRegistry);
      success := success and (releaseFromRegistry >= release);
    end;
      
  result := success;
end;

//-----------------------------------------------------------------------------
//  A wrapper function for detecting the specific version of .NET Framework
//-----------------------------------------------------------------------------
function IsRequiredDotNetDetected(): boolean;
begin
  result := IsDotNetDetected('{#DotNetFrameworkVersion}', 0);
end;

//-----------------------------------------------------------------------------
//  Install .NET Framework
//-----------------------------------------------------------------------------
function InstallDotNet(): String;
var
  resultCode: Integer;
begin
  if not Exec(ExpandConstant('{tmp}\.NET Framework {#DotNetFrameworkVersion}.exe'), '/passive /norestart /showrmui /showfinalerror', '', SW_SHOW, ewWaitUntilTerminated, resultCode) then
    begin
      Result := FmtMessage(CustomMessage('DotNetFrameworkFailedToLaunch'), [SysErrorMessage(resultCode)]);
    end
  else
    begin
      case resultCode of
        0: begin
          dotNetFrameworkStatusLabel.Caption := CustomMessage('DotNetFrameworkInstallationSuccessful');
        end;
        1602 : begin
          MsgBox(CustomMessage('DotNetFrameworkFailed1602'), mbInformation, MB_OK);
        end;
        1603: begin
          Result := CustomMessage('DotNetFrameworkFailed1603');
        end;
        1641: begin
          requiresRestart := True;
        end;
        3010: begin
          requiresRestart := True;
        end;
        5100: begin
          Result := CustomMessage('DotNetFrameworkFailed5100');
        end;
        else begin
          MsgBox(FmtMessage(CustomMessage('DotNetFrameworkFailedOther'), [IntToStr(resultCode)]), mbError, MB_OK);
        end;
      end;
    end;
end;

//-----------------------------------------------------------------------------
//  .NET Framework custom page
//-----------------------------------------------------------------------------
procedure CreateDotNetFrameworkWizardPage;
var
  Page: TWizardPage;
begin
  // ID download page is 100
  Page := CreateCustomPage(100, CustomMessage('DotNetFrameworkPageTitle'), CustomMessage('DotNetFrameworkPageDescription'));
                           
  dotNetFrameworkStatusLabel := TLabel.Create(Page);
  dotNetFrameworkStatusLabel.Caption := CustomMessage('InstallingDotNetFramework');
  dotNetFrameworkStatusLabel.AutoSize := True;
  dotNetFrameworkStatusLabel.Parent := Page.Surface;
end;

//-----------------------------------------------------------------------------
//  InitializeWizard
//-----------------------------------------------------------------------------
procedure InitializeWizard;
begin
  idpDownloadAfter(wpInstalling);

  if not IsRequiredDotNetDetected() then
    CreateDotNetFrameworkWizardPage();
end;

//-----------------------------------------------------------------------------
//  NextButtonClick
//-----------------------------------------------------------------------------
function NextButtonClick(CurPageID: Integer) : boolean;
begin
  result := true;

  if CurPageID = wpSelectTasks then
    begin
      if not IsDotNetDetected('{#DotNetFrameworkVersion}', 0) then
        begin
          if not WizardIsTaskSelected('dotnetframeworkinstall') then
            begin
              if MsgBox(CustomMessage('DotNetFrameworkInstallationIgnore'), mbInformation, MB_YESNO) = IDYES then
                begin
                  result := true;
                  ignoreInstallDotNetFramework := true;
                end
              else
                result := false;
            end
          else
            begin
              idpAddFile('{#DotNetFrameworkDownloadLink}', ExpandConstant('{tmp}\.NET Framework {#DotNetFrameworkVersion}.exe'));
              ignoreInstallDotNetFramework := false;
            end;
        end;
    end;
end;

//-----------------------------------------------------------------------------
//  CurrentPageChanged
//-----------------------------------------------------------------------------
procedure CurPageChanged(CurPageID: Integer);
begin
  // ID install .NET Framework page is 101
  if CurPageID = 101 then 
    InstallDotNet();
  
  if CurPageID = wpSelectTasks then
    if IsDotNetDetected('{#DotNetFrameworkVersion}', 0) then
      begin
        WizardSelectTasks('!dotnetframeworkinstall');
        WizardForm.TasksList.ItemEnabled[3] := False;
      end
end;