describe("when adding suite", function() {

	var suite = {something:42};
	var harness = Forseti.harnesses.Harness.create();
	harness.addSuite(suite);

	it("should have one suite", function() {
		expect(harness.suites.length).toBe(1);
	});

	it("should have the suite added", function() {
		expect(harness.suites[0]).toBe(suite);
	});
});