app.service("authenticationService", function ($http, $q) {

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
});