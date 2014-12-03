var app = angular.module("fuelman", ['ngChartist']);

app.service("VehicleService", ["$http", function ($http) {
    // arrays to hold data.
    var vehicles = [];
    var brands = [];
    var models = [];
    var refills = [];
    var refillUnits = [];
    var statistics = [];

    var count = vehicles.length;

    // Adders..
    this.addVehicle = function (v) {
        $http.post('/api/vehicle', JSON.stringify(v))
            .success(function (data, status, headers, config) {
                vehicles.push(data);
            });
    };

    this.addRefill = function (r) {
        $http.post('/api/refill?vehicleId=' + r.VehicleId, JSON.stringify(r))
            .success(function (data, status, headers, config) {
                refills.push(data);
            });
    };

    // Getters..
    this.getVehicles = function (callOnSuccess) {

        $http.get('/api/vehicle')
            .success(function (data, status, headers, config) {
                $.each(data, function () {
                    vehicles.push(this);
                });
                callOnSuccess();
            })
            .error(function (data, status, headers, config) {
                // what to do if no data?
            });

        return vehicles;
    };
    
    this.getBrands = function () {
        $http.get('/api/brand')
            .success(function (data, status, headers, config) {
                $.each(data, function () {
                    brands.push(this);
                });
            })
            .error(function (data, status, headers, config) {
                // what to do if no data?
            });

        return brands;
    };

    this.getModels = function (brandId) {
        $http.get('/api/model?brand=' + brandId)
            .success(function (data, status, headers, config) {
                $.each(data, function () {
                    models.push(this);
                });
            })
            .error(function (data, status, headers, config) {
                // what to do if no data?
            });

        return models;
    };

    this.getRefills = function (vehicleId) {
        $http.get('/api/refill?vehicleId=' + vehicleId)
            .success(function (data, status, headers, config) {
                $.each(data, function () {
                    refills.push(this);
                });
            })
            .error(function (data, status, headers, config) {
                // what to do if no data?
            });

        return refills;
    };

    this.getRefillUnits = function () {
        $http.get('/api/refillunit')
            .success(function (data, status, headers, config) {
                $.each(data, function () {
                    refillUnits.push(this);
                });
            })
            .error(function (data, status, headers, config) {
                // what to do if no data?
            });

        return refillUnits;
    };

    this.getStatistics = function (vehicleId, callBack) {
        statistics.length = 0; // clear the statistics.
        $http.get('/api/stat/' + vehicleId)
            .success(function (data, status, headers, config) {                
                $.each(data, function () {                    
                    statistics.push(this);
                });
                callBack();
            })
            .error(function (data, status, headers, config) {
                // what to do if no data?
            });

        return statistics;
    };

}]);

