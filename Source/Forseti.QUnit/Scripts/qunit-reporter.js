(function () {
    if (!QUnit) {
        throw new Exception("QUnit library does not exist in global namespace");
    }

    QUnit.begin(function () {
        forseti.log("Qunit-begin");
        forseti.log(arguments);
    });

    QUnit.moduleStart(function () {
        forseti.log("Qunit-moduleStart");
        forseti.log(arguments);
    });
    


    QUnit.testStart(function () {
        forseti.log("Qunit-testStart");
        forseti.log(arguments);
    });
    


    QUnit.log(function (result) {
        forseti.log("Qunit-log");
        forseti.log(arguments);
        if (result.result) {
            forseti.reportPassedCase(result.module, result.name);
        } else {
            forseti.reportFailedCase(result.module, result.name, result.message + " " + result.source);
        }

    });

    QUnit.testDone(function () {
        forseti.log("Qunit-testDone");
        forseti.log(arguments);
    });

    QUnit.moduleDone(function () {
        forseti.log("Qunit-moduleDone");
        forseti.log(arguments);
    });

    QUnit.done(function () {
        forseti.log("Qunit-done");
        forseti.log(arguments);
        forseti.reportingComplete();
    });
})();

