
$(document).ready(function () {

    //GetSlotsByDate();

});

function GetRecord1(e) {
    debugger
    var id = e;

    swal(

        {
            title: "Are you sure?",

            text: "Once deleted, you will not be able to recover this  file!",

            type: "success",

            showCancelButton: true,

            confirmButtonColor: "##62b76e",

            confirmButtonText: "Yes, Procceed!",

            closeOnConfirm: false
        },
        function () {

            $.ajax({

                type: "GET",

                url: "/Admin/ParkingSlots/Delete/" + id,

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
                        $("#divLoader").hide();
                    }

                },
                error: function (response) {
                    swal({
                        position: 'top-end',
                        type: "error",
                        title: "Server error ,kindly contact the admin for asasasasasasasasasa",
                        showConfirmButton: false,
                        timer: 5000,
                    });
                    $("#divLoader").hide();
                }

            })

        }
    );
}
function DeleteRecord1(e) {
    debugger
    //$("#divLoader").show();
    var id = e;

    console.log(id);

    swal(

        {
            title: "Are you sure?",

            text: "Once deleted, you will not be able to recover this  file!",

            type: "success",

            showCancelButton: true,

            confirmButtonColor: "##62b76e",

            confirmButtonText: "Yes, Procceed!",

            closeOnConfirm: false
        },
        function () {

            $.ajax({

                type: "GET",

                url: "/Admin/ParkingSlots/Delete/" + id,

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
                        $("#divLoader").hide();
                    }

                },
                error: function (response) {
                    swal({
                        position: 'top-end',
                        type: "error",
                        title: "Server error ,kindly contact the admin for asasasasasasasasasa",
                        showConfirmButton: false,
                        timer: 5000,
                    });
                    $("#divLoader").hide();
                }

            })

        }
    );
}
$("#btnSubmit").click(function () {

    if ($('#txtName').val() == '') {
        $('#txtName').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Please enter first name",
            showConfirmButton: true,
        });

        return false;
    }

    $("#ModalCreateSlots").modal('hide');

    $("#divLoader").show();

    var formData = new FormData($('#frmAddSlots').get(0));

    $.ajax({
        type: "POST",
        url: "/Admin/ParkingSlots/Create", // NB: Use the correct action name
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,


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

            $("#divLoader").hide();
        },


        error: function (error) {
            alert("errror");
        }
    });

});
$("#btnUpdate").click(function () {


    if ($('#txtName1').val() == '') {
        $('#txtName1').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Please enter category name",
            showConfirmButton: true,
        });

        return false;
    }



    $("#ModalEditSlots").modal('hide');

    $("#divLoader").show();


    var formData = new FormData($('#frmUpdateSlot').get(0));

    $.ajax({
        type: "POST",
        url: "/Admin/ParkingSlots/Update", // NB: Use the correct action name
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,


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

            $("#divLoader").hide();
        },


        error: function (error) {
            alert("errror");
        }
    });

});
function GetRecord(e) {

    var id = e;

    $.get("/Admin/ParkingSlots/GetById/?Id=" + id, function (data, status) {

        console.log(data);
        if (data == null) {
            alert("Does not exist");
        } else {

            $("#txtId").val(data.data.id);
            $("#txtName1").val(data.data.name);


            $('#ModalEditSlots').modal({ backdrop: 'static', keyboard: false })
            $("#ModalEditSlots").modal('show');
        }

    });
};

//Allow users to enter numbers only
$(".numericOnly").bind('keypress', function (e) {
    if (e.keyCode == '9' || e.keyCode == '16') {
        return;
    }
    var code;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    if (e.which == 46)
        return false;
    if (code == 8 || code == 46)
        return true;
    if (code < 48 || code > 57)
        return false;
});

//Disable paste
$(".numericOnly").bind("paste", function (e) {
    e.preventDefault();
});

$(".numericOnly").bind('mouseenter', function (e) {
    var val = $(this).val();
    if (val != '0') {
        val = val.replace(/[^0-9]+/g, "")
        $(this).val(val);
    }
});


function GetSlotsByDate() {

    var date = $('#txtAppointmentDate').val();

    console.log(date)

    $("#txtAppDate").text(date);

    $.ajax({
        type: "GET",
        url: "/Admin/ParkingSlots/GetSlotsByAppointmentDate",
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
                    input.id = "txtTimeSlotId";
                    input.name = "TimeSlotId";
                    input.value = value.slotId;


                    label.appendChild(input);
                    slots.appendChild(label);

                    IsRadioChecked();

                });
            }

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
        document.getElementById("nextBtn").style.display = "inline";

    });
}