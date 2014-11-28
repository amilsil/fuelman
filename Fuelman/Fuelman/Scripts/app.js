var app = angular.module("fuelman", []);

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
                alert(data);
            });

        vehicles.push(v);
        return v.id;
    };

    this.addRefill = function (r) {
        $http.post('/api/refill?vehicleId=' + r.VehicleId, JSON.stringify(r))
            .success(function (data, status, headers, config) {
                alert(data);
            });

        //refills.push(r);
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

    this.getStatistics = function (vehicleId) {
        statistics.length = 0; // clear the statistics.
        $http.get('/api/stat/' + vehicleId)
            .success(function (data, status, headers, config) {                
                $.each(data, function () {                    
                    statistics.push(this);
                });
            })
            .error(function (data, status, headers, config) {
                // what to do if no data?
            });

        return statistics;
    };

}]);


/* Vehicle Controller */
app.controller("VehicleController", ["$scope", "$filter", "VehicleService", function ($scope, $filter, VehicleService) {
    $scope.vehicles = [];
    $scope.brands = []; 
    $scope.models = [];
    $scope.refills = [];
    $scope.refillUnits = [];
    $scope.statistics = [];

    $scope.selectedVehicle = null;
    $scope.selectedVehicleId = null;

    // form data
    $scope.newVehicle = {};
    $scope.newRefill = {};

    // bools.
    $scope.isCreateNewVehicle = false;
    $scope.isCreateNewRefill = false;    

    function init() {
        $scope.vehicles = VehicleService.getVehicles(function () {
            if ($scope.vehicles.length > 0)
                $scope.selectedVehicle = $scope.vehicles[0];
        });

        $scope.brands = VehicleService.getBrands();
        $scope.refillUnits = VehicleService.getRefillUnits();
    }

    // logic for creating and cancelling creation.
    $scope.startCreateNewRefill = function () { $scope.isCreateNewRefill = true; };
    $scope.cancelCreateNewRefill = function () { $scope.isCreateNewRefill = false; };
    $scope.createNewVehicle = function() { $scope.isCreateNewVehicle = true; };
    $scope.cancelCreateNewVehicle = function() { $scope.isCreateNewVehicle = false; }
  
   $scope.getVehicleTitle = function () {
        if ($scope.selectedVehicle) {
            return $scope.selectedVehicle.name;
        }
    };

    // Saving logic
    $scope.saveRefill = function () {
        var refill = {
            RefillDate: $filter('date')(new Date($scope.newRefill.RefillDate), "yyyy-MM-ddTHH:mm:ss"),
            Odometer:       $scope.newRefill.Odometer,
            RefillAmount:   $scope.newRefill.RefillAmount,
            IsFullTank:     $scope.newRefill.IsFullTank,
            VehicleId:      $scope.selectedVehicle.Id
        };

        VehicleService.addRefill(refill);

        $scope.newRefill = {};
        $scope.isCreateNewRefill = false;
    };

    $scope.saveVehicle = function () {
        var vehicle = {
            Name: $scope.newVehicle.Name,
            BrandId: $scope.newVehicle.Brand.Id,
            ModelId: $scope.newVehicle.Model.Id,
            BrandName:$scope.newVehicle.Brand.BrandName,
            ModelName: $scope.newVehicle.Model.ModelName,
            RefillUnitId: $scope.newVehicle.RefillUnit.Id
        };
      
        VehicleService.addVehicle(vehicle);
        
        $scope.newVehicle = {};
        $scope.isCreateNewVehicle = false;
        $scope.selectedVehicle = vehicle;
    };

    $scope.selectThisVehicle = function (vehicle) {
        $scope.selectedVehicle = vehicle;
    };

    // When the selected vehicle changes..
    $scope.$watch('selectedVehicle', function (newValue, oldValue) {
        if (newValue != undefined && newValue.Id != undefined) {
            // Reset and load the dependent arrays.
            $scope.statistics.length = 0;
            $scope.statistics = VehicleService.getStatistics(newValue.Id);

            $scope.refills.length = 0;
            $scope.refills = VehicleService.getRefills(newValue.Id);

            $scope.selectedVehicleId = $scope.vehicles.indexOf(newValue);
        }
    });

    $scope.$watch('selectedVehicleId', function (newValue, oldValue) {
        if (newValue != undefined ) {
            $scope.selectedVehicle = $scope.vehicles[newValue];            
        }
    });

    // When the brand changes..
    $scope.$watch('newVehicle.Brand', function (newValue, oldValue) {
        if (newValue != undefined && newValue.Id != undefined) {
            $scope.models.length = 0;
            $scope.models = VehicleService.getModels(newValue.Id);
        }
    });

    // Css
    $scope.cssVehicleListItem = function (vehicle) {
        if ($scope.selectedVehicle === vehicle)
            return "active";
        else
            return "";
    };

    init();
}]);