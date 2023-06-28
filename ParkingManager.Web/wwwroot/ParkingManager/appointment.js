﻿$(document).ready(function () {

    GetParkingSlots();
    $('#txtFirstName').keyup(function (e) {
        var txtFName = $(this).val();
        $('#txtCustomerFName').text(txtFName);
    });
    $('#txtLastName').keyup(function (e) {
        var txtLName = $(this).val();
        $('#txtCustomerLName').text(txtLName);
    });

    $('#txtEmail').keyup(function (e) {
        var txtEmail = $(this).val();
        $('#txtSummaryEmail').text(txtEmail);
    });
    $('#txtPhoneNumber').keyup(function (e) {
        var txtPhone = $(this).val();
        $('#txtSummaryPhoneNo').text(txtPhone);
    });
    $('#txtTransactionNumber').keyup(function (e) {
        var txtTransNo = $(this).val();
        $('#txtSummaryTransactionNumber').text(txtTransNo);
    });

    $('#txtParkingSlotId').keyup(function (e) {
        var txtlblName = $(this).val();
        $('#txtMySlot').text(txtlblName);
    });


});
function ShowLoader() {

    $("#loadMe").modal('show');
}

function GetSummaryDetails() {

    debugger

    var firstName = document.getElementById("txtFirstName");

    var lastName = document.getElementById("txtLastName");

    var email = document.getElementById("txtEmail");

    var fullname = firstName.value + "" + lastName.value;

    $("#txtCustomerName").text(fullname.value);

    $("#txtSummaryEmail").text(email.value);

}



function HideLoader() {

    $("#loadMe").modal('hide');
}

function IsRadioChecked() {
    debugger
    $('input[type="radio"]').click(function () {

        var value = $(this);

        var label = value.parent();


        // first make ALL labels normal

        label.parent().parent().find('label').css('background', '#fff');
        label.parent().find('label').css('color', '#455A64');

        // then color just THIS one
        label.css('background', '#0071dc');
        label.css('color', '#fff');

        document.getElementById("nextBtn").style.display = "inline";

    });
}

function GetParkingSlotId() {

    var timeslotid = $('[id*="txtTime"]:checked').map(function () { return $(this).val().toString(); }).get().join(",");

    console.log(timeslotid);

    $.get("/Appointment/GetSlotById/?Id=" + timeslotid, function (data, status) {

        console.log(data);

        if (data.data == false) {
            /*  alert("Does not exist");*/
        } else {

            $("#txtId").val(data.data.id);
            $("#txtTimeSlotName").text(data.data.timeSlot);
        }

    });

}
function stkSelected() {

    document.getElementById('divPaybill').style.display = 'none';

    document.getElementById('divSTK').style.display = 'block';
}
function showInvoice() {

    document.getElementById('divmessage').style.display = 'none';

    document.getElementById('dvinvoice').style.display = 'block';
}
function payBillSelected() {

    document.getElementById('divSTK').style.display = 'none';

    document.getElementById('divPaybill').style.display = 'block';

    document.getElementById("errorMsg").style.display = "none";

}
function SendStkPush() {

    if ($('#txtMpesaPhoneNumber').val() == '') {
        $('#txtMpesaPhoneNumber').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Please enter mpesa Phone Number",
            showConfirmButton: true,
        });
        return false;
    }

    $("#loadMe").modal('show');

    var data = { PhoneNumber: $('#txtMpesaPhoneNumber').val() };

    console.log(data);

    $.ajax({
        type: 'POST',
        data: data,
        url: '/Appointment/MpesaSTKPush/',


        success: function (response) {

            console.log(response);

            if (response.success == true) {

                $("#loadMe").modal('hide');

                document.getElementById("successMsg").style.display = "block";

                $("#showsuccess").text(response.responseText);

            }

            else {

                $("#loadMe").modal('hide');

                document.getElementById("errorMsg").style.display = "block";

                $("#showError").text(response.responseText);
            }



        },

        error: function (response) {
            $("#loadMe").modal('hide');

            document.getElementById("errorMsg").style.display = "block";

            $("#showError").text("Something went wrong");
        }
    });
}
function SendStkPushOne() {

    if ($('#txtMpesaPhoneNumber').val() == '') {
        $('#txtMpesaPhoneNumber').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Please enter mpesa Phone Number",
            showConfirmButton: true,
        });
        return false;
    }


    $("#loadMe").modal('show');

    var phoneNumber = document.getElementById("txtMpesaPhoneNumber").value;

    var link = "/Appointment/MpesaSTKPush?PhoneNumber=" + phoneNumber;

    console.log(link);

    $.ajax({

        type: "POST",

        url: link,


        success: function (response) {

            console.log(response);

            if (response.success == true) {

                $("#loadMe").modal('hide');

                document.getElementById("successMsg").style.display = "block";

                $("#showsuccess").text(response.responseText);

            }

            else {

                $("#loadMe").modal('hide');

                document.getElementById("errorMsg").style.display = "block";

                $("#showError").text(response.responseText);
            }
        },

        error: function (response) {
            alert("error!");
        },
        complete: function () {
            HideLoader();
        }
    })


}
function SubmitBooking() {

    debugger


    if ($('#txtFirstName').val() == '') {
        $('#txtFirstName').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "First Name is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtLastName').val() == '') {
        $('#txtLastName').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Last Name is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtEmail').val() == '') {
        $('#txtEmail').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Email is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtPhoneNumber').val() == '') {
        $('#txtPhoneNumber').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Phone Number is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    $("#loadMe").modal('show');

    var data = $("#frmSubAppointment").serialize();

    $.ajax({

        type: "POST",

        url: "/Booking/Create/",

        data: data,

        success: function (response) {

            if (response.success) {

                var bookingId = response.responseText;

                console.log(bookingId);
                setTimeout(function () { window.location = "/Home/Receipt/" + bookingId; }, 5);
                //setTimeout(function () { window.location = "/Home/Receipt/"}, 5);

            } else {

                swal({

                    position: 'top-end',

                    type: "error",

                    title: response.responseText,

                    showConfirmButton: true,

                    timer: 5000,
                });


            }
        },

    })

}

// the selector will match all input controls of type :checkbox
// and attach a click event handler
$("input:checkbox").on('click', function () {
    // in the handler, 'this' refers to the box clicked on
    var $box = $(this);
    if ($box.is(":checked")) {
        // the name of the box is retrieved using the .attr() method
        // as it is assumed and expected to be immutable
        var group = "input:checkbox[name='" + $box.attr("name") + "']";
        // the checked state of the group/box on the other hand will change
        // and the current value is retrieved using .prop() method
        $(group).prop("checked", false);
        $box.prop("checked", true);
    } else {
        $box.prop("checked", false);
    }
});

$(document).ready(function () {

    var ckbox = $("input[name='Time']");
    var chkId = '';
    $('input').on('click', function () {

        if (ckbox.is(':checked')) {
            $("input[name='Time']:checked").each(function () {
                chkId = $(this).val() + ",";
                chkId = chkId.slice(0, -1);
            });

            //alert($(this).val()); // return all values of checkboxes checked
            //alert(chkId); // return value of checkbox checked
            console.log(chkId);
        }
    });

});


var currentTab = 0; // Current tab is set to be the first tab (0)

showTab(currentTab); // Display the current tab

function showTab(n) {
    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:
    if (n == 0) {

        document.getElementById("prevBtn").style.display = "none";

        var isSlotChecked = document.querySelector('input[name = "ParkingSlotId"]:checked');

        console.log(isSlotChecked);

        if (isSlotChecked == null) {  //Test if something was checked

            document.getElementById("nextBtn").style.display = "none";

        } else {
            document.getElementById("nextBtn").style.display = "inline";
        }


    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {

        document.getElementById("nextBtn").innerHTML = "Submit";

    } else {
        document.getElementById("nextBtn").innerHTML = "Next";

    }
    //... and run a function that will display the correct step indicator:
    fixStepIndicator(n)

    console.log(n);


}

function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");

    // Exit the function if any field in the current tab is invalid:
    // if (n == 1 && !validateForm()) return false;
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form...

    if (currentTab == 2) {

        document.getElementById("btnSubmit").style.display = "inline";
        document.getElementById("nextBtn").style.display = "none";

    } else {
        document.getElementById("btnSubmit").style.display = "none";
        document.getElementById("nextBtn").style.display = "inline";

    }


    if (currentTab >= x.length) {
        // ... the form gets submitted:

        console.log(currentTab);



        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");

    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";

}

$(function () {
    $("#txtAppointmentDate").datepicker();
});


function GetParkingSlots() {

    var date = $('#txtAppointmentDate').val();

    console.log(date)

    $("#txtAppDate").text(date);

    $.ajax({
        type: "GET",
        url: "/Admin/ParkingSlots/GetSlots",
        data: "{}",

        success: function (data) {

            var arr = data;

            if (arr.length == 0) {

                $('#slots').hide();

                $('#divShowMessage').show();

                $("#divmessage").html("Sorry ,there no slots ,please come back later");
            } else {

                $('#slots').show();

                $('#divShowMessage').hide();

                $('#slots').empty();

                $.each(arr, function (index, value) {

                    //console.log('The value at arr [' + index + '] is : ' + value);


                    console.log(value);

                    let label = document.createElement("label");
                    label.innerText = value.slotName;                  
                    label.classList.add('btn');
                    label.classList.add('btn-outline-primary');
                    label.classList.add('label');

                    let input = document.createElement("input");
                    input.type = "radio";
                    input.id = "txtParkingSlotId";
                    input.name = "ParkingSlotId";
                    input.value = value.slotId;


                    label.appendChild(input);
                    slots.appendChild(label);

                    IsRadioChecked();

                });
            }

        }

    });


}


function GetParkingSlotsReschedule() {

    var date = $('#txtAppointmentDateReschedule').val();

    console.log(date)

    $("#txtAppDate").text(date);

    $.ajax({
        type: "GET",
        url: "/Appointment/GetSlotsByAppointmentDate?AppointmentDate=" + date,
        data: "{}",

        success: function (data) {

            var arr = data;

            if (arr.length == 0) {


                $('#slots').hide();

                $('#divShowMessage').show();

                $("#divmessage").html("Sorry ,there no slots on the selected date ,please select another date");
            } else {

                $('#slots').show();

                $('#divShowMessage').hide();

                $('#slots').empty();

                $.each(arr, function (index, value) {

                    //console.log('The value at arr [' + index + '] is : ' + value);


                    console.log(value);

                    let label = document.createElement("label");
                    label.innerText = value.slotName;
                    label.classList.add('btn');
                    label.classList.add('btn-outline-primary');
                    label.classList.add('label');

                    let input = document.createElement("input");
                    input.type = "radio";
                    input.id = "txtParkingSlotId";
                    input.name = "ParkingSlotId";
                    input.value = value.slotId;
                    input.onchange = GetParkingSlotId;


                    label.appendChild(input);
                    slots.appendChild(label);

                    IsRadioChecked();

                });
            }

        }

    });


}

function GetSlotsByQuickSearch() {

    var search_value = $('#txtQuickSearch').val();

    console.log(search_value)

    //$("#txtAppDate").text(date);


    $.ajax({
        type: "GET",
        url: "/Appointment/GetSlotsByQuickSearch?QuickSearch=" + search_value,
        data: "{}",

        success: function (data) {

            var arr = data;

            if (arr.length == 0) {


                $('#slots').hide();

                $('#divShowMessage').show();

                $("#divmessage").html("Sorry ,there no slots on the selected date ,please select another date");

            } else {

                $('#slots').show();

                $('#divShowMessage').hide();

                $('#slots').empty();

                $.each(arr, function (index, value) {

                    //console.log('The value at arr [' + index + '] is : ' + value);


                    console.log(value);

                    let label = document.createElement("label");
                    label.innerText = value.slotName;
                    label.classList.add('btn');
                    label.classList.add('btn-outline-primary');
                    label.classList.add('label');

                    let input = document.createElement("input");
                    input.type = "radio";
                    input.id = "txtParkingSlotId";
                    input.name = "ParkingSlotId";
                    input.value = value.slotId;
                    input.onchange = GetParkingSlotId;


                    label.appendChild(input);
                    slots.appendChild(label);

                    IsRadioChecked();

                });
            }

        }

    });


}

function ApproveAppointment() {

    $("#ModalApproveAppointment").modal('hide');

    var data = $("#frmApproveAppointment").serialize();

    $.ajax({

        type: "POST",

        url: "/Admin/Appointments/ApproveAppointment/",

        data: data,

        beforeSend: function () { ShowLoader(); },

        success: function (response) {

            if (response.success) {

                swal({

                    position: 'top-end',

                    type: "success",

                    title: response.responseText,

                    showConfirmButton: false,

                }), setTimeout(function () { location.reload(); }, 3000);

            } else {

                swal({

                    position: 'top-end',

                    type: "error",

                    title: response.responseText,

                    showConfirmButton: true,

                    timer: 5000,
                });


            }
        },

        error: function (response) {
            alert("error!");
        },
        complete: function () {
            HideLoader();
        }
    })

}

function GetAppointment(e) {

    var id = e;

    console.log(id);

    $.get("/Admin/Appointments/GetById/?Id=" + id, function (data, status) {
        console.log(data);
        if (data.data == false) {
            alert("Does not exist");
        } else {

            $("#txtAppointmentId").val(data.data.id);
            $("#txtAppointmentTime").text(data.data.timeSlot);
            $("#txtAppointmentDate").text(data.data.newAppDate);
            $("#txtPatientName").text(data.data.fullName);
            $("#txtEmail").text(data.data.email);
            $("#txtPhoneNumber").text(data.data.phoneNumber);
            $("#txtpatientId").val(data.data.patientId);

            $('#ModalApproveAppointment').modal({ backdrop: 'static', keyboard: false })

            $("#ModalApproveAppointment").modal('show');

        }

    });
};

function GetRescheduleDetails(e) {

    var id = e;

    console.log(id);

    $.get("/Admin/Appointments/GetById/?Id=" + id, function (data, status) {
        console.log(data);
        if (data.data == false) {
            alert("Does not exist");
        } else {

            $("#txtAppointmentId1").val(data.data.id);
            $("#txtAppointmentTime1").text(data.data.timeSlot);
            $("#txtAppointmentDate1").text(data.data.newAppDate);
            $("#txtPatientName1").text(data.data.fullName);
            $("#txtEmail1").text(data.data.email);
            $("#txtPhoneNumber1").text(data.data.phoneNumber);
            $("#txtpatientId1").val(data.data.patientId);
            $("#txtParkingSlotIdId1").val(data.data.timeSlotId);


            $('#ModalRescheduleAppointment').modal({ backdrop: 'static', keyboard: false })

            $("#ModalRescheduleAppointment").modal('show');
        }

    });
};

function RescheduleAppointment() {

    $("#ModalRescheduleAppointment").modal('hide');

    var data = $("#frmRescheduleAppointment").serialize();

    $.ajax({

        type: "POST",

        url: "/Admin/Appointments/RescheduleAppointment/",

        data: data,

        beforeSend: function () { ShowLoader(); },

        success: function (response) {

            if (response.success) {


                swal({

                    position: 'top-end',

                    type: "success",

                    title: response.responseText,

                    showConfirmButton: false,

                }), setTimeout(function () { location.reload(); }, 3000);

            } else {

                swal({

                    position: 'top-end',

                    type: "error",

                    title: response.responseText,

                    showConfirmButton: true,

                    timer: 5000,
                });


            }
        },

        error: function (response) {
            alert("error!");
        },
        complete: function () {
            HideLoader();
        }
    })
}



