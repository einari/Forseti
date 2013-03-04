(function () {
    if (!jasmine) {
        throw new Exception("jasmine library does not exist in global namespace");
    }

    var ForsetiReporter = function () {
        this.started = false;
        this.finished = false;

        this.reportRunnerStarting = function (runner) {
            //forseti.log("Runner starting");
        };

        this.reportRunnerResults = function (runner) {
            //forseti.log("Runner results");
        };

        this.reportSuiteResults = function (suite) {
            //forseti.log("\nSuite(" + suite.description + ")");

            var specs = suite.specs();
            for (var specIndex = 0; specIndex < specs.length; specIndex++) {
                var spec = specs[specIndex];
                var messages = spec.results().getItems();
                for (var messageIndex = 0; messageIndex < messages.length; messageIndex++) {
                    var message = messages[messageIndex];
                    var passed = message.passed ? message.passed() : true;
                    if (passed !== true) {
                        forseti.reportFailedCase(suite.description, spec.description, messages[messageIndex].toString());
                    } else {
                        forseti.reportPassedCase(suite.description, spec.description);
                    }
                }
            }
            forseti.reportingComplete();
        };

        this.reportSpecStarting = function (spec) {
            //forseti.log("SpecStarting : " + spec);
        };

        this.reportSpecResults = function (spec) {
            //forseti.log("Spec : " + spec.description);
        };

        this.log = function (str) {
            //forseti.log("LOG : " + str);
        };
    };

    var reporter = new ForsetiReporter();
    jasmine.getEnv().addReporter(reporter);
})();


/*
MyReporter = function () {
    this.subReporters_ = [];
};
jasmine.util.inherit(MyReporter, jasmine.Reporter);

MyReporter.prototype.addReporter = function (reporter) {
    this.subReporters_.push(reporter);
};
*/