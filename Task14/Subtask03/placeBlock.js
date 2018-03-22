function init(selBlockId) {
	var myselect = document.getElementById(selBlockId);
	var items = ["Option 1", "Option 2", "Option 3", "Option 4", "Option 5"];
	var selector = '.selectOption#' + selBlockId;

	$.each(items, function() {
		$('<option/>', {
			val : this,
			text : this,
		}).appendTo(myselect);
	});
	$(selector).append(myselect);
}

function move(selBlockId, selOp) {
	if (selOp.length == 0) {
		return;
	}

	var myselect = document.getElementById(selBlockId);
	var selector = '.selectOption#' + selBlockId;

	var idOp = 'op';
	$.each(selOp, function() {
		$('<option/>', {
			val : this,
			text : this,
		}).appendTo(myselect);
	});
	$(selector).append(myselect);
}

function allToRight(selectFromId, selectToId) {
	var selectorFrom = '.selectOption#' + selectFromId;
	var items = $(selectorFrom).children('option').map(function() {
		return this.value;
	}).get();
	move(selectToId, items);
	$(selectorFrom).empty();
}

function allToLeft(selectFromId, selectToId) {
	var selectorFrom = '.selectOption#' + selectFromId;
	var items = $(selectorFrom).children('option').map(function() {
		return this.value;
	}).get();
	move(selectToId, items);
	$(selectorFrom).empty();
}

function selToRight(selectFromId, selectToId) {
	var selectorFrom = '.selectOption#' + selectFromId;
	var items = $(selectorFrom).find('option:selected').map(function() {
		return this.value;
	}).get();

	move(selectToId, items);
	$(selectorFrom).children('option:selected').remove();
}

function selToLeft(selectFromId, selectToId) {
	var selectorFrom = '.selectOption#' + selectFromId;
	var items = $(selectorFrom).find('option:selected').map(function() {
		return this.value;
	}).get();

	move(selectToId, items);
	$(selectorFrom).children('option:selected').remove();
}
