# Foreword #
Forseti is a headless test / spec runner written to be independent of testing / specification frameworks and also independent of JavaScript engines.

# No browser #
Most test / spec runners out there have focus on bringing in real browsers and testing your JavaScript code against the real browsers. Although, we recognize the importance of this - we have a different angle to it and are saying that we want to optimize for the 99% usages when one is writing JavaScript code, instead of optimizing the the scenarios of running against a real browser. With this in mind, Forseti uses as its default the JavaScript engine directly and utilizes [env.js](http://www.envjs.com/) to fake the browser. This enables faster running of the tests / specs, closing the feedback loop. [Env.js](http://www.envjs.com/) does a great job simulating the browser and you are even able to still run your jQuery tests / specs.

# #

# How to use #


# Why... Ohhh... Why... #

## Why another runner ##
Well. Looking at what was out there, we didn't feel that the philosophies of those projects were what we wanted. Feedback loop is really important and we want to optimize as mentioned for the 99% of the usage scenarios. The "edge cases" can be run in a browser context with other test runners on a continuous integration server, or a daily build - depending on how big feedback loop you can allow to have.

## Why .net ##