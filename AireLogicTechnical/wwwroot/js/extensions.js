var checkArrayContainsPropertyValue = function (array, property, value) {

    for (var i = 0; i < array.length; i++) {
        if (array[i][property] === value) {
            return true;
        }
    }

    return false;
};

function isPalindrome(str) {
    var lowerStr = str.toLowerCase();
    var reversedLower = lowerStr.split("").reverse().join("");
    return lowerStr === reversedLower;
};