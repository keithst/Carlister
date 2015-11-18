﻿angular.module("car-finder").controller("indexController", ['$interval', 'testCarSvc', function ($interval, testCarSvc) {
    var self = this;

    self.yearchecked = "";
    self.makechecked = "";

    self.stopwatch = 0;
    self.clock = Date.now();

    var updateClock = function () {
        if (!self.pause)
        {
            self.clock = Date.now();
        }
    }

    self.names = ["Bob", "Dan", "Ray"];
    self.index = 0;
    self.name = self.names[self.index];

    var updateName = function () {
        if (!self.pause) {
            self.index++;
            if (self.index >= self.names.length) {
                self.index = 0;
            }
            self.name = self.names[self.index];
        }
    }

    self.pause = false;
    self.pausewatch = true;

    self.pauseit = function () {
        if(self.pause)
        {
            self.pause = false;
        }
        else
        {
            self.pause = true;
        }
    }

    self.pauseitwatch = function () {
        if (self.pausewatch) {
            self.pausewatch = false;
        }
        else {
            self.pausewatch = true;
        }
    }

    var updatestopwatch = function () {
        if(!self.pausewatch)
        {
            self.stopwatch += 10;
        }
    }

    self.selected = {
        year: "",
        make: "",
        model: "",
        trim: ""
    }

    self.options = {
        years: "",
        makes: "",
        models: "",
        trims: ""
    }

    self.cars = [];

    self.DataIni = function (year, make) {
        self.selected.year = "";
        self.options.years = "";
        self.selected.make = "";
        self.options.makes = "";
        self.selected.model = "";
        self.options.models = "";
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        if(year)
        {
            self.getYears();
            self.makechecked = "";
        }
        if(make)
        {
            self.getMakes1();
            self.yearchecked = "";
        }
    }

    self.getYears = function () {
        testCarSvc.getYears().then(function (data) {
            self.options.years = data;
        });
    }

    self.getMakes1 = function () {
        testCarSvc.getMakes1().then(function (data) {
            self.options.makes = data;
        });
    }

    self.getYears1 = function () {
        self.selected.year = "";
        self.options.years = "";
        self.selected.model = "";
        self.options.models = "";
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        testCarSvc.getYears1(self.selected).then(function (data) {
            self.options.years = data;
        });
    }

    self.getMakes = function () {
        self.selected.make = "";
        self.options.makes = "";
        self.selected.model = "";
        self.options.models = "";
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];

        testCarSvc.getMakes(self.selected).then(function (data) {
            self.options.makes = data;
        })
        self.getCars();
    }

    self.getModels = function () {
        self.selected.model = "";
        self.options.models = "";
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];

        testCarSvc.getModels(self.selected).then(function (data) {
            self.options.models = data;
        })
        self.getCars();
    }

    self.getTrims = function () {
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];

        testCarSvc.getTrims(self.selected).then(function (data) {
            self.options.trims = data;
        })
        self.getCars();
    }
    self.getCars = function () {
        self.cars = [];

        testCarSvc.getCars(self.selected).then(function (data)
        {
            self.cars = data;
        })
    }
    $interval(updatestopwatch, 10);
    $interval(updateName, 1000);
    $interval(updateClock, 1000);
}]);