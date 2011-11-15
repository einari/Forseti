(function () {
    if (!jasmine) {
        throw new Exception("jasmine library does not exist in global namespace");
    }

    var ForsetiReporter = function () {
        this.started = false;
        this.finished = false;

        this.reportRunnerStarting = function (runner) {
            //print("Runner starting");
        };

        this.reportRunnerResults = function (runner) {
            //print("Runner results");
        };

        this.reportSuiteResults = function (suite) {
            //print("Suite Results");
        };

        this.reportSpecStarting = function (spec) {
            //print("SpecStarting : " + spec);
        };

        this.reportSpecResults = function (spec) {
            //print("Spec : " + spec.description);

            var messages = spec.results().getItems();
            for (var messageIndex = 0; messageIndex < messages.length; messageIndex++) {
                print("Message : " + messages[messageIndex].toString());
            }

            //var items = spec.getResults().getItems();
            //print("Items : "+items);
            //print("Spec : " + spec);
        };

        this.log = function (str) {
            //print("LOG : " + str);
        };
    };

    var reporter = new ForsetiReporter()
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