app.service("authenticationService", function ($http, $q) {

    this.Login = function (formData, token) {
        $http.post("/Account/JsonLogin", {
            method: "POST",
            headers: { "auth-token": token },
            data: formData
        }).success(function (data, status, headers, config) {
            var defer = $q.defer();
            var result = defer.resolve(data);
            if (result.status) {
                // Successful login
                return true;
            } else {
                console.log(data.error);
                // Unsuccessful login
                return false;
            }
        }).error(function (data, status, headers, config) {
            // error
            return false;
        });
    };

});