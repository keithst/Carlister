(function () {
    angular.module("car-finder").factory('testCarSvc', ['$http', function ($http) {
        var f = {};

        f.getYears = function () {
            return $http.post('/api/Car/GetYearsDist').then(function (response) {
                return response.data;
            });
        }

        f.getMakes = function (selected) {
            return $http.post('/api/Car/GetMakesByYearDist', selected).then(function (response) {
                return response.data;
            });
        }

        f.getModels = function (selected) {
            return $http.post('/api/Car/GetModelsByYearMakeDist', selected).then(function (response) {
                return response.data;
            });

        }

        f.getTrims = function (selected) {
            return $http.post('/api/Car/GetTrimsByYearMakeModelDist', selected).then(function (response) {
                return response.data;
            });
        }

        f.getCars = function (selected) {
            return $http.post('/api/Car/GetVariableCars', selected).then(function (response) {
                return response.data;
            });
        }

        f.getMakes1 = function () {
            return $http.post('/api/Car/GetMakesDist').then(function (response) {
                return response.data;
            });
        }

        f.getYears1 = function (selected) {
            return $http.post('/api/Car/GetYearsByMakeDist', selected).then(function (response) {
                return response.data;
            });
        }

        return f;

    }
    ])
})();