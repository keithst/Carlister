﻿(function () {
    angular.module("car-finder").factory('testCarSvc', ['$q', function ($q) {
        var f = {};

        f.getYears = function () {
            var d = $q.defer();
            d.resolve(['1999', '2000', '2001', '2002'])
            return d.promise;
        }

        f.getMakes = function (year) {
            var d = $q.defer();
            switch (year) {
                case '1999':
                case '2000':
                    d.resolve(['kia', 'ford'])
                    break;
                case '2001':
                case '2002':
                    d.resolve(['acura', 'chevy'])
                    break;
            }

            return d.promise;
        }

        f.getModels = function (year, make) {
            var d = $q.defer();
            switch (make) {
                case 'kia':
                    d.resolve(['optima', 'rio'])
                    break;
                case 'ford':
                    d.resolve(['fusion', 'mustang'])
                    break;
                case 'acura':
                    d.resolve(['NSX', 'TL'])
                    break;
                case 'chevy':
                    d.resolve(['impala', 'corvette'])
                    break;
            }

            return d.promise;

        }

        f.getTrims = function (year, make, model) {
            var d = $q.defer();
            d.resolve(['4-door', '2-door']);
            return d.promise;
        }

        f.getCars = function (year, make, model, trim) {
            var d = $q.defer();
            d.resolve([
                {
                    year: year,
                    make: make,
                    model: model,
                    trim: trim,
                    color: 'black'
                },
                {
                    year: year,
                    make: make,
                    model: model,
                    trim: trim,
                    color: 'white'
                }
            ]);
            return d.promise;
        }

        f.getMakes1 = function () {
            var d = $q.defer();
            d.resolve(['kia', 'ford', 'acura', 'chevy'])
            return d.promise;
        }

        f.getYears1 = function (make) {
            var d = $q.defer();
            switch (make) {
                case 'kia':
                    d.resolve(['1999', '2000'])
                    break;
                case 'ford':
                    d.resolve(['1999', '2000'])
                    break;
                case 'acura':
                    d.resolve(['2001', '2002'])
                    break;
                case 'chevy':
                    d.resolve(['2001', '2002'])
                    break;
            }
            return d.promise;
        }


        return f;

    }
    ])
})();