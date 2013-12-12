app.service("utilitiesService", function () {

    this.isItemInArray = function (array, item) {
        for (var i = 0; i <= array.length; i++) {
            if (array[i] === item) return true;
        }
        return false;
    };

    this.isItemInArrayByProperty = function (array, arrayProp, item) {
        var flat = flattenData(arrayProp, array);
        for (var i = 0; i <= flat.length; i++) {
            if (flat[i] === item) return true;
        }
        return false;
    };

    // Returns the object where the matching key has been found.
    // Searches through collection of objects to find the objects with a matching property
    // and then checks if the properties value is equal to item.
    this.findObjByKey = function (array, arrayProp, item) {
        for (var i = 0; i < array.length; i++) {
            if (array[i][arrayProp] === item) {
                return array[i];
            }
        }
        return [];
    };

    // Take in an array (can be nested to any level) and returns a flattened array of the specified properties values.
    function flattenData(flattenByProp, array) {
        return _.flatten(_.pluck(array, flattenByProp.toString()));
    }

    this.removeFromArray = function (array, item) {
        for (var i = 0; i <= array.length; i++) {
            if (array[i] === item) {
                array.splice(i, 1);
            }
        }
    };

});