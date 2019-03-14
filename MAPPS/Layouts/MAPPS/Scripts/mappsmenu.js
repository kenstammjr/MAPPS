//var $sofMenu = $("#sofmenu");
$(document).ready(function () {

    $LeftPanelImg = $('#LeftPanelImg');
    $LeftPanelImg.attr('src', '/_layouts/images/blank.gif');
    SOCMasterPageRotateQuickLaunch(false);
    $("#s4-searcharea div").hide();


    $("#sofmenusearchimg").click(function () {
        var filter = $('#sofmenusearchbox').val();
        var url = $('#sofmenusearchurl').val();
        window.location = url + filter;
    });

    $("#sofmenusearchbox").keyup(function (event) {
        var obj = $(this);
        if ((event.keyCode ? event.keyCode : event.which) === 13) {
            search();
        }
        
        //clearTimeout($.data(this, 'timer'));
        //var wait = setTimeout(search, 500);
        //$(this).data('timer', wait);
    });

    function search() {
        var filter = $('#sofmenusearchbox').val();
        var url = $('#sofmenusearchurl').val();
        window.location = url + filter;
    };

    function SOCMasterPageRotateQuickLaunch(clicked) {
    }
});

var mtimeout = 500;
var mclosetimer = 0;
var ddmenuitem = 0;

// open hidden layer
function mopen(id) {
    // cancel close timer
    mcancelclosetime();

    // close old layer
    if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';

    // get new layer and show it
    ddmenuitem = document.getElementById(id);
    ddmenuitem.style.visibility = 'visible';

}
// close showed layer
function mclose() {
    if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
}

// go close timer
function mclosetime() {
    mclosetimer = window.setTimeout(mclose, mtimeout);
}

// cancel close timer
function mcancelclosetime() {
    if (mclosetimer) {
        window.clearTimeout(mclosetimer);
        mclosetimer = null;
    }
}
$('li').hover(function () {
    $(this).toggleClass('nav-list-selected').siblings().removeClass('nav-list-selected');
});
