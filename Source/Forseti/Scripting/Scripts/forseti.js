var forseti = (function (window) {

    var inBrowser = function () {
        return typeof window.Envjs === "undefined";
    };


    return {
        runningInBrowser: inBrowser(),
        log: function (message) {
            if (this.runningInBrowser)
                console.log("Debug : " + message);
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
        }
    }

})(window);