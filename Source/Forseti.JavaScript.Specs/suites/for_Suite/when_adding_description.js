describe("when adding description", function() {

	var description = {something:42};

	var suite = Forseti.suites.Suite.create();
	suite.addDescription(description);

	it("should have one description", function() {
		expect(suite.descriptions.length).toBe(1);
	});

});