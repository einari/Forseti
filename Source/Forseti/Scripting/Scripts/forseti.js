var forseti = (function (window) {

    var nextDescriptionForExecutingIndex = 0;

    var inBrowser = function () {
        return typeof window.Envjs === "undefined";
    };

    window.onload = function () {
        forseti.run();
    };

    return {
        reset: function () {
            nextDescriptionForExecutingIndex = 0;
        },
        require: function () {
            if (!window.__require)
                window.__require = require;
            __require.apply(this,arguments);
        },
        framework: {
            instance: null,
            initialize: function () { },
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
            var descriptionFile = this.currentDescription;
            if (this.runningInBrowser)
                console.log("PASSED : " + description + " / " + caseName + " / " + descriptionFile);
            else
                window.reportPassedCase(description, caseName, descriptionFile);
        },
        reportFailedCase: function (description, caseName, message) {
            var descriptionFile = this.currentDescription;
            if (this.runningInBrowser)
                console.log("FAILED : " + description + " / " + caseName + " / message: " + message + " / " + descriptionFile);
            else
                window.reportFailedCase(description, caseName, message, descriptionFile);
        },
        systems: [],
        descriptions: [],
        execute: function () {
            if (this.framework) {
                this.framework.execute();
            }
        },
        hasUnexecutedDescriptions: function () {
            return nextDescriptionForExecutingIndex < this.descriptions.length;
        },
        getNextDescriptionForExecuting: function () {
            var description = this.descriptions[nextDescriptionForExecutingIndex];
            return description;
        },
        initialize: function () {
            if (this.framework) {
                this.framework.initialize();
                this.loadSystems();
            }
        },
        prepareNextDescription: function () {
            var nextDescription = this.getNextDescriptionForExecuting();

            this.currentDescription = nextDescription;
            nextDescriptionForExecutingIndex++;
        },
        currentDescription: "",
        loadSystems: function (func) {
            var sytems = this.require(this.systems);
        },
        run: function () {
            this.initialize();
            this.executeNextDescription();
        },
        reportingComplete: function () {
            this.executeNextDescription();
        },
        executeNextDescription: function () {
            var self = this;
            if (self.hasUnexecutedDescriptions()) {
                self.prepareNextDescription();
                this.require([self.currentDescription], function () {
                    self.execute();
                });
            }
        }
    }

})(window);