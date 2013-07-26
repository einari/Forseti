describe("when stating it should contain an object and it does", function() {
	var myArray = [1,2,3];

	var result = null;
	try {
		myArray.shouldContain(2);
	} catch( ex ) {
		result = ex;
	}

	it("should not throw an exception", function() {
		expect(result).toBe(null);
	});

});