// Execute timer
$(function() {
	window.timerId = setInterval(timer, 1000);
});
// Init timer seconds on page
function setTime(sec) {
	if (parseInt(sec, 10) >= 0) {
		$('.timer', function() {
			$('#sec').text(sec);
		});
	} else {
		alert('Incorrect number(must be integer and more than 0)');
	}
}

function timer() {
	var sec = parseInt($('.timer>#sec').text());
	var end = false;

	if (sec > 1) {
		sec--;
	} else {
		end = true;
	}

	if (end) {
		clearInterval(timerId);
		var page = parseInt(document.location.pathname.match(/[^\/]+$/)[0]);
		var ref = './' + page + '.html';
		location.assign(ref);
		if (page === 5) {
			setTime(0);
			if (confirm('    Finish\n    Start anew?')) {
				var ref = './1.html';
				location.assign(ref);
				return;
			} else {
				window.close();
				alert('Failed : look at console');
				return;
			}
		}

		replacePage(++page);
	} else {
		$('.timer>#sec').text(sec);
	}
	return 0;
}

function replacePage(nextPage) {
	var ref = './' + nextPage + '.html';
	window.location.assign(ref);
}

function next() {
	var curPage = parseInt(document.location.pathname.match(/[^\/]+$/)[0]);
	if (curPage != 5) {
		clearInterval(timerId);
		replacePage(++curPage);
	}
}

function back() {
	var curPage = parseInt(document.location.pathname.match(/[^\/]+$/)[0]);
	if (curPage != 1) {
		clearInterval(timerId);
		replacePage(--curPage);
	}
}

function pause() {
	$('.jump>#btnPause').click(function() {
		if ($(this).prop('value') == ' || ') {
			clearInterval(timerId);
			$(this).prop('value', '>>');
		} else {
			window.timerId = setInterval(timer, 1000);
			$(this).prop('value', ' || ');
		}
	});
}