describe("forsetiJs", function () {

    it("should create the forseti namespace", function () {
        var ns = forseti;

        expect(ns).not.toBeNull();
    });
});

describe("when reporting a passed case in envjs", function () {

    var originalDescription;
    var currentDescription = "App/for_system/when_testing.js";
    var reporter;

    require = function () { };
    beforeEach(function () {

        originalDescription = forseti.currentDescription;
        forseti.currentDescription = currentDescription;
        reporter = window.reportPassedCase = sinon.stub();
        forseti.runningInBrowser = false;

        forseti.reportPassedCase("descriptionName", "caseName");
    });

    afterEach(function () {
        forseti.currentDescription = originalDescription;
        forseti.runningInBrowser = true;

        window.reportPassedCase = null;
    });

    it("should report the passed case result to 'forseti's' reporter", function () {

        expect(reporter.calledOnce).toBe(true);

    });

    it("should call with the description name", function () {
        var descriptionInput = reporter.args[0][0];
        expect(descriptionInput).toBe("descriptionName");

    });
    it("should call with the case name", function () {
        var caseName = reporter.args[0][1];
        expect(caseName).toBe("caseName");

    });
    it("should call with the current descriptionFile", function () {
        var descriptionfile = reporter.args[0][2];
        expect(descriptionfile).toBe(currentDescription);

    });
});


describe("when reporting a failed case in envjs", function () {

    var originalDescription;
    var currentDescription = "App/for_system/when_testing.js";
    var reporter;

    beforeEach(function () {

        originalDescription = forseti.currentDescription;
        forseti.currentDescription = currentDescription;
        reporter = window.reportFailedCase = sinon.stub();
        forseti.runningInBrowser = false;

        forseti.reportFailedCase("descriptionName", "caseName", "message");
    });

    afterEach(function () {
        forseti.currentDescription = originalDescription;
        forseti.runningInBrowser = true;

        window.reportFailedCase = null;
    });

    it("should report the failed case result to 'forseti's' reporter", function () {

        expect(reporter.calledOnce).toBe(true);

    });

    it("should call with the description name", function () {
        var descriptionInput = reporter.args[0][0];
        expect(descriptionInput).toBe("descriptionName");

    });
    it("should call with the case name", function () {
        var caseName = reporter.args[0][1];
        expect(caseName).toBe("caseName");

    });

    it("should call with the message", function () {
        var message = reporter.args[0][2];
        expect(message).toBe("message");

    });
    it("should call with the current descriptionFile", function () {
        var descriptionfile = reporter.args[0][3];
        expect(descriptionfile).toBe(currentDescription);

    });

});


describe("when running forseti", function () {

    //stubs
    var initialized, executing, loadSystems;

    beforeEach(function () {
        initialized = sinon.stub(forseti.framework, "initialize");
        loadSystems = sinon.stub(forseti, "loadSystems");
        executing = sinon.stub(forseti, "executeNextDescription");

        forseti.run();
    });

    afterEach(function () {
        initialized.restore();
        executing.restore();
        loadSystems.restore();
    });


    it("should initialize the current framework", function () {

        expect(initialized.calledOnce).toBe(true);
    });
    it("should load systems", function () {
        expect(loadSystems.calledOnce).toBe(true);
    });

    it("start executing descriptions", function () {
        expect(executing.calledOnce).toBe(true);
    });

    it("should load systems before executing descriptions", function () {
        expect(loadSystems.calledBefore(executing)).toBe(true);
    });
});

describe("when executing next description and there are no descriptions to be executed", function () {

    beforeEach(function () {
        forseti.descriptions = [];
        require = sinon.stub();

        forseti.executeNextDescription();
    });

    afterEach(function () {
        forseti.descriptions = [];
        require = function () { };
        forseti.reset();
    });

    it("should not attempt to reqiure any descriptions", function () {
        expect(require.called).toBe(false);
    });
});

describe("when executing next description and there are descriptions to be executed", function () {

    var execute;

    beforeEach(function () {
        forseti.descriptions = ["description1.js"];
        execute = sinon.stub(forseti, "execute");
        require = sinon.stub();

        forseti.executeNextDescription();

        require.args[0][1](); //triggering the require callback
    });

    afterEach(function () {
        forseti.descriptions = [];
        require = function () { };
        execute.restore();
        forseti.reset();
    });


    it("should attempt to require only one file", function () {
        expect(require.calledOnce).toBe(true);
    })

    it("should require the next description file", function () {
        var requiredDescription = require.args[0][0];

        expect(requiredDescription[0]).toBe("description1.js");
    });

    it("should call execute on the underlying framework", function () {

        expect(execute.calledOnce).toBe(true);
    });

});

describe("when reporting is marked as completed", function () {


    var execute;

    beforeEach(function () {
        execute = sinon.stub(forseti, "executeNextDescription");

        forseti.reportingComplete();
    });

    afterEach(function () {
        execute.restore();
    });

    it("should attempt to execute the next decription", function () {
        expect(execute.calledOnce).toBe(true);
    });


});