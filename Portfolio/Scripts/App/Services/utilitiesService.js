app.service("utilitiesService", function () {

    this.isItemInArray = function (array, item) {
        for (var i = 0; i <= array.length; i++) {
            if (array[i] == item) return true;
        }
        return false;
    };

    this.isItemInArrayProp = function (array, arrayProp, item) {
        var flat = flattenData(arrayProp, array);
        for (var i = 0; i <= flat.length; i++) {
            if (flat[i] == item) return true;
        }
        return false;
    };

    // Take in an array (can be nested to any level) and returns a flattened array of the property 
    // specified values.
    function flattenData(flattenByProp, array) {
        return _.flatten(_.pluck(array, flattenByProp.toString()));
    };

    this.removeFromArray = function (array, item) {
        for (var i = 0; i <= array.length; i++) {
            if (array[i] == item) {
                array.splice(i, 1);
            }
        }
    };

});