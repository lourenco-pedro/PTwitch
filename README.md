# PTwitch

<p align="center"> 
<img src="https://media.giphy.com/media/SWv4iUzXhV1nC9edp7/giphy.gif" style="max-height: 300px;">
</p>

## About

An interactive way for your Twitch Followers watch your streams.

This git is the raw version of my PTwich project that I solved to share with you guys :)

Feel free to implement in your own way and use it for commercial porpuses.

If you guys have any feedback about the code - It architecture, performance, etc - I'll be glad to read them in my email pedropereralourenco@gmail.com.

## TwitchLib by swiftyspiffy

Para toda parte de interação com os serviços da Twitch foi utilizado a livraria TwitchLib, de swiftyspiffy. Meus agradecimentos para eles por facilitarem e realização dete projeto.

For the entire interaction with Twitch services, the TwitchLib library, from swiftyspiffy, was used. My thanks to them for facilitating the realization of this project

TwitchLib:  https://github.com/TwitchLib.

contact: swiftyspiffy@gmail.com.

# The Backend 

## About

You can modify the entire Backend source code as you want in **TwitchChat_bckEnd/**. 

In the Backend is where the registration of all activities sent by users in the chat happens. The program must save all of these events within a json file located in the path defined in **Program.cs**.

````csharp
public const string CredentialsJsonPath;
````

**note:** The json file is automatically created when you save all the definitions of PTwich settings in Unity Engine. Just click at the gear icon located on the top right corner of the screen.

###### The Json file will look like this
```json
{
    "UserName": "",
    "AccessToken": "",
    "Connected": false,
    "ReadMessages": false,
    "Followers": []
}
````

# The Frontend 

All the visual stuff, such as login authentication and chat visualization happens in the Unity Project. The project can be found in  **TwitchChat/**.

## Setup Dev and Standalone Path 

The frontend needs to receive the same json file path location, so it can collect all information and display them on the screen.

For that, you will need to define it in the Login Authorization screen, just click at the gear icon and set the values. No file name needs to be added, only the path where it will be loaded. 

**note:** There will be two ways where you can save the json file. One is for when you are testing in Unity Engine (DEV), and the other is when you build the project.
