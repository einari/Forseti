Bifrost.namespace("Forseti.suites", {
	Suite: Bifrost.Type.extend(function() {
		var self = this;

		this.name = "";
		this.descriptions = [];

		this.addDescription = function(description) {
			this.descriptions.push(description);
		};
	})
});