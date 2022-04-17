# Basics

#### 1. Starting team server

    ./teamserver IP a_password
    ./teamserver 192.168.56.104 Admin_4612
    
#### 2. Starting client interface    

    java -jar cobaltstrike.jar
    

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Cobalt_strike/images/cb_start.png width="900" height="500"/>

#### 3. Creating a listener

    Cobalt Strike -> Listeners
    or
    Click the 'Headphone symbole'
    
Now, click 'Add' button to open the 'Add new listener' window:

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Cobalt_strike/images/cb_listener.png width="700" height="400"/>

Specify name, payload type (reverse https in this case) and the listening port (443 in this case)

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Cobalt_strike/images/cb_listener_details.png width="500" height="250"/>


#### 4. Generate the payload

To generate the payload for a listener, do following:

    Attacks -> Packages -> Payload generator/Windows executables/(S)
    
<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Cobalt_strike/images/cb_payload1.png width="500" height="250"/>

After generating the payload, execute it on target machine using any trick such as PowerShell file download and execute trick.

#### 5. Hosting generated payload files

To host the generated payload files using cobalt strike, navigate to following path:
    
    Attacks -> Web Drive-by -> Host File

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Cobalt_strike/images/host_file.png width="500" height="250"/>

Browse the file from local machine and specify the URL using which hosted file should be accessible:

<img src=https://raw.githubusercontent.com/incredibleindishell/Red_Team_Op/main/Cobalt_strike/images/host_file2.png  width="500" height="250"/>
   
./continue
