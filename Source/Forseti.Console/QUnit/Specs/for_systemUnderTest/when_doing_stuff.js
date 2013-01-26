forseti.log("when doing stuff");

module("when_doing_stuff");

test("it should fail!", function () {
    equal(true,false);
});

test("it should not fail", function () {
    equal(true,true);
});