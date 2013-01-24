var forseti = (function (w) {
    function createLocalRequire() {

        var r = w.require;
        delete w.require;
        delete w.define;
        delete w.requirejs;

        w.__require = r;
    };

    createLocalRequire();
    var nextDescriptionForExecutingIndex = 0;

    var inBrowser = function () {
        return typeof w.Envjs === "undefined";
    };

    w.onload = function () {
        forseti.run();
    };

    return {
        reset: function () {
            nextDescriptionForExecutingIndex = 0;
        },
        require: function () {
            __require.apply(this, arguments);
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
                w.print(message);
        },
        reportPassedCase: function (description, caseName) {
            var descriptionFile = this.currentDescription;
            if (this.runningInBrowser)
                console.log("PASSED : " + description + " / " + caseName + " / " + descriptionFile);
            else
                w.reportPassedCase(description, caseName, descriptionFile);
        },
        reportFailedCase: function (description, caseName, message) {
            var descriptionFile = this.currentDescription;
            if (this.runningInBrowser)
                console.log("FAILED : " + description + " / " + caseName + " / message: " + message + " / " + descriptionFile);
            else
                w.reportFailedCase(description, caseName, message, descriptionFile);
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
                __jquery.getScript(self.currentDescription, function () {
                    self.execute();
                })
                .fail(function (jqXhr, errorText, errorObject) {
                    self.reportFailedCase(self.currentDescription, "", errorText + " : " +  errorObject + " in : " + self.currentDescription);
                    self.executeNextDescription();
                });
            }
        }
    };

})(window);