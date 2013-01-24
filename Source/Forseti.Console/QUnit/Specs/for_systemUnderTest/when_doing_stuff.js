module("when_doing_stuff");

test("it should fail!", function () {
    ok(true == false);
});

test("it should not fail", function () {
    ok(true == true);
});