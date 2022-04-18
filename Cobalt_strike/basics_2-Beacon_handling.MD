once user get the Coblat strike beacon session, following things can be done:

#### 1. Intracting with the beacon

Left click and select 'Intract' to open the beacon intraction console:

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Cobalt_strike/images/beacon_intract.png width="900" height="400"/>

In the console, user can execute the inbuilt beacon commands. To list all the avialble commands, type 'help' and press enter.
Sample output of help command:

<details>
  <summary><b>beacon> help</b></summary>

  ```
     Beacon Commands
     ===============

    Command                   Description
    -------                   -----------
    browserpivot              Setup a browser pivot session
    bypassuac                 Spawn a session in a high integrity process
    cancel                    Cancel a download that's in-progress
    cd                        Change directory
    checkin                   Call home and post data
    clear                     Clear beacon queue
    covertvpn                 Deploy Covert VPN client
    cp                        Copy a file
    dcsync                    Extract a password hash from a DC
    desktop                   View and interact with target's desktop
    dllinject                 Inject a Reflective DLL into a process
    download                  Download a file
    downloads                 Lists file downloads in progress
    drives                    List drives on target
    elevate                   Try to elevate privileges
    execute                   Execute a program on target
    exit                      Terminate the beacon session
    getsystem                 Attempt to get SYSTEM
    getuid                    Get User ID
    hashdump                  Dump password hashes
    help                      Help menu
    inject                    Spawn a session in a specific process
    jobkill                   Kill a long-running post-exploitation task
    jobs                      List long-running post-exploitation tasks
    kerberos_ccache_use       Apply kerberos ticket from cache to this session
    kerberos_ticket_purge     Purge kerberos tickets from this session
    kerberos_ticket_use       Apply kerberos ticket to this session
    keylogger                 Inject a keystroke logger into a process
    kill                      Kill a process
    link                      Connect to a Beacon peer over SMB
    logonpasswords            Dump credentials and hashes with mimikatz
    ls                        List files
    make_token                Create a token to pass credentials
    mimikatz                  Runs a mimikatz command
    mkdir                     Make a directory
    mode dns                  Use DNS A as data channel (DNS beacon only)
    mode dns-txt              Use DNS TXT as data channel (DNS beacon only)
    mode dns6                 Use DNS AAAA as data channel (DNS beacon only)
    mode http                 Use HTTP as data channel
    mode smb                  Use SMB peer-to-peer communication
    mv                        Move a file
    net                       Network and host enumeration tool
    note                      Assign a note to this Beacon       
    portscan                  Scan a network for open services
    powerpick                 Execute a command via Unmanaged PowerShell
    powershell                Execute a command via powershell.exe
    powershell-import         Import a powershell script
    ppid                      Set parent PID for spawned post-ex jobs
    ps                        Show process list
    psexec                    Use a service to spawn a session on a host
    psexec_psh                Use PowerShell to spawn a session on a host
    psinject                  Execute PowerShell command in specific process
    pth                       Pass-the-hash using Mimikatz
    pwd                       Print current directory
    rev2self                  Revert to original token
    rm                        Remove a file or folder
    rportfwd                  Setup a reverse port forward
    runas                     Execute a program as another user
    runu                      Execute a program under another PID
    screenshot                Take a screenshot
    shell                     Execute a command via cmd.exe
    shinject                  Inject shellcode into a process
    shspawn                   Spawn process and inject shellcode into it
    sleep                     Set beacon sleep time
    socks                     Start SOCKS4a server to relay traffic
    socks stop                Stop SOCKS4a server
    spawn                     Spawn a session 
    spawnas                   Spawn a session as another user
    spawnto                   Set executable to spawn processes into
    spawnu                    Spawn a session under another PID
    ssh                       Use SSH to spawn an SSH session on a host
    ssh-key                   Use SSH to spawn an SSH session on a host
    steal_token               Steal access token from a process
    timestomp                 Apply timestamps from one file to another
    unlink                    Disconnect from parent Beacon
    upload                    Upload a file
    wdigest                   Dump plaintext credentials with mimikatz
    winrm                     Use WinRM to spawn a session on a host
    wmi                       Use WMI to spawn a session on a host
  ```
</details>

#### 2. A few usefull Beacon commands

<b>a) sleep</b>  
By-default Cobalt Strike beacon intracts with the server after every 1 minute. When user executes a beacon command, the moment beacon intracts with the server, it will get instrcuted by the server to execute the command.

Intraction time can be changed by executing the sleep command. By executing below mentioned command, intraction time will be set to 5 seconds:

    sleep 5


<b>b) shell</b> 

To execute the OS command using Windows cmd.exe, shell command with an argument can be used.

The below mentioned command will task beacon to execute the OS command `whoami` using Windows cmd.exe binary:

    shell whoami
  
<b>c) steal_token</b> 

When beacon is running as a user, which has sufficient privilege to steal session token from a process, steal token can be used.

Let's say, beacon is running with the privilege of the local admin user and an AD user is logged-in to the machine. The AD user opened notepad.exe and notepad.exe process has process ID `3312`.

To steal the token of this process, below mentioned command will serve the purpose:

    steal_token 3312
 
