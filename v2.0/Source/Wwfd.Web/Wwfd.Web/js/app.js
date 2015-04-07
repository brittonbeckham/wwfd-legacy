var app = angular.module('wwfdApp', ['ngRoute']);

app.config([
	'$routeProvider', function ($routeProvider) {
		$routeProvider.when('/founder/:founderId',
			{ templateUrl: 'views/founder.html' });
	}
]);

app.controller('appController', ['dataService', '$scope', function (wwfdData, $scope) {

	init();

	function init() {
		wwfdData.getAllFounders(loadFounders);
	}

	$scope.clearFounderSearch = function ($event) {

		if ($event.keyCode === 27) {
			$scope.founderSearchText = "";
			wwfdData.getAllFounders(loadFounders);
		}
	}

	$scope.searchFounders = function () {
		wwfdData.searchFounders($scope.founderSearchText, loadFounders);
	}
	
	function loadFounders(data) {
		$scope.founders = data;
	}

	return {};
}]);

app.controller('founderController', ['dataService', '$scope', '$routeParams', function (wwfdData, $scope, $routeParams) {

	init();

	function init() {
		wwfdData.getFounderById($routeParams.founderId, loadFounder);
		wwfdData.getFounderQuotesById($routeParams.founderId, loadQuotes);
	}

	function loadFounder(data) {
		$scope.founder = data;
	}

	function loadQuotes(data) {
		$scope.quotes = data;

		for (var i = 0; i < $scope.quotes.length; i++)
			$scope.quotes[i].Keywords = $scope.quotes[i].Keywords.split(', ');
	}


	return {};
}]);


app.service('dataService', ['$http', function ($http) {

	var baseurl = "http://localhost:30000/";

	function get(url, callback, error) {
		$http.get(baseurl + url)
			.success(function (data) {
				callback(data);
			})
			.error(function (data) {
				error(data);
			});
	}

	return {
		getAllFounders: function (callback) {
			get('founder/all/quotecount', callback);
		},
		searchFounders: function (searchString, callback) {
			get('founder/search/' + searchString + '/quotecount', callback);
		},
		getFounderById: function (id, callback) {
			get('founder/' + id, callback);
		},
		getFounderQuotesById: function (id, callback) {
			get('quote/founder/' + id, callback);
		}
	}

}]);
