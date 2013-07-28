Bifrost.namespace("Forseti.suites", {
	Description: Bifrost.Type.extend(function() {
		var self = this;

		this.name = "";
		this.file = "";

		this.cases = [];

		this.addCase = function(caseToAdd) {
			self.cases.push(caseToAdd);
		};
	})
});