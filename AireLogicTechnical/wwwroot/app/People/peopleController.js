app.controller("peopleCtrl", function ($scope, peopleService) {

    $scope.people = new Array();
    $scope.selectedPerson = {};
    $scope.showList = true;

    $scope.showAuth = true;
    $scope.showEnabled = true;

    $scope.showRed = true;
    $scope.showBlue = true;
    $scope.showGreen = true;

    $scope.getAllPeople = function () {
        peopleService.GetAllPeople()
            .then(function (data) {

                $scope.people = data;

                for (var i = 0; i < $scope.people.length; i++) {
                    var fullName = $scope.people[i].firstName + $scope.people[i].lastName;
                    $scope.people[i].palindrome = isPalindrome(fullName);
                }

            }, function () {
                alert('Error getting all people');
            });
    };

    $scope.viewPerson = function (id) {
        peopleService.GetPerson(id)
            .then(function (data) {

                $scope.selectedPerson = data;

                $scope.selectedPerson.red = checkArrayContainsPropertyValue(data.favouriteColours, "colourId", 1);
                $scope.selectedPerson.green = checkArrayContainsPropertyValue(data.favouriteColours, "colourId", 2);
                $scope.selectedPerson.blue = checkArrayContainsPropertyValue(data.favouriteColours, "colourId", 3);

                $scope.showList = false;

            }, function () {
                alert('Error viewing person');
            });
    };

    $scope.savePerson = function () {

        //convert red,green,blue properties to list equivalent
        $scope.selectedPerson.favouriteColours = new Array();

        if ($scope.selectedPerson.red)
        {
            $scope.selectedPerson.favouriteColours.push(
            {
                colourId: 1,
                personId: $scope.selectedPerson.personId
            });
        }

        if ($scope.selectedPerson.green)
        {
            $scope.selectedPerson.favouriteColours.push(
            {
                colourId: 2,
                personId: $scope.selectedPerson.personId
            });
        }

        if ($scope.selectedPerson.blue)
        {
            $scope.selectedPerson.favouriteColours.push(
            {
                colourId: 3,
                personId: $scope.selectedPerson.personId
            });
        }

        peopleService.UpdatePerson($scope.selectedPerson)
            .then(function (data) {

                var updatedPerson = data;
                
                var updateIndex = $scope.people.findIndex(x => x.personId === updatedPerson.personId);

                var fullName = updatedPerson.firstName + updatedPerson.lastName;
                updatedPerson.palindrome = isPalindrome(fullName);

                $scope.people[updateIndex] = updatedPerson;

                //show again
                $scope.showList = true;

            }, function () {
                alert('Error saving person');
            });

    };

    $scope.switchAuth = function () {
        $scope.showAuth = !$scope.showAuth;
    };

    $scope.switchEnabled = function () {
        $scope.showEnabled = !$scope.showEnabled;
    };

    $scope.switchBlue = function () {
        $scope.showBlue = !$scope.showBlue;
    };

    $scope.switchRed = function () {
        $scope.showRed = !$scope.showRed;
    };

    $scope.switchGreen = function () {
        $scope.showGreen = !$scope.showGreen;
    };

    $scope.goBack = function (id) {
        $scope.showList = true;
    };

    $scope.getAllPeople();
});