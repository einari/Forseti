describe("when adding case", function() {

	var expectedCase = {something:42};

	var suite = Forseti.suites.Description.create();
	suite.addCase(expectedCase);

	it("should have one case", function() {
		expect(suite.descriptions.length).toBe(1);
	});

});