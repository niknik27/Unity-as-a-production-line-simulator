# Unity-as-a-production-line-simulator
A project I made in 2 weeks using Unity to create a simulation of a production line for doors and windows

## Motivation
This was created as a coding test for an interview. I incredibly enjoyed creating this project because I had to learn how to 
create and manage threads. I was also curious if I would be able to create this project on Unity because it isn't very thread friendly 
and is not necessarily designed for how I envisioned the project.

## Build status
[![Build Status](http://img.shields.io/travis/badges/badgerbadgerbadger.svg?style=flat-square)](https://travis-ci.org/badges/badgerbadgerbadger)

## Preview
<img src="TinyPlanetGame.gif" width="100%">

## Screenshots
![Image](gameScreenshot1.png?raw=true "Screenshot 1")

![Image](gameScreenshot2.png?raw=true "Screenshot 2")

## Tech/framework used
<b>Built with</b>
- [Unity](https://unity.com/)

## Features
  > Multi-threaded application
  
  > Door measurements are randomly generated to simulate orders
  
  > Calculates height and width of the window using door measurements. Windows are calculated differently depending on 3 different window types.
  
  > Constantly calculates the average measurements of finished doors and windows after each new addition
  
  > The speed of the lasers and products can be changed during runtime
  
  > Generates a report in a .txt file that lists each product information as well as the final averages of the measurements
    (Note: the .txt file path must be changed for it to work properly)

## Installation
> Step 1: Download and install [Unity](https://store.unity.com/download-nuo) (Sign in might be needed when opening)

> Step 2: Download all files from repository

> Step 3: Click "Open" from Unity after installation

> Step 4: Select folder where all files from this repository are located on your computer

> Step 5: Code!

## IMPORTANT:

>This project was developed with Unity 2019

>The door and window graphics move but do not get altered according to their measurements

Copyright 2019 © [niknik27](https://github.com/niknik27)
