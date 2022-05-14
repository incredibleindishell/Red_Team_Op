### AD Domain Reconnaissance using PowerView
---

Load PowerView using Cobal Strike beacon session:

    powershell-import PowerView.ps1
    
Call any function using PowerShell

Get domain related info:

    powershell Get-domain
    
Get the list of Domain Comtrollers of current or specific domain:

     powershell Get-DomainController | select Forest, Name, OSVersion | fl
     powershell Get-DomainController -Domain ica.lab | select Forest, Name, OSVersion | fl

Get List of the domains in current forest

     powershell Get-ForestDomain
     powershell Get-ForestDomain -Forest DC01.ica.lab

To get the user password related policy

     powershell Get-DomainPolicyData | select -ExpandProperty SystemAccess

To retrieve the information of domain users

     powershell Get-DomainUser 
     powershell Get-DomainUser -Identity administrator  <--- To get the info of particular user account

powershell Get-DomainUser -Identity ad_user -Properties DisplayName, MemberOf | fl


powershell Get-DomainComputer  
powershell Get-DomainComputer  -Properties name

powershell Get-DomainOU 

powershell Get-DomainGroup | where Name -like "*Admins*" | select SamAccountName,description

powershell Get-DomainGroup | where Name -like "*Admins*" | select cn, member, memberof | fl


powershell Get-DomainGroupMember -Identity "Domain Admins" 
powershell Get-DomainGroupMember -Identity "Domain Admins" | where MemberName -like "Admin*" 
powershell Get-DomainGroupMember -Identity "Domain Admins" | select MemberDistinguishedName


powershell Get-DomainGPO | select displayname
powershell Get-DomainGPO -ComputerIdentity computer_name

List the GPOs applied to particular domain joined machine:
powershell Get-DomainGPO -ComputerIdentity WIN-A08PEI13CFI | select displayname, iscriticalsystemobject | fl


Returns all GPOs that modify local group memberships through Restricted Groups or Group Policy Preferences.
powershell Get-DomainGPOLocalGroup | select GPODisplayName, GroupName

Enumerates the machines where a specific domain user/group is a member of a specific local group.
powershell Get-DomainGPOUserLocalGroupMapping -LocalGroup Administrators | select ObjectName, GPODisplayName, ContainerName, ComputerName


Enumerates all machines and queries the domain for users of a specified group (default Domain Admins). Then finds domain machines where those users are logged into.

powershell Find-DomainUserLocation 

powershell Find-DomainUserLocation  -UserIdentity Administrator

powershell Find-DomainUserLocation -UserGroupIdentity "Domain Admins"
powershell Find-DomainUserLocation -CheckAccess
powershell Find-DomainUserLocation -UserAdminCount

Returns session information for the local (or a remote) machine (where CName is the source IP).
powershell Get-NetSession -ComputerName dc-2 | select CName, UserName
