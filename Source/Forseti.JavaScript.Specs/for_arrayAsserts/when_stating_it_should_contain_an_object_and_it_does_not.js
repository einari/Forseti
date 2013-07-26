describe("when stating it should contain an object and it does not", function() {
	var myArray = [1,3,4];

	var result = null;
	try {
		myArray.shouldContain(2);
	} catch( ex ) {
		result = ex;
	}

	it("should throw an exception", function() {
		expect(result).not.toBe(null);
	});

});