interface Employee {
    firstName: string;
    lastName: string;
    fullName(): string;
}

let employee: Employee = {
    firstName : "Emil",
    lastName: "Andersson",
    fullName(): string {
        return this.firstName + " " + this.lastName;
    }
}

// employee.firstName = 10;  //* Error - Type 'number' is not assignable to type 'string'

employee.firstName = "Whatever";

console.log(employee.firstName);