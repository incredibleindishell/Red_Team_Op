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
