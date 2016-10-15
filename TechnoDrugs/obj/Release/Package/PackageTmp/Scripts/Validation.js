/*
Author: Aslam Iqbal
Created: 04 March, 2013
*/
/// <reference path="jquery-1.9.0.js" />

$(document).ready(function () {
    //for design
    $("input:text").each(function () {
        $(this).addClass("textbox-input");
    });
    //for design end
    //class event
    $(".numeric").keydown(function (event) {
        if (event.keyCode == 46 || event.keyCode == 190 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
            (event.keyCode == 65 && event.ctrlKey == true) ||
                (event.keyCode >= 35 && event.keyCode <= 39)) {

            var text = $("#" + this.id).val();
            if (event.keyCode == 190 && text.toString().indexOf(".") > 0)
                event.preventDefault();

            return;
        } else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });
    $("input:text").focus(function () {
        $("#" + this.id).addClass("textboxonfocus");
    });
    /////////////
    $(".require").focus(function (event) {

        $("#" + this.id).addClass("textboxonfocus"); //textboxonfocus
        //$("#" + this.id).removeClass("textbox-input:focus");

        //ValidateRequireElement($("#" + this.id));
    });
    $("input:text").blur(function () {
        $("#" + this.id).removeClass("textboxonfocus");
        //$("#" + this.id).removeClass("inputerror-focus");
    });
    ///////////////////////////// focusout
    $(".require").blur(function (event) {
        ValidateRequireElement($("#" + this.id));
        $("#" + this.id).removeClass("textboxonfocus");
        $("#" + this.id).removeClass("inputerror-focus"); //
        //$("#" + this.id).addClass("inputerror-focus");  
    });

    ///////////////////////////
    $(".require").keypress(function (event) {
        $("#" + this.id).removeClass("inputerror");
    });


    $(".validateonclick").click(function (event) {
        return ValidateRequireControls();
    });

    //class event end
});  //$(document).ready(function () {

function ValidateRequireElement(ctrl) {
    var text = ctrl.val();
    if (text.toString().length == 0) {
        ctrl.addClass("inputerror");

        $(".validateonclick").each(function () {
            //$(this).attr('disabled', 'disabled');
        });

        return false; //error
    } else {
        ctrl.removeClass("inputerror");
        return true; // no error
    }
}

/*
function ShowAlert(alertMessage) { 
$(".validationbox").each(function () {
$(this).removeClass('successstyle');
$(this).addClass('errorstyle');
$(this).text(alertMessage);
});
}*/
/*
$(".validationbox").each(function () {
$(this).removeClass('successstyle');
$(this).removeClass('errorstyle');
$(this).addClass('validationbox');
$(this).text('');
});
*/
//
function ShowAlert(alertMessage) {
    $('#ContentPlaceHolder1_lblMsg').removeClass('successstyle');
    $('#ContentPlaceHolder1_lblMsg').addClass('errorstyle');
    $('#ContentPlaceHolder1_lblMsg').text(alertMessage);
}
function ValidateRequireControls(alertMessage) {
    $('#ContentPlaceHolder1_lblMsg').removeClass('successstyle');
    $('#ContentPlaceHolder1_lblMsg').removeClass('errorstyle');
    $('#ContentPlaceHolder1_lblMsg').addClass('validationbox');
    $('#ContentPlaceHolder1_lblMsg').text('');

    /*
    lblMsg.InnerText = string.Empty;
    lblMsg.Attributes.Remove("class");
    lblMsg.Attributes.Add("class", "validationbox");
    */
    var noError = true;
    var firstelmtofocus;
    $('.require').each(function () {
        var requireField = $(this)[0];
        if (requireField.type == 'text') {
            if (!ValidateRequireElement($('#' + requireField.id))) {
                noError = false;
                if (firstelmtofocus == null) firstelmtofocus = requireField;
            }
        }
    });

    if (!noError) {
        if (alertMessage != null) {
            ShowAlert(alertMessage);
        }
        if (firstelmtofocus != null) {
            focusing = true;
            $('#' + firstelmtofocus.id).focus();
            $('#' + firstelmtofocus.id).removeClass("textboxonfocus"); //
            $('#' + firstelmtofocus.id).addClass("inputerror-focus");
            focusing = false;
        }

    }
    return noError;
}

var focusing = false;

function isNumberKeyAndDot(event, value) {
    var charCode = (event.which) ? event.which : event.keyCode
    var intcount = 0;
    var stramount = value;
    for (var i = 0; i < stramount.length; i++) {
        if (stramount.charAt(i) == '.' && charCode == 46) {
            return false;
        }
    }
    if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode != 46)
        return false;
    return true;
}

function ShowWatermark(textBox, waterMark) {
    if (textBox.value.length == 0) {
        textBox.value = waterMark;
    }
}

function HideWatermark(textBox, waterMark) {
    if (textBox.value.length == 0)
        textBox.value = waterMark;
    else if (textBox.value == waterMark)
        textBox.value = '';
}