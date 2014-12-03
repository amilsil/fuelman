﻿'use strict';

var ngChartist = angular.module('ngChartist', []);

var bindEvents = function (chart, events) {
    Object.keys(events).forEach(function (eventName) {
        chart.on(eventName, events[eventName]);
    });
};

ngChartist.directive('chartist', [
    function () {
        return {
            restrict: 'EA',
            scope: {
                // mandatory
                data: '&chartistData',
                chartType: '@chartistChartType',
                // optional
                events: '&chartistEvents',
                chartOptions: '&chartistChartOptions',
                responsiveOptions: '&chartistResponsiveOptions',

                height: '@height'
            },
            link: function (scope, element, attrs) {
                var data = scope.data();
                var type = scope.chartType;

                var events = scope.events() || {};
                var options = scope.chartOptions() || null;
                var responsiveOptions = scope.responsiveOptions() || null;

                var chart = Chartist[type](element[0], data, options, responsiveOptions);

                bindEvents(chart, events);

                // Deeply watch the data and create a new chart if data is updated
                scope.$watch(scope.data, function (newData, oldData) {
                    // Avoid initializing the chart twice,
                    // fix 'TypeError: Cannot read property 'removeMediaQueryListeners' of undefined' as well
                    if (newData !== oldData) {
                        chart.detach();
                        chart = Chartist[type](element[0], newData, options, responsiveOptions);
                        bindEvents(chart, events);
                    }
                }, true);

                scope.$watch(scope.height, function (newHeight, oldHeight) {                    
                    element.css('pixel-height', newHeight + "px");
                }, true);
            }
        };
    }
]);