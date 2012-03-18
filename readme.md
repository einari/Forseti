# Foreword #
Forseti is a headless test / spec runner written to be independent of testing / specification frameworks and also independent of JavaScript engines.

# No browser #
Most test / spec runners out there have focus on bringing in real browsers and testing your JavaScript code against the real browsers. Although, we recognize the importance of this - we have a different angle to it and are saying that we want to optimize for the 99% usages when one is writing JavaScript code, instead of optimizing the the scenarios of running against a real browser. With this in mind, Forseti uses as its default the JavaScript engine directly and utilizes [env.js](http://www.envjs.com/) to fake the browser. This enables faster running of the tests / specs, closing the feedback loop. [Env.js](http://www.envjs.com/) does a great job simulating the browser and you are even able to still run your jQuery tests / specs.

# How to build #

Forseti compiles fine in MonoDevelop 2 and Visual Studio 2010. Once you have the source, you navigate to the Source/Solutions path and you'll find two solution files, one that is tested under Windows and Visual Studio 2010 called *'Forseti.sln'* and another that has been tested with MonoDevelop 2 and Mac OSX; *'Forseti OSX.sln'*. If you're on OSX, you can't just go ahead and compile it, you need all the packages it depends on downloaded first. A rakefile has been created to do the downloads of these packages from [Nuget](http://www.nuget.org). 
From your terminal window :

	Your-Computer:Source someone$ sudo rake

Then you should be able to build it.
On Windows you only need to open the solution and build it with Visual Studio, it will automatically download the dependencies from Nuget.

# How to use #

For now, the only thing up and running is the console runner. It relies on a configuration file that you need to create called *forseti.yaml* - its layout is like this : 

	Harnesses:
		- Harness:
			Name					: [name of harness]
			SystemsSearchPath		: [relative path from current directory]/{placeholders}.js
			DescriptionsSearchPath	: [relative path from current directory]/{placeholders}.js

A concrete configuration could be something like this : 

	Harnesses:
		- Harness
			Name					: Something
			SystemsSearchPath		: Scripts/{system}.js
			DescriptionsSearchPath	: Specs/for_{system}/{description}.js
			
In addition, Forseti has a temporary solution for specifying dependencies that it will load for every time it runs tests / specs. These are defined in a file called dependencies.config by just adding file-paths on a per line basis.



# Why... Ohhh... Why... #

### Why another runner ###
Well. Looking at what was out there, we didn't feel that the philosophies of those projects were what we wanted. Feedback loop is really important and we want to optimize as mentioned for the 99% of the usage scenarios. The "edge cases" can be run in a browser context with other test runners on a continuous integration server, or a daily build - depending on how big feedback loop you can allow to have.

Another reason we wanted to create our own runner is that most runners was hard to get up and running properly, and also was very focused on the terminal window. Sure, the terminal is fine and one could probably argue that real developers live in the terminal. We think we can do better, so even though the terminal is one option of Forseti, in time we will have more options such as integration with popular IDEs.

### Why write it in .net ###
Well. The short answer; this is what we know best. Even though not considered the most cross platform friendly platforms, we have had great success with Mono.
