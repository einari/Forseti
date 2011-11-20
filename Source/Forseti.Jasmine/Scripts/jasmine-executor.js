(function () {
    //print(something);
    var jasmineEnv = jasmine.getEnv();
    jasmineEnv.updateInterval = 1000;
    jasmineEnv.execute();
})();