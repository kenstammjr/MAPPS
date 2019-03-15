function rbnCancelClick(url) {
    if (window.frameElement != null) {
        window.frameElement.cancelPopUp(); return false;
    } else {
        window.location = url;
    }
}

function closeModalDialog(operation, message) {
    ExecuteOrDelayUntilScriptLoaded(function () {
        SP.UI.ModalDialog.commonModalDialogClose(operation, message);
    }, "sp.js");
}

function openModalDialog(title, url, autoSize) {
    ExecuteOrDelayUntilScriptLoaded(function () {
        autoSize = (autoSize == undefined || autoSize == null) ? true : autoSize;
        var options = {
            url: url,
            title: title,
            allowMaximize: false,
            showClose: true,
            autoSize: autoSize,
            dialogReturnValueCallback: function (dialogResult, returnValue) {
                if (dialogResult == SP.UI.DialogResult.OK)
                    window.location.href = window.location.href;
            }
        }
        SP.UI.ModalDialog.showModalDialog(options);
    }, "sp.js");
}

function resizeModalDialog(reCenter) {
    ExecuteOrDelayUntilScriptLoaded(function () {
//        autoSize(SP.UI.ModalDialog.get_childDialog());
 //       SP.UI.ModalDialog.get_childDialog().autoSize();
        if (reCenter === 'True') {
            var dlgWin = $(".ms-dlgContent", window.parent.document);
            dlgWin.css({
                top: ($(window.top).height() / 2 - dlgWin.height() / 2) + 'px',
                left: $(window.top).width() / 2 - dlgWin.width() / 2
            });
        }
    }, "sp.js");
}
$(document).ready(function () {
    // custom change event for rank category drop down list
    $("#ctl00_PlaceHolderMain_ddlRankCategory").change(function () {
        if ($('option:selected', $(this)).text() == "DoD Contractor") {
            $("#trCompany").removeClass('mapps-hide');
        } else {
            $("#trCompany").addClass('mapps-hide');
        }
        var webMethod = "/_layouts/15/MAPPS/webservices/data.asmx/RanksDDL";
        var parameters = "{'RankCategoryID':'" + $('option:selected', $(this)).val() + "'}";
        $.ajax({
            type: "POST",
            url: webMethod,
            data: parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#ctl00_PlaceHolderMain_ddlRank").empty();
                var newOption = msg.d;
                $("#ctl00_PlaceHolderMain_ddlRank").append(newOption);
                $("#ctl00_PlaceHolderMain_ddlRank").trigger("chosen:updated");
                //alert(msg.d);
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
                alert("errMessage: " + err.message);
            }


        });
    });

    // show company drop down list if rank category equals "DoD Contractor"
    if ($('option:selected', $("#ctl00_PlaceHolderMain_ddlRankCategory")).text() == "DoD Contractor") {
        $("#trCompany").removeClass('mapps-hide');
    }
    // show company label if rank category equals "DoD Dontractor"
    if ($("#ctl00_PlaceHolderMain_lblRankCategoryView").text() == "DoD Contractor") {
        $("#trCompany").removeClass('mapps-hide');
    }
});
function SaveProfile() {
    // get list of active properties
    // iterate through list and see if a control exists
    // if control exists, 
    // --- save the value whether new or not
    // --- (TODO) log the save action
    var webMethod = "/_layouts/15/MAPPS/webservices/data.asmx/ActiveProperties";
    var parameters = "{'ID':'0'}";
    var userID = $('#ctl00_PlaceHolderMain_hfUserID').val();
    $.ajax({
        type: "POST",
        url: webMethod,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            // if userid = 0 the create a new user profile before adding user profile values
            //if (userID == 0)


            $.each(msg.d, function (id, property) {
                // for each property
                switch (property.dataType) {
                    case 'nvarchar':
                        if (property.isUserEditable) {
                            var control = $('#ctl00_PlaceHolderMain_txt' + property.name);
                            SaveProperty(property.id, control.val(), userID, property.isEncrypted)
                        }
                        break;
                    case 'lookup':
                        if (property.isUserEditable) {
                            var control = $('#ctl00_PlaceHolderMain_ddl' + property.name);
                            SaveProperty(property.id, control.val(), userID, property.isEncrypted)
                            //alert(property.name + " -> " + control.val() + " -> " + property.id);
                        }

                        break;
                    case 'datetime':
                        if (property.isUserEditable) {
                            var control = $('#ctl00_PlaceHolderMain_txt' + property.name);
                            SaveProperty(property.id, control.val(), userID, property.isEncrypted)
                        }
                        break;
                    case 'bit':
                        if (property.isUserEditable) {
                            var control = $('#ctl00_PlaceHolderMain_cb' + property.name);
                            var checked = control.is(':checked') ? 'True' : 'False';
                            //alert(checked);
                            SaveProperty(property.id, checked, userID, property.isEncrypted)
                        }
                        break;

                    default:
                        break;
                }
            });
            window.location.replace("/_layouts/15/mapps/pages/userprofiles.aspx");
        },
        error: function (e) {
            alert("ActiveProperties: Error" + e);
        }
    });
}
function SaveProperty(propertyId, propertyValue, userId, isEncrypted) {
    var webMethod = "/_layouts/15/MAPPS/webservices/data.asmx/SaveUserProperty";
    var parameters = "{'PropertyID':'" + propertyId + "', 'PropertyValue':'" + propertyValue + "', 'UserID':'" + userId + "', 'IsEncrypted':'" + isEncrypted + "'}";
    $.ajax({
        type: "POST",
        url: webMethod,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
        },
        error: function (e) {
            alert("error");
        }
    });
}

var tipwidth = '150px' //default tooltip width
var tipbgcolor = 'lightyellow'  //tooltip bgcolor
var disappeardelay = 250  //tooltip disappear speed onMouseout (in miliseconds)
var vertical_offset = "0px" //horizontal offset of tooltip from anchor link
var horizontal_offset = "-3px" //horizontal offset of tooltip from anchor link

/////No further editting needed

var ie4 = document.all
var ns6 = document.getElementById && !document.all

if (ie4 || ns6)
    document.write('<div id="fixedtipdiv" style="visibility:hidden;width:' + tipwidth + ';background-color:' + tipbgcolor + '" ></div>')

function getposOffset(what, offsettype) {
    var totaloffset = (offsettype == "left") ? what.offsetLeft : what.offsetTop;
    var parentEl = what.offsetParent;
    while (parentEl != null) {
        totaloffset = (offsettype == "left") ? totaloffset + parentEl.offsetLeft : totaloffset + parentEl.offsetTop;
        parentEl = parentEl.offsetParent;
    }
    return totaloffset;
}


function showhide(obj, e, visible, hidden, tipwidth) {
    if (ie4 || ns6)
        dropmenuobj.style.left = dropmenuobj.style.top = -500
    if (tipwidth != "") {
        dropmenuobj.widthobj = dropmenuobj.style
        dropmenuobj.widthobj.width = tipwidth
    }
    if (e.type == "click" && obj.visibility == hidden || e.type == "mouseover")
        obj.visibility = visible
    else if (e.type == "click")
        obj.visibility = hidden
}

function iecompattest() {
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
}

function clearbrowseredge(obj, whichedge) {
    var edgeoffset = (whichedge == "rightedge") ? parseInt(horizontal_offset) * -1 : parseInt(vertical_offset) * -1
    if (whichedge == "rightedge") {
        var windowedge = ie4 && !window.opera ? iecompattest().scrollLeft + iecompattest().clientWidth - 15 : window.pageXOffset + window.innerWidth - 15
        dropmenuobj.contentmeasure = dropmenuobj.offsetWidth
        if (windowedge - dropmenuobj.x < dropmenuobj.contentmeasure)
            edgeoffset = dropmenuobj.contentmeasure - obj.offsetWidth
    }
    else {
        var windowedge = ie4 && !window.opera ? iecompattest().scrollTop + iecompattest().clientHeight - 15 : window.pageYOffset + window.innerHeight - 18
        dropmenuobj.contentmeasure = dropmenuobj.offsetHeight
        if (windowedge - dropmenuobj.y < dropmenuobj.contentmeasure)
            edgeoffset = dropmenuobj.contentmeasure + obj.offsetHeight
    }
    return edgeoffset
}

function fixedtooltip(menucontents, obj, e, tipwidth) {
    if (window.event) event.cancelBubble = true
    else if (e.stopPropagation) e.stopPropagation()
    clearhidetip()
    dropmenuobj = document.getElementById ? document.getElementById("fixedtipdiv") : fixedtipdiv
    dropmenuobj.innerHTML = menucontents
    if (!dropmenuobj.repositioned) {
        document.body.appendChild(dropmenuobj)
        dropmenuobj.repositioned = true
    }

    if (ie4 || ns6) {
        showhide(dropmenuobj.style, e, "visible", "hidden", tipwidth)
        dropmenuobj.x = getposOffset(obj, "left")
        dropmenuobj.y = getposOffset(obj, "top")
        dropmenuobj.style.left = dropmenuobj.x - clearbrowseredge(obj, "rightedge") + "px"
        dropmenuobj.style.top = dropmenuobj.y - clearbrowseredge(obj, "bottomedge") + obj.offsetHeight + "px"
    }
}

function hidetip(e) {
    if (typeof dropmenuobj != "undefined") {
        if (ie4 || ns6)
            dropmenuobj.style.visibility = "hidden"
    }
}

function delayhidetip() {
    if (ie4 || ns6)
        delayhide = setTimeout("hidetip()", disappeardelay)
}

function clearhidetip() {
    if (typeof delayhide != "undefined")
        clearTimeout(delayhide)
}