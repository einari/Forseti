(function (global) {
    global.require = undefined;
    global.define = undefined;
    global.requirejs = undefined;

    global.__jquery = jQuery.noConflict(true);
})(window);