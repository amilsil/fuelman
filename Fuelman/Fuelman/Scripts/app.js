var app = angular.module('root', []);

/* Vehicle service to get the vehicles */
app.service("VehicleService", [function () {
    var vehicles = [
            { id: 1, name: 'MyToyota', brand: 'Toyota', model: 'Vios', year: '1997'},
            { id: 2, name: 'MyVitz', brand: 'Toyota', model: 'Vitz', year: '2007' }
    ];

    var count = vehicles.length;

    this.getVehicles = function () {
        return vehicles;
    };

    this.addVehicle = function (v) {
        v.id = ++count;
        vehicles.push(v);
    };

    this.getBrands = function () {
        return [
            { id: 1, name: 'Toyota' },
            { id: 2, name: 'Nissan' }
        ];
    };

    this.getModels = function () {
        return [
            { id: 1, name: 'Vitz' },
            { id: 2, name: 'Vios' },
            { id: 3, name: 'FB15' },
            { id: 4, name: 'Vezel' },
            { id: 5, name: 'Fit' }
        ];
    };

}]);

/* Vehicle Controller */
app.controller("VehicleController", ["$scope", "VehicleService", function ($scope, VehicleService) {
    $scope.vehicles = [];
    $scope.brands = [];
    $scope.models = [];

    $scope.selectedVehicle = null;
    $scope.newVehicle = {};

    init();

    function init() {
        $scope.vehicles = VehicleService.getVehicles();
        $scope.selectedVehicle = $scope.vehicles[0];

        $scope.brands = VehicleService.getBrands();
        $scope.models = VehicleService.getModels();
    }

    $scope.getVehicleTitle = function () {
        if ($scope.selectedVehicle) {
            return $scope.selectedVehicle.name;
        }
    };

    $scope.save = function () {
        VehicleService.addVehicle($scope.newVehicle);
    };
}]);