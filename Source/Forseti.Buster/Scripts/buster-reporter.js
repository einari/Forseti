buster.spec.expose();

(function() {
	var forsetiReporter = {
		create: function(opt) {
      		var reporter = buster.create(this);
            opt = opt || {};
            reporter.contexts = [];			
            
            return reporter;
		},
		
		contextStart: function(context) {
			//print("\nSuite("+context.name+")");
		},
		
		contextEnd: function(context) {
		},
		
		testSuccess: function(test) {
			reportPassedCase(test.name);
		},
		
		testFailure: function(test) {
			reportFailedCase(test.name,test.error.message);
		},
		
		testError: function(test) {
		},
		
		testTimeout: function(test) {
		},
		
		testDeferred: function(test) {
		},
		
		suiteEnd: function(suite) {
		},
		
		listen: function(runner) {
			runner.bind(this, {
				"context:start" : "contextStart",
				"context:end" : "contextEnd",
				"test:success" : "testSuccess",
				"test:failure" : "testFailure",
				"test:error" : "testError",
				"test:timeout" : "testTimeout",
				"test:deferred" : "testDeferred",
				"suite:end" : "suiteEnd"
			});
		}
	};
	
    if (typeof module == "object" && module.exports) {
        module.exports = forsetiReporter;
    } else {
        buster.reporters = buster.reporters || {};
        buster.reporters.html = forsetiReporter;
    }	
}());
