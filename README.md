# Display Buttons-Windows
A simple way to run macros using your cell phone.

This project is initialy a fork of the [NickAcPT] project (https://github.com/NickAcPT).com/NickAcPT)


This project consists of 5 divisions of responsibility in the same repository.

BackendAPI - Responsible for the backend, and implementation of the interface for lua plugins,communication with extern api services(github and spotify)
MoonSharpInterpreter - Lua interpreter used by the backend, to extend an application at the sandbox level.
InterfaceDLL - Interface required to create plugins and external dependencies (DLLs) using C #
NthMultiLanguage - External library for configuring multiple languages ​​in the frontend layer.
ProxyBackend - Communication between Backend events and the frontend
Setup - Creating the Display Buttons installer
DisplayButtons - Joining all other solutions.

To use Display Buttons, it is necessary to install the client on the Android device

 [Client Display Buttons] (https://github.com/brutalzinn/Display Buttons-Android)



## Contributors
 - [brutalzinn](https://www.github.com/brutalzinn/) - System of plugins, events and rewriting of the entire original project.
 - [NickAcPT](https://github.com/NickAcPT) - Original creator of Display Buttons
 - [MatthewHaskett](https://www.github.com/MatthewHaskett/) - Helped with the TCP protocol.

