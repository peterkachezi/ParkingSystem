

function ShowLoader() {

    $("#loadMe").modal('show');
}

function HideLoader() {

    $("#loadMe").modal('hide');
}


function GetAppointmentById(e) {

    var id = e;

    var tr = '';

    $.ajax({

        url: '/Admin/Appointments/GetAppointmentDetailsById/?Id=' + id,

        method: 'Get',

        success: (result) => {

            $.each(result, (k, v) => {

                tr += `<tr>

                    <td>${v.sequenceNo}</td>

                    <td>${v.fullName}</td>

                    <td>
                     ${v.newAppDate}
                     <span class="text-primary d-block">${v.timeSlot}</span>
                     </td>

                    <td>${v.newCreateDate}</td>

                      <td>                                             

                     <span class="pending">Pending Approval</span>
                                            
                    </td>


                      <td class="text-end">
                          <a class="btn btn-success btn-sm " href="#" onclick="GetAppointment('${v.id}')" value=""><i class="fa fa-check btn-app" aria-hidden="true"></i><span class="btn-approve">Approve</span></a>
                           <a class="btn btn-primary btn-sm " href="#" onclick="GetRescheduleDetails('${v.id}')" value=""><i class="fa fa-calendar btn-res" aria-hidden="true"></i><span class="btn-reschedule">Reschedule</span></a>
                      </td>

                       </tr>`
            })

            $("#tblAppointment").html(tr);


            console.log(result);
        },
        error: (error) => {
            console.log(error);
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
            $("#txtTimeSlotIdId1").val(data.data.timeSlotId);


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

function GetSlotsByDateReschedule() {

    var date = $('#txtAppointmentDateReschedule').val();

    console.log(date)
      

    $.ajax({
        type: "GET",
        url: "/Admin/Appointments/GetSlotsByAppointmentDate?AppointmentDate=" + date,
        data: "{}",

        success: function (data) {

            var arr = data;

            if (arr.length == 0) {

                $('#slots').hide();

                $('#divShowMessage').show();

                $("#divmessage").html("Sorry ,there are no slots on the selected date ,please select another date");

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
                    input.id = "txtTimeSlotId";
                    input.name = "TimeSlotId";
                    input.value = value.slotId;
                    input.onchange = GetTimeSlotId;


                    label.appendChild(input);
                    slots.appendChild(label);

                    IsRadioChecked();

                });
            }

        }

    });


}

function GetTimeSlotId() {

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

function IsRadioChecked() {

    $('input[type="radio"]').click(function () {

        var value = $(this);

        var label = value.parent();


        // first make ALL labels normal

        label.parent().parent().find('label').css('background', '#fff');
        label.parent().find('label').css('color', '#455A64');

        // then color just THIS one
        label.css('background', '#0071dc');
        label.css('color', '#fff');


    });
}

function StartVideoCall(e) {

    var id = e;

    window.location.href = "/Admin/VideoChat/Index/" + id;
}


function DeleteRecord(e) {

    var id = e;

    swal(

        {
            title: "Are you sure?",

            //text: "Deac!",

            type: "success",

            showCancelButton: true,

            confirmButtonColor: "##62b76e",

            confirmButtonText: "Yes, Procceed!",

            closeOnConfirm: false
        },

        function () {

            $.ajax({

                type: "GET",

                url: "/Admin/Appointments/Delete/" + id,

                success: function (response) {

                    if (response.success) {

                        swal({

                            position: 'top-end',

                            type: "success",

                            title: response.responseText,

                            showConfirmButton: false,

                            // timer: 2000,

                        });
                        setTimeout(function () { location.reload(); }, 3000);

                    }

                    else {
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

                    console.log(response);
                    swal({
                        position: 'top-end',
                        type: "error",
                        title: "Server error ,kindly contact the admin for assistance",
                        showConfirmButton: false,
                        timer: 5000,
                    });

                }

            })

        });
}