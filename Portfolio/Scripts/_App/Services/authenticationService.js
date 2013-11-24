app.service("authenticationService", ["$http", "$q", function ($http, $q) {

    this.Login = function (formData, token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/JsonLogin",
            data: formData,
            headers: { "RequestVerificationToken": token },
        }).success(function (data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    };

    this.Register = function (formData, token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/JsonRegister",
            data: formData,
            headers: { "RequestVerificationToken": token },
        }).success(function (data) {
            deferred.resolve(data);
        });
        return deferred.promise;
    };

    this.LogOut = function (token) {
        var deferred = $q.defer();
        $http({
            method: "POST",
            url: "Account/LogOff",
            headers: { "RequestVerificationToken": token }
        }).success(function (data) {
            deferred.resolve(data);
        });;
        return deferred.promise;
    };

}]);