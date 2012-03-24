buster.spec.expose();

/*
function print(m) {
	console.log(m);
}

function reportPassedCase(name) {
	print("  SPEC( "+name+" ) PASSED");

}

function reportFailedCase(name, message) {
	print("  SPEC( "+name+" ) FAILED with message : "+message);
}*/


(function() {
	var forsetiReporter = {
		create: function(opt) {
      		var reporter = buster.create(this);
            opt = opt || {};
            reporter.contexts = [];			
            
            return reporter;
		},
		
		contextStart: function(context) {
			print("\nSuite("+context.name+")");
		},
		
		testSuccess: function(test) {
			reportPassedCase(test.name);
		},
		
		testFailure: function(test) {
			reportFailedCase(test.name,test.error.message);
		},
		
		listen: function(runner) {
			runner.bind(this, {
				"context:start" : "contextStart",
				"test:success" : "testSuccess",
				"test:failure" : "testFailure"
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
