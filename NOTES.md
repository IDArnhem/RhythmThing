## Cool vids for inspiration

- https://www.youtube.com/watch?v=Cp5WWtMoeKg
- https://www.youtube.com/watch?v=5zbFVfEGwnw Dan Paluska & Dan Lieberman, Absolut comission. Inspiration on the connection between digital and physical.

## The hassle of Unity versions and Unity Hub

Unity is quite finicky about versions. A project made with one version of Unity might not work well in another version. Keeping up with all the versions is quite a hassle, specially if you are collaborating and different people in your team have different versions. So we suggest installing [Unity Hub](https://unity3d.com/get-unity/download) which makes this process a bit easier and more transparent. 

## Collaborating in a Unity project

The easiest and "free" way of doing this is to use Github. Follow these steps:

- install unity **2018.3.11f1** (and don't expect that a unity version close to that number will also work!)
- install the Unity Github package http://unity.github.com
- import it as a plugin into your project
- you will also need `git-lfs`, which you can install in your mac using Homebrew, like this: `brew install git-lfs`
- now you have everything installed
- our common project is hosted in this shared github repository: https://github.com/IDArnhem/RhythmThing.git
- clone it form Unity Git
- create a branch with your name, that will create a development line just for you that will hopefully not affect the work of others
- happy hacking!
