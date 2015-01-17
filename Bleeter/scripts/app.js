/// <reference path="http://ajax.googleapis.com/ajax/libs/angularjs/1.3.8/angular.js" />

var oauth_token = localStorage.getItem('token');

var qs = { key: oauth_token };

var bleeterApp = angular.module('bleeter', ['controllers']);

var controllers = angular.module('controllers', []);

controllers.controller('BleetController', function ($scope, $http) {
    $http.get('user/feed?key=' + oauth_token).success(function (data) {
        $scope.bleets = data;
        console.log($scope.bleets);
    });

    var connection = $.connection("/realtime", qs);

    connection.received(function (data) {
        $scope.bleets.push(data);
        $scope.$apply();
        console.log($scope.bleets);
    });

    connection.start().done(function () {
        console.log("connected");
    });
});

controllers.controller('UserDetailsController', function ($scope, $http) {
    $http.get('user/details?key=' + oauth_token).success(function (data) {
        console.log('retrieved details');
        $scope.following = data.FollowingCount;
        $scope.followers = data.FollowersCount;
        $scope.bleets = data.BleetsCount;
        $scope.username = data.Username;
        $scope.displayName = data.DisplayName;
    });
});

controllers.controller('PostBleetController', function ($scope, $http) {

});