describe("when doing stuff", function () {
    it("Another fail", function () {
        expect(true).toEqual(false);
    });
    it("Should not fail", function () {
        expect(true).toBe(true);
    });
    it("should fail", function () {
        expect(true).toEqual(false);
    });
});
