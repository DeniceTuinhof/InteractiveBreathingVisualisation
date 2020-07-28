# Interactive-Breathing-Visualisation-Tool-Microphone
 An interactive breathing visualisation tool that uses a microphone
 
Within this simulation, raw audio is recorded and streamed into Unity. We used a C# script to transform the amplitude-frequency into eight bands of spectrum data. 
We distinguish breathing in from breathing out by checking whether the threshold for specific frequency bands is reached. When the data is classified as breathing in, the circle shape comes towards you. Once you breathe out, the circle moves away. 

SET-UP
1. Install Unity and open a new project. 
2. Download the assets and import to your Unity project. 
3. Place a plane into the Unity scene. 
4. Connect the circle (circle.png) from assets to the Unity scene. 
5. Put audio object into the scene. 
6. Connect the Audiopeer script to the audio object. 
7. Connect the Movementscript to the plane. 

* make sure you use an external microphone and make sure you breath into the microphone from a small range (place in front of the mouth) 
* fine tune the threshold as these might differ per microphone laptop combination. 
* copy paste the png and change the Z-coordinates to have multiple objects moving. 
* add a video to the plane to create an dynamic invironment. 

This work is inspired by the work of: 

AudioPeer > yt = https://www.youtube.com/watch?v=Ri1uNPNlaVs&t=632s

George Lecakes > yt = ttps://www.youtube.com/watch?v=DrMyhQY2udg

