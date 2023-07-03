
function ShowLoader() {

    $("#loadMe").modal('show');
}

function HideLoader() {

    $("#loadMe").modal('hide');
}

$("#btnSubmit").click(function () {



    if ($('#txtName').val() == '') {
        $('#txtName').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Name is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtAmount').val() == '') {
        $('#txtAmount').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Amount is a required field",
            showConfirmButton: true,
        });
        return false;
    }


    $("#ModalCreateParkingFee").modal('hide');

    var data = $("#frmAddParkingFee").serialize();

    $.ajax({

        type: "POST",

        url: "/Admin/ParkingFees/Create/",

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

})


$("#btnUpdate").click(function () {



    if ($('#txtName1').val() == '') {
        $('#txtName1').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Name is a required field",
            showConfirmButton: true,
        });
        return false;
    }


    if ($('#txtAmount1').val() == '') {
        $('#txtAmount1').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Amount is a required field",
            showConfirmButton: true,
        });
        return false;
    }


    $("#ModalUpdateParkingFee").modal('hide');

    var data = $("#frmUpdateParkingFee").serialize();

    $.ajax({

        type: "POST",

        url: "/Admin/ParkingFees/Update/",

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

})













function GetRecord(e) {

    var id = e;

    console.log(id);

    $.get("/Admin/ParkingFees/GetById/?Id=" + id, function (data, status) {
        console.log(data);
        if (data.data == false) {
            alert("Does not exist");
        } else {

            $("#txtId1").val(data.data.id);

            $("#txtName1").val(data.data.parkingFee);

            $('#ModalUpdateParkingFee').modal({ backdrop: 'static', keyboard: false })

            $("#ModalUpdateParkingFee").modal('show');

        }

    });
};


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

                url: "/Admin/ParkingFees/Delete/" + id,

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