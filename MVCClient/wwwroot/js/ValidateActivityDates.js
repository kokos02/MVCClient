

$(document).ready(function () {

    $("#StartDate").focusout(function () {
        document.getElementById("submitButton").disabled = false;
        ValidateDates();
    });

    $("#EndDate").focusout(function () {
        document.getElementById("submitButton").disabled = false;
        ValidateDates();
    });

})

function ValidateDates() {

    if (($("#StartDate").val() != "") && ($("#EndDate").val() != "")) {

        if ($("#StartDate").val() > $("#EndDate").val()) {
            document.getElementById("submitButton").disabled = true;
            alert(`End date can't be earlier than Start date`);
        }
    }
}
