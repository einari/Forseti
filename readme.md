# The What #
Forseti is a headless test / spec runner for JavaScript written to be independent of testing / specification frameworks and also independent of JavaScript engines. It has been built for keeping the feedback loop as tight as possible and has a high focus on conventions and automatic running of tests / specs.

For a quick demo on Forseti running on Windows using the [BusterJS](http://busterjs.org/) framework, have a look [here](https://vimeo.com/45744794)

# No browser #
Most test / spec runners out there have focus on bringing in real browsers and testing your JavaScript code against the real browsers. Although, we recognize the importance of this - we have a different angle to it and are saying that we want to optimize for the 99% usages when one is writing JavaScript code, instead of optimizing the the scenarios of running against a real browser. With this in mind, Forseti uses as its default the JavaScript engine directly and utilizes [env.js](http://www.envjs.com/) to fake the browser. This enables faster running of the tests / specs, closing the feedback loop. [Env.js](http://www.envjs.com/) does a great job simulating the browser and you are even able to still run your jQuery tests / specs.

# How to use #

Head over to the [wiki](https://github.com/dolittle/Forseti/wiki) for help on getting started with Forseti.


# Getting started #

You can either build it yourself, or download a pre-compiled binary package from [here](https://github.com/dolittle/Forseti/wiki). You will however have to have .net 4 installed on a Windows machine or Mono 2.8 installed on Mac OSX or Linux. The Forseti.exe file in the distribution is the one you want to run with Mono.  
  
A tip for Mono users is to set up Mono to by default run runtime version 4 in your .bash_profile file :

	export MONO_ENV_OPTIONS='--runtime=v4.0'
 
 
# How to build #

Forseti compiles fine in MonoDevelop 2 and Visual Studio 2010/2012. Once you have the source, you navigate to the Source/Solutions path and you'll find two solution files, one that is tested under Windows and Visual Studio 2010 called *'Forseti.sln'* and another that has been tested with MonoDevelop 2 and Mac OSX; *'Forseti OSX.sln'*. If you're on OSX, you can't just go ahead and compile it, you need all the packages it depends on downloaded first. A rakefile has been created to do the downloads of these packages from [Nuget](http://www.nuget.org). 
From your terminal window :

	Your-Computer:Source someone$ sudo rake

Then you should be able to build it.
On Windows you only need to open the solution and build it with Visual Studio, it will automatically download the dependencies from Nuget.



# Why... Ohhh... Why... #

### Why another runner ###
Well. Looking at what was out there, we didn't feel that the philosophies of those projects were what we wanted. Feedback loop is really important and we want to optimize as mentioned for the 99% of the usage scenarios. The "edge cases" can be run in a browser context with other test runners on a continuous integration server, or a daily build - depending on how big feedback loop you can allow to have.

Another reason we wanted to create our own runner is that most runners was hard to get up and running properly, and also was very focused on the terminal window. Sure, the terminal is fine and one could probably argue that real developers live in the terminal. We think we can do better, so even though the terminal is one option of Forseti, in time we will have more options such as integration with popular IDEs.

### Why write it in .net ###
Well. The short answer; this is what we know best. Even though not considered the most cross platform friendly platforms, we have had great success with Mono.

# Future #
Please keep an eye open in the issues part of this site to see what we're working on, what features we're adding. Please contribute to the project through the issues list, or fork it and add features and send us a pull request. 
