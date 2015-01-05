var forseti = (function (w) {
    function createLocalRequire() {

        var r = w.require;
        delete w.require;
        delete w.define;
        delete w.requirejs;

        w.__require = r;
    };

    function logToBrowser(message) {
        typeof message === "string"
                ? console.log("Log : " + message)
                : console.log(message);
    }

    function logToRunner(message) {
        w.print(message);
    }

    function loadNextDescription(description) {
        var options = {
            script: description,
            success: function () { forseti.execute(); }
        };

        forseti.getScript(options);
    }

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
        getScript: function (settings) {
            var options = {
                script: "",
                success: function () { },
                fail: function () { }
            };
            __jquery.extend(options, settings);


            if (this.runningInBrowser)
                this.require([options.script], options.success);
            else
                __jquery.getScript(options.script, function () {
                    options.success();
                })
                .fail(function (jqXhr, errorText, errorObject) {
                    forseti.reportFailedCase(description, "", errorText + " : " + errorObject + " in : " + description);
                    forseti.executeNextDescription();
                });

        },
        framework: {
            instance: null,
            initialize: function () { },
            execute: function () { }
        },
        runningInBrowser: inBrowser(),
        log: function () { },
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
            this.log = this.runningInBrowser ? logToBrowser : logToRunner;
            if (this.framework) {
                this.framework.initialize();
                this.loadSystems();
            }
        },
        prepareNextDescription: function () {
            var nextDescription = this.getNextDescriptionForExecuting();

            this.currentDescription = nextDescription;
            nextDescriptionForExecutingIndex++;
            return nextDescription;
        },
        currentDescription: "",
        loadSystems: function (func) {
            logToRunner("loadSystems");
            var sytems = this.require(this.systems);
        },
        run: function () {
            logToRunner("run");
            this.initialize();
            this.executeNextDescription();
        },
        reportingComplete: function () {
            logToRunner("reportingComplete");
            this.executeNextDescription();
        },
        executeNextDescription: function () {
            var self = this;
            if (self.hasUnexecutedDescriptions()) {
                var nextDescription = self.prepareNextDescription();
                loadNextDescription(nextDescription);
            }
        }
    };
})(window);