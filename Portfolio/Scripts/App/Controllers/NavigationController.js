// Setup navigation routes controller sets navbar items to active and so highlights when on page.
app.controller("NavigationController", function ($scope, $location) {
    $scope.getClass = function (path) {
        if ($location.path().substr(0, path.length) == path) {
            return true;
        } else {
            return false;
        }
    };
});