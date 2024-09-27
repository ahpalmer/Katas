export function filter_list(l:Array<any>):Array<number> {
    let answer: Array<number> = [];
    l.forEach(individualNum => {
        if(typeof individualNum === 'number') {
            answer.push(individualNum)
        };
    });
    return answer;
}

export function getSum(a: number, b: number): number {
    let difference: number = b - a
    if (difference === 0) {
        return a;
    }
    else if (difference < 0) {
        difference = difference * -1
    }
    if (a > b) {
        let c: number = b;
        b = a;
        a = c;
    }
    let total: number = a;
    for (let i = 1; i <= difference; i++)
    {
        total = total + a + i;
    }
    return total;
}

const a = 5
const b = 1
console.log(getSum(a, b));

// const input = [1,2,'aasf','1','123',123];
// console.log(filter_list(input));