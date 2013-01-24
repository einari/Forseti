(function () {

    if (!forseti) {
        throw new Exception();
    }

    var framework = forseti.framework;
    framework.initialize = function () {
        if (!framework.instance) {
            QUnit.init();
            QUnit.config.autostart = true;
            QUnit.config.blocking = false;
            QUnit.config.autorun = false;
            QUnit.config.updateRate = 0;
            framework.instance = QUnit;
        }
    };

    framework.execute = function () {
        //to manually start run: 
        //QUnit.start();

        //QUnit.stop() //to stop (duh!)
    }
})();
