var myApp = angular.module("myApp", ['dirPagination']);
myApp.controller("mainController", function ($scope, $http) {
    $scope.EmpBasicInfoVM = {};
    $scope.model = {};

    function GetAll() {
        $http({
            method: "GET",
            url: "EmpBasicInfo/GetAllEmployee"
        }).then(
            function (response) {
                $scope.EmpBasicInfoVM = response.data;
            },
            function () {
                alert("Data Load Failed.");
                console.log(response.data);
            });
    }

    $scope.btnSubmit = "Create";
    function Create(model) {
        var json = model;
        $http({
            method: "POST",
            url: "EmpBasicInfo/AddEmployee",
            data: json
        }).then(
            function (response) {
                $scope.model = {};
                GetAll();
                alert("Data add successful");
            },
            function () {
                alert("Fail to add Employee");
                console.log(response.data);
            });
    }
    $scope.btnUpdate = function (ps) {
        var editData = { "id": ps };
        $http({
            method: "GET",
            url: "EmpBasicInfo/GetEmployeeByID" + "/" + ps.EmpID
        }).then(
            function (response) { $scope.model = response.data; },
            function () { alert("Failed."); }
        );
        $scope.btnSubmit = "Update";
    };

    function Update(model) {
        var json = model;
        $http({
            method: "POST",
            url: "EmpBasicInfo/EditEmp",
            data: json
        }).then(
            function (response) {
                $scope.model = {};
                GetAll();
                $scope.btnSubmit = "Create";
                alert("Data update successful");
            },
            function () {
                alert("Failed.");
                console.log(response.data);
            });
    }
    $scope.btnDelete = Delete;
    GetAll();
    function Delete(model) {
        var json = model;
        var c = confirm("Are you sure to delete this record?");
        if (c == true) {
            $http({
                method: "POST",
                url: "EmpBasicInfo/DeleteEmp",
                data: json
            }).then(
                function (response) {
                    GetAll();
                    alert("Data delete successful");
                },
                function () {
                    alert("Failed.");
                    //console.log(response.data);
                });
        }
    }

    // Employee Bulk Deletation

    $scope.toggleAll = function () {
        for (var i = 0; i < $scope.EmpBasicInfoVM.length; i++) {
            $scope.EmpBasicInfoVM[i].Selected = $scope.checkAll;
        }
    };

    $scope.toggleOne = function () {
        $scope.checkAll = true;
        for (var i = 0; i < $scope.EmpBasicInfoVM.length; i++) {
            if (!$scope.EmpBasicInfoVM[i].Selected) {
                $scope.checkAll = false;
                break;
            }
        };
    };

    $scope.btnBulkDelete = BulkDelete;
    function BulkDelete() {

        var checkedEmpId = [];
        for (var i = 0; i < $scope.EmpBasicInfoVM.length; i++) {
            if ($scope.EmpBasicInfoVM[i].Selected) {
                checkedEmpId.push($scope.EmpBasicInfoVM[i]);
            }
        }

        var jsonData = checkedEmpId;
        var c = confirm("Are you sure to delete this record?");
        if (c == true) {
            $http({
                method: "POST",
                url: "EmpBasicInfo/BulkDeleteEmp",
                data: jsonData
            }).then(
                function (response) {
                    $scope.checkAll = false;
                    alert("Data dekete successful");
                    GetAll();                   
                },
                function () {
                    alert("Delete Failed.");
                    //console.log(response.data);
                });
        }
    }

    $scope.Click = function (model) {
        if ($("#btnSubmit").val() == "Create") {
            Create(model);
        }
        if ($("#btnSubmit").val() == "Update") {
            Update(model);
        }
    }
});