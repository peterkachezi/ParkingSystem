
$(document).ready(function () {

    GetSlots();

   //document.getElementById("btnNextPersonalDetails").style.display = "none";


});

function GetSlots() {
    $.ajax({
        type: "GET",
        url: "/Appointment/GetSlots/",
        data: "{}",
        success: function (data) {
            if (data.length > 0) {
                $('#slots').show();
                $('#divShowMessage').hide();
                $('#slots').empty();
                var s = '';
                for (var i = 0; i < data.length; i++) {
                    s += '<div class="col-sm-4 col-md-4 col-xs-9 p-1"><label class="btn btn-outline-primary label"> ' + data[i].slotName + ' <input type = "radio" id ="txtTimeSlotId" class="check-with-label" onclick="IsRadioChecked()" name="TimeSlotId" value=' + data[i].slotId + '></label></div>';
                }
                $("#slots").html(s);
            }
            if (data.length == 0) {
                $('#slots').hide();
                $('#divShowMessage').show();
                $("#divmessage").html("Sorry ,there are no slots on the selected date ,please select another date");
            }
            console.log(data.length);
            IsRadioChecked();
        }
    });
}

function GetSlotsByDate() {
    var date = $('#txtAppointmentDate').val();
    console.log(date)
    $.ajax({
        type: "GET",
        url: "/Appointment/GetSlotsByAppointmentDate?AppointmentDate=" + date,
        data: "{}",
        success: function (data) {
            if (data.length > 0) {
                $('#slots').show();
                $('#divShowMessage').hide();
                $('#slots').empty();
                var s = '';
                for (var i = 0; i < data.length; i++) {
                    s += '<div class="col-sm-4 col-md-4 col-xs-9 p-1"><label class="btn btn-outline-primary label"> ' + data[i].slotName + ' <input type = "radio" id ="txtTimeSlotId" class="check-with-label" onclick="IsRadioChecked()" name="TimeSlotId" value=' + data[i].slotId + '></label></div>';
                }
                $("#slots").html(s);
            }
            if (data.length == 0) {
                $('#slots').hide();
                $('#divShowMessage').show();
                $("#divmessage").html("Sorry ,there are no slots on the selected date ,please select another date");
            }
            console.log(data.length);
            IsRadioChecked();
        }
    });
}

function GetBillingDetails() {

    var firstName = document.getElementById('txtFirstName').value;

    var lastName = document.getElementById('txtLastName').value;

    var phoneNumber = document.getElementById('txtPhoneNumber').value;

    var email = document.getElementById('txtEmail').value;



    document.getElementById("txtPatientName").innerHTML = firstName + " " + lastName;

    document.getElementById("txtPatientPhoneNumber").innerHTML = phoneNumber;

    document.getElementById("txtPatientEmail").innerHTML = email; 
}

function GetService() {
       

    $.get("/Appointment/GetService/", function (data, status) {

        console.log(data);

        if (data.data == false) {

            alert("Does not exist");

        } else {

            $("#txtTotalAmount").val(data.data.amount);

        }

    });
};

function IsRadioChecked() {

    var timeslotid = $('[id*="txtTime"]:checked').map(function () { return $(this).val().toString(); }).get().join(",");

    $.get("/Appointment/GetSlotById/?Id=" + timeslotid, function (data, status) {
        console.log(data);
        if (data.data == false) {
            /*  alert("Does not exist");*/
        } else {
            $("#txtId").val(data.data.id);
            $("#txtPatientAppTime").text(data.data.timeSlot);            
            $("#txtPatientAppDate").text(data.data.newAppointmentDate);  
        }
    });

    $('input[type="radio"]').click(function () {
        var value = $(this);
        var label = value.parent();
        // first make ALL labels normal
        label.parent().parent().find('label').css('background', '#fff');
        label.parent().parent().find('label').css('color', '#455A64');

        label.parent().find('label').css('color', '#455A64');
        // then color just THIS one
        label.css('background', '#0071dc');
        label.css('color', '#fff');

        document.getElementById("btnSlotNext").style.display = "inline";
    });

}

//function IsRadioChecked() {

//    var timeslotid = $('[id*="txtTime"]:checked').map(function () { return $(this).val().toString(); }).get().join(",");

//    console.log(timeslotid);

//}

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
                $('#divStkStatus').show();
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

function SubmitAppointment() {

    if ($('#txtAppointmentDate').val() == '') {
        $('#txtAppointmentDate').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Appointment Date is a required field",
            showConfirmButton: true,
        });
        return false;
    }






    if ($('#txtFirstName').val() == '') {

        $('#txtFirstName').focus();

        $("#loadMe").modal('hide');

        document.getElementById("paymentErrorMsg").style.display = "block";

        $("#showPaymentErrorMsg").text("First Name is a required field");

        return false;
    }

    if ($('#txtLastName').val() == '') {
        $('#txtLastName').focus();
        $("#loadMe").modal('hide');

        document.getElementById("paymentErrorMsg").style.display = "block";

        $("#showPaymentErrorMsg").text("Last Name is a required field");
        return false;
    }

    if ($('#txtEmail').val() == '') {
        $('#txtEmail').focus();
        $("#loadMe").modal('hide');

        document.getElementById("paymentErrorMsg").style.display = "block";

        $("#showPaymentErrorMsg").text("Email is a required field");
        return false;
    }

    if ($('#txtPhoneNumber').val() == '') {
        $('#txtPhoneNumber').focus();
        $("#loadMe").modal('hide');

        document.getElementById("paymentErrorMsg").style.display = "block";

        $("#showPaymentErrorMsg").text("Phone Number Name is a required field");
        return false;
    }

    var appointment = new Object();

    appointment.NewAppointmentDate = $('#txtAppointmentDate').val();

    appointment.FirstName = $('#txtFirstName').val();

    appointment.LastName = $('#txtLastName').val();

    appointment.Email = $('#txtEmail').val();

    appointment.PhoneNumber = $('#txtPhoneNumber').val();

    appointment.TimeSlotId = $('#txtTimeSlotId').val(); 

    console.log(appointment);

    $("#loadMe").modal('show');

    //var data = $("#frmSubAppointment").serialize();

    $.ajax({

        type: "POST",

        url: "/Appointment/Create1/",

        data: appointment,

        success: function (response) {

            if (response.success) {

                var appointmentId = response.responseText;

                console.log(appointmentId);

                //setTimeout(function () { location.reload(); }, 3000);

                setTimeout(function () { window.location = "/Appointment/Receipt/" + appointmentId; }, 5);

            } else {

                $("#loadMe").modal('hide');

                document.getElementById("paymentErrorMsg").style.display = "block";

                $("#showPaymentErrorMsg").text(response.responseText);


            }
        },

    })

}



//$("#txtLastName").keypress(function (e) {

//    if ($("#txtLastName").val == '') {
//        //..disable button..
//        document.getElementById("btnNextPersonalDetails").style.display = "none";

//    } else {
//        //..enable button
//        document.getElementById("btnNextPersonalDetails").style.display = "none";

//    }
//});





//$('input[name=LastName]').change(function () {
//    if ($(this).val() == "") {
//        document.getElementById("btnNextPersonalDetails").style.display = "none";
//    } else {
//        document.getElementById("btnNextPersonalDetails").style.display = "none";
//    }
//});




//$('input[name=PhoneNumber]').change(function () {
//    if ($(this).val() == "") {
//        document.getElementById("btnNextPersonalDetails").style.display = "none";
//    } else {
//        document.getElementById("btnNextPersonalDetails").style.display = "none";
//    }
//});


//$('input[name=Email]').change(function () {
//    if ($(this).val() == "") {
//        document.getElementById("btnNextPersonalDetails").style.display = "none";
//    } else {
//        document.getElementById("btnNextPersonalDetails").style.display = "block";
//    }
//});


