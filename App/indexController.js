﻿angular.module("car-finder").controller("indexController", ['testCarSvc', function (testCarSvc) {
    var self = this;

    self.perPages = 50;

    self.changeperPage = function () {
        if (self.perPages > 200)
        {
            self.perPages = 200;
        }
        self.selected.perPage = self.perPages;
    }

    self.yearchecked = "";
    self.makechecked = "";

    self.selected = {
        year: "",
        make: "",
        model: "",
        trim: "",
        paging: true,
        page: 0,
        perPage: ""
    }

    self.options = {
        years: "",
        makes: "",
        models: "",
        trims: ""
    }

    self.Carcount = "";

    self.cars = [];
    self.printstring = "";

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
            self.printstring = "Please select a year"
            self.makechecked = "";
            self.getYears();
        }
        if(make)
        {
            self.printstring = "Please select a make"
            self.yearchecked = "";
            self.getMakes1();
        }
    }

    self.getYears = function () {
        testCarSvc.getYears().then(function (data) {
            self.options.years = data;
        });
        self.getCars();
        self.getCount();
    }

    self.getMakes1 = function () {
        testCarSvc.getMakes1().then(function (data) {
            self.options.makes = data;
        });
        self.getCars();
        self.getCount();
    }

    self.getYears1 = function () {
        self.selected.year = "";
        self.options.years = "";
        self.selected.model = "";
        self.options.models = "";
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        self.printstring = "Please select a year"
        testCarSvc.getYears1(self.selected).then(function (data) {
            self.options.years = data;
        });
        self.getCars();
        self.getCount();
    }

    self.getMakes = function () {
        self.selected.make = "";
        self.options.makes = "";
        self.selected.model = "";
        self.options.models = "";
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        self.printstring = "Please select a make"
        testCarSvc.getMakes(self.selected).then(function (data) {
            self.options.makes = data;
        })
        self.getCars();
        self.getCount();
    }

    self.getModels = function () {
        self.selected.model = "";
        self.options.models = "";
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        self.printstring = "Please select a model"
        testCarSvc.getModels(self.selected).then(function (data) {
            self.options.models = data;
        })
        self.getCars();
        self.getCount();
    }

    self.getTrims = function () {
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        self.printstring = "Please select a trim"
        testCarSvc.getTrims(self.selected).then(function (data) {
            self.options.trims = data;
        })
        self.getCount();
    }

    self.getCars = function () {
        self.cars = [];
        var s = angular.copy(self.selected);
        s.page += 1;
        testCarSvc.getCars(s).then(function (data)
        {
            self.cars = data;
        })
    }

    self.getCount = function () {
        testCarSvc.getCount(self.selected).then(function (data) {
            self.Carcount = data;
        })
    }

    self.getAllinfo = function () {
        self.getCars();
        self.getCount();
    }
}]);