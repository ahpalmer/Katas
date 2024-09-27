"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.filter_list = filter_list;
exports.getSum = getSum;
function filter_list(l) {
    var answer = [];
    l.forEach(function (individualNum) {
        if (typeof individualNum === 'number') {
            answer.push(individualNum);
        }
        ;
    });
    return answer;
}
function getSum(a, b) {
    var difference = b - a;
    console.log(difference);
    if (difference === 0) {
        return a;
    }
    else if (difference < 0) {
        difference = difference * -1;
    }
    console.log(difference);
    if (a > b) {
        var c = b;
        b = a;
        a = c;
    }
    var total = a;
    console.log(total);
    for (var i = 1; i <= difference; i++) {
        total = total + a + i;
        console.log(total);
    }
    return total;
}
var a = 5;
var b = 1;
console.log(getSum(a, b));
// const input = [1,2,'aasf','1','123',123];
// console.log(filter_list(input));
