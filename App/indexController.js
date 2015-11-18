angular.module("car-finder").controller("indexController", ['$interval', 'testCarSvc', function ($interval, testCarSvc) {
    var self = this;

    self.yearchecked = "";
    self.makechecked = "";

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
        self.printstring = "Please select a year"
        testCarSvc.getYears1(self.selected).then(function (data) {
            self.options.years = data;
        });
        self.getCars();
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
    }

    self.getTrims = function () {
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        self.printstring = "Please select a trim"
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
}]);