describe("when doing other stuff", function () {
    it("We want to fail", function () {
        expect(true).toEqual(false);
    });
    it("Should not fail", function () {
        expect(true).toEqual(true);
    });
});
