/**
 * @author Lenovo
 */
function removeDuplicateChar() {
	var text = document.getElementById("textIn").value;
	var ignore = ["?", "!", ":", ";", ",", ".", " ", "\t", "\r"];
	var letters = {}, result;
	var words = text.split(' ');

	words.forEach(function(word) {
		word.split('').forEach(function(letter, i) {
			if (!letters[letter] && ignore.indexOf(letter) == -1 && -1 != word.indexOf(letter, i + 1)) {
				letters[letter] = 1;
			}
		});
	});

	result = text.split('').filter(function(v) {
		return !letters[v];
	}).join('');

	console.log(result);
	document.getElementById("res").innerHTML = result;
}
