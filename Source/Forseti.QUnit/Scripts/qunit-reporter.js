(function () {
    if (!QUnit) {
        throw new Exception("QUnit library does not exist in global namespace");
    }

    QUnit.init();

    QUnit.testDone(function (result) {
        var caseName = "<QUnit Dummy Case>";

        if (result.failed > 0) {
            reportFailedCase(result.name, caseName, result.failed + " assert"+ (result.failed==1?"":"s")+" failed");
        } else {
            reportPassedCase(result.name, caseName);
        }
        
    });

    /*
    QUnit.log = function (result, message) {
        if (result == true) {
            console.log("PASS");
        } else {
            console.log("FAIL");
        }
        //forseti.log(result ? 'PASS' : 'FAIL');
    }*/
})();

