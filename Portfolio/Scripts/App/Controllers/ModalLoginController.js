app.controller("ModalLoginController", function($scope, $modal) {

    $scope.open = function() {
        $modal.open({
            templateUrl: "/Main/Login",
            controller: "ModalInstanceController"
        });
    };

});

app.controller("ModalInstanceController", function ($scope, $modalInstance) {

    $scope.ok = function() {
        alert($scope.text);
    };

    $scope.cancel = function() {
        $modalInstance.dismiss("cancel");
    };

});