var founderSearchTimer

function fireSearchEvent() {
    if (founderSearchTimer)
        clearTimeout(founderSearchTimer);

    founderSearchTimer = setTimeout('__doPostBack(\'ctl00$txtSearchFounders\',\'\')', 350);
}

function btnSearch_Click() {
    document.forms[0].action = 'Search.aspx?'
}


