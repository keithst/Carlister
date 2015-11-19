angular.module("car-finder").controller("indexController", ['$q', '$uibModal', 'testCarSvc', function ($q, $uibModal, testCarSvc) {
    var self = this;

    self.perPages = 50;
    self.loading = false;

    self.changeperPage = function () {
        if (self.perPages > 200)
        {
            self.perPages = 200;
        }
        if (self.perPages < 1)
        {
            self.perPages = 1;
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
            self.makechecked = "";
            self.getYears();
        }
        if(make)
        {
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
        testCarSvc.getYears1(self.selected).then(function (data) {
            self.options.years = data;
        });
        self.getCars1();
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
        self.getCars1();
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
        self.getCars1();
    }

    self.getTrims = function () {
        self.selected.trim = "";
        self.options.trims = "";
        self.cars = [];
        testCarSvc.getTrims(self.selected).then(function (data) {
            self.options.trims = data;
        })
        self.getCars1();
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

    self.getCars1 = function () {
        if (!self.loading) {
            self.loading = true;
            var s = angular.copy(self.selected);
            s.page += 1;
            self.cars = [];

            $q.all([testCarSvc.getCars(s), testCarSvc.getCount(s)])
                .then(function (data) {
                console.log(data);
                self.cars = data[0];
                self.Carcount = data[1];
                self.loading = false;
            });
        }
    }

    self.getCount = function () {
        testCarSvc.getCount(self.selected).then(function (data) {
            self.Carcount = data;
        })
    }

    self.getAllinfo = function () {
        if ((self.yearchecked && self.selected.year != "") || (self.makechecked && self.selected.make != ""))
        {
            self.getCars1();
        }
    }

    self.open = function (id) {
        console.log("Id in open " + id);
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: 'carModal.html',
            controller: 'carModalCtrl as cm',
            size: 'lg',
            resolve: {
                car: function () {
                    return testCarSvc.getDetails(id);
                }
            }
        })
    }
}]);

angular.module("car-finder").controller('carModalCtrl', function ($modalInstance, car) {

    var scope = this;

    scope.car = car;

    scope.ok = function () {
        $modalInstance.close();
    };

    scope.cancel = function () {
        $modalInstance.dismiss();
    };
});