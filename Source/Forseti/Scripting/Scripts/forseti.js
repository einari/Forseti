var forseti = (function (window) {


    var inBrowser = function () {
        return typeof window.Envjs === "undefined";
    };

    window.onload = function () {
        forseti.initialize();
        var systems = forseti.systems;
        var descriptions = forseti.descriptions;

        for (var i = 0; i < descriptions.length; i++) {
            var description = descriptions[i];
            var dependencies = systems.splice(0);
            dependencies.push(description);
            require(dependencies, function () {
                forseti.execute();
            });
        }
    };

    return {
        framework: {
            instance : null,
            initalize: function () { },
            execute: function () { }
        },
        runningInBrowser: inBrowser(),
        log: function (message) {
            if (this.runningInBrowser)
                typeof message === "string" ? console.log("Log : " + message)
                                            : console.log(message);
            else
                window.print(message);
        },
        reportPassedCase: function (description, caseName) {
            if (this.runningInBrowser)
                console.log("PASSED : " + description + " / " + caseName);
            else
                window.reportPassedCase(description, caseName);
        },
        reportFailedCase: function (descriptiom, caseName, message) {
            if (this.runningInBrowser)
                console.log("FAILED : " + description + " / " + caseName + " / message: " + message);
            else
                window.reportPassedCase(description, caseName, message);
        },
        systems: [],
        descriptions: [],
        execute: function () {
            if (this.framework)
                this.framework.execute();
        },
        initialize: function () {
            if (this.framework)
                this.framework.initialize();
        }

    }

})(window);