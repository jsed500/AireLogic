app.service("peopleService", function ($http, $q) {

    var factory = this;

    var _getAllPeople = function () {

        var deferred = $q.defer();
        
        $http.get("api/People/GetAll")
            .then(function (result) {
                deferred.resolve(result.data);
            }, function (msg, code) {
                deferred.reject(msg);
            });

        return deferred.promise;
    };

    var _getPerson = function (id) {

        var deferred = $q.defer();

        $http.get("api/People/GetPerson?id=" + id)
            .then(function (result) {
                deferred.resolve(result.data);
            }, function (msg, code) {
                deferred.reject(msg);
            });

        return deferred.promise;
    };

    var _updatePerson = function (person) {

        var deferred = $q.defer();

        $http.put("api/People/UpdatePerson", person)
            .then(function (result) {
                deferred.resolve(result.data);
            }, function (msg, code) {
                deferred.reject(msg);
            });

        return deferred.promise;
    };

    factory.GetAllPeople = _getAllPeople;
    factory.GetPerson = _getPerson;
    factory.UpdatePerson = _updatePerson;

});