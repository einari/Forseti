Bifrost.namespace("Forseti.harnesses", {
	Harness: Bifrost.Type.extend(function() {
		var self = this;

		this.suites = [];


		this.addSuite = function(suite) {
			self.suites.push(suite);
		};

	})
});