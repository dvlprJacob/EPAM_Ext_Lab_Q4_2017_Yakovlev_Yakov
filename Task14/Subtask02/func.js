/**
 * @author Lenovo
 */
function calcExec() {

	var expr = document.getElementById("textIn").value.replace(/\s+/g, '');
	var separators = " ";
	var opRegex = /[-+*/=]/;
	var numRegex = /\d+(?:\.\d+)?/;

	var numbers = expr.split(opRegex).clean("");
	var operators = expr.split(numRegex).clean("");

	console.log("exprLen : " + expr.length);
	console.log("expr : " + expr);

	console.log("numLen : " + numbers.length);
	console.log("numbers : " + numbers);

	console.log("operLen : " + operators.length);
	console.log("opers : " + operators);

	var result = calc(numbers, operators);
	console.log(result);
	document.getElementById("res").innerHTML = result;
};

function calc(numbers, operators) {

	var result = parseFloat(numbers[0]);

	if (!(Array.isArray(numbers) && Array.isArray(operators))) {
		return "Incorrect input"
	}

	for (var i = 0; i < numbers.length; i++) {
		if (isNaN(parseFloat(numbers[i]))) {
			return "Incorrect input : " + numbers[i + 1];
		}
	}

	for (var i = 0; i < operators.length; i++) {

		switch(operators[i]) {
		case "+":
			result += parseFloat(numbers[i + 1]);
			break;

		case "-":
			result -= parseFloat(numbers[i + 1]);
			break;

		case "*":
			result *= parseFloat(numbers[i + 1]);
			break;

		case "/":
			result /= parseFloat(numbers[i + 1]);
			break;

		case "=":
			return parseFloat(result).toFixed(2);

		default:
			return "Incorrect input oper : " + operators[i];
		}
	}

	return parseFloat(result).toFixed(2);
};

Array.prototype.clean = function(deleteValue) {
	for (var i = 0; i < this.length; i++) {
		if (this[i] == deleteValue) {
			this.splice(i, 1);
			i--;
		}
	}
	return this;
};
