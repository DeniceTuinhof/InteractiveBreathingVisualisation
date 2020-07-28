# Interactive-Breathing-Visualisation-Tool-Microphone
 An interactive breathing visualisation tool that uses a microphone
 
Within this simulation, raw audio is recorded and streamed into Unity. We used a C# script to transform the amplitude-frequency into eight bands of spectrum data. 
We distinguish breathing in from breathing out by checking whether the threshold for specific frequency bands is reached. When the data is classified as breathing in, the circle shape comes towards you. Once you breathe out, the circle moves away. 

We placed a plane into the Unity scene with the circle.png that can be found in assets. We also put an audio object into the scene and connected the Audiopeer script. The Audiopeer talks to the movement script, which is connected to the plane with the circle. 



This work is inspired by the work of: 

AudioPeer > yt = https://www.youtube.com/watch?v=Ri1uNPNlaVs&t=632s

George Lecakes > yt = ttps://www.youtube.com/watch?v=DrMyhQY2udg

