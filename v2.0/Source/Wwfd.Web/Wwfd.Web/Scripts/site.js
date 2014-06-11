$(document).ready(function() {
	$('#searchTextFounder').keydown(function(e) {
		var code = e.which ? e.which : e.keyCode;
		switch (code) {
		case 27: //esc
			$('#searchTextFounder').val();
			break;
		}
	});

	$('#searchTextFounder').keyup(function (e) {
		$('#founderList').load('/Founder/Listing/' + $('#searchTextFounder').val());
	});
});
