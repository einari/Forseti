(function () {
    //forseti.log(something);

    if (!forseti) {
        throw new Exception();
    }

    var framework = forseti.framework;
    framework.initialize = function () {
        if (!framework.instance) {
            var jasmineEnv = jasmine.getEnv();
            jasmineEnv.updateInterval = 1000;
            framework.instance = jasmineEnv;
            framework.instance.execute();
        }
    };

    framework.execute = function () {
//        if (framework.instance)
//            framework.instance.execute();
    }
})();