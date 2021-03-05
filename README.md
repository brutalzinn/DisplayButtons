# ButtonDeck-Windows
A simple way to run macros using your cell phone.

This project is initialy a fork of the [NickAcPT] project (https://github.com/NickAcPT).com/NickAcPT)


This project consists of 5 divisions of responsibility in the same repository.

BackendAPI - Responsible for the backend, and implementation of the interface for lua plugins
MoonSharpInterpreter - Lua interpreter used by the backend, to extend an application at the sandbox level.
InterfaceDLL - Interface required to create plugins and external dependencies (DLLs) using C #
NthMultiLanguage - External library for configuring multiple languages ​​in the frontend layer.
ProxyBackend - Communication between Backend events and the frontend
Setup - Creating the ButtonDeck installer
DisplayButtons - Joining all other solutions.

To use ButtonDeck, it is necessary to install the client on the Android device

 [Client ButtonDeck] (https://github.com/brutalzinn/ButtonDeck-Android)



## Contributors
 - [brutalzinn](https://www.github.com/brutalzinn/) - Sistema de plugins, eventos e reescrição de todo o projeto original.
 - [NickAcPT](https://github.com/NickAcPT) - Criador original do ButtonDeck
 - [MatthewHaskett](https://www.github.com/MatthewHaskett/) - Ajudou com o protocolo TCP.
