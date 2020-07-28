# Interactive_breathing_visualisation_tool_microphone
 An interactive breathing visualisation tool that uses a microphone
 
Within this simulation raw audio is recorded and streamed into Unity. A C# script is used to transform the amplitude frequency into eight bands of spectrum data. 
We distinguish breathing in from breathing out by checking whether the threshold for certain frequency bands is reached. When the data is classified as breathing 
in the circle shape comes towards you. Once you breath out the circle moves away. 
