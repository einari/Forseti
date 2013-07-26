Array.prototype.contains = function(obj) {

	var found = false;
	this.some(function(item) {
		if( item === obj ) {
			found = true;
			return true;
		}
	});
    return found;
};

Array.prototype.shouldContain = function(expected) {
	if( this.contains(expected) == false ) {
		throw "Horse";
	}
};

Array.prototype.shouldBeEmpty = function() {

};

Array.prototype.shouldNotBeEmpty = function() {

};