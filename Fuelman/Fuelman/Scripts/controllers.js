
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
    // vehicle
    $scope.nv_name = "";
    $scope.nv_brand = "";
    $scope.nv_model = "";
    $scope.nv_refillunit = "";
    // refill
    $scope.nr_refilldate = new Date();
    $scope.nr_odometer = "";
    $scope.nr_refillamount = "";
    $scope.nr_isfulltank = true;

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

    function resetRefillForm() {
        $scope.nr_refilldate = new Date();
        $scope.nr_odometer = "";
        $scope.nr_refillamount = "";
        $scope.nr_isfulltank = true;
    }

    function resetVehicleForm() {
        $scope.nv_name = "";
        $scope.nv_brand = "";
        $scope.nv_model = "";
        $scope.nv_refillunit = "";
    }

    // logic for creating and cancelling creation.
    $scope.startCreateNewRefill = function () { $scope.isCreateNewRefill = true; };
    $scope.cancelCreateNewRefill = function () { $scope.isCreateNewRefill = false; };
    $scope.createNewVehicle = function () { $scope.isCreateNewVehicle = true; };
    $scope.cancelCreateNewVehicle = function () { $scope.isCreateNewVehicle = false; }

    $scope.getVehicleTitle = function () {
        if ($scope.selectedVehicle) {
            return $scope.selectedVehicle.name;
        }
    };

    // Saving logic
    $scope.saveRefill = function () {

        //if valid, save.
        if ($("#vehicle_form").valid() === true) {
            var refill = {
                RefillDate: $filter('date')(new Date($scope.nr_refilldate), "yyyy-MM-ddTHH:mm:ss"),
                Odometer: $scope.nr_odometer,
                RefillAmount: $scope.nr_refillamount,
                IsFullTank: $scope.nr_isfulltank? 1: 0,
                VehicleId: $scope.selectedVehicle.Id
            };

            VehicleService.addRefill(refill);

            //reset the form values.
            resetRefillForm();
            // hide the form
            $scope.isCreateNewRefill = false;
        }
    };

    $scope.saveVehicle = function () {

        //if valid, save.
        if ($("#vehicle_form").valid() === true) {
            var vehicle = {
                Name: $scope.nv_name,
                BrandId: $scope.nv_brand.Id,
                ModelId: $scope.nv_model.Id,
                BrandName: $scope.nv_brand.BrandName,
                ModelName: $scope.nv_model.ModelName,
                RefillUnitId: $scope.nv_refillunit.Id
            };

            VehicleService.addVehicle(vehicle);

            // reset the form values.
            resetVehicleForm();

            $scope.isCreateNewVehicle = false;
            $scope.selectedVehicle = vehicle;
        }
    };

    $scope.selectThisVehicle = function (vehicle) {
        $scope.selectedVehicle = vehicle;
    };

    // When the selected vehicle changes..
    $scope.$watch('selectedVehicle', function (newValue, oldValue) {
        if (newValue != undefined && newValue.Id != undefined) {
            // Reset and load the dependent arrays.
            $scope.statistics.length = 0;
            $scope.statistics = VehicleService.getStatistics(newValue.Id, function () { $scope.statChanged(); });

            $scope.refills.length = 0;
            $scope.refills = VehicleService.getRefills(newValue.Id);

            $scope.selectedVehicleId = $scope.vehicles.indexOf(newValue);
        }
    });

    $scope.$watch('refills', function (newValue, oldValue) {
        $scope.statistics.length = 0;
        $scope.statistics = VehicleService.getStatistics(newValue.Id, function () { $scope.statChanged(); });
    });

    $scope.$watch('selectedVehicleId', function (newValue, oldValue) {
        if (newValue != undefined) {
            $scope.selectedVehicle = $scope.vehicles[newValue];
        }
    });

    // When the brand changes..
    $scope.$watch('nv_brand', function (newValue, oldValue) {
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

    // Statistics changed. Update the chart. 
    $scope.statChanged = function () {
        var data = $scope.chartist.barData;
        data.labels = [];
        data.series = [[]];
        var format = "d/MMM";

        for (stat in $scope.statistics) {
            var date = $filter('date')(new Date($scope.statistics[stat].Date), format)
            data.labels.push(date);
            data.series[0].push($scope.statistics[stat].Economy);
        }
    };

    $scope.chartist = {
        barData: {
            labels: ['Week1', 'Week2', 'Week3', 'Week4', 'Week5', 'Week6'],
            series: [
              [5, 4, 3, 7, 5, 10]
            ]
        }
    }
}]);