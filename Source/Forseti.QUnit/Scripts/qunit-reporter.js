(function () {
    if (!QUnit) {
        throw new Exception("QUnit library does not exist in global namespace");
    }


    QUnit.log(function (result) {
        if (result.result) {
            forseti.reportPassedCase(result.module, result.name);
        } else {
            forseti.reportFailedCase(result.module, result.name, result.message + " " + result.source);
        }
        
    });

    
    QUnit.done = function () {
        forseti.reportingComplete();
    }
})();

