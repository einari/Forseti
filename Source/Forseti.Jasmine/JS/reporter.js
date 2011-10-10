MyReporter = function () {
    this.subReporters_ = [];
};
jasmine.util.inherit(MyReporter, jasmine.Reporter);

MyReporter.prototype.addReporter = function (reporter) {
    this.subReporters_.push(reporter);
};
