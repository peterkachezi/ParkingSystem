
function ShowLoader() {

    $("#loadMe").modal('show');
}

function HideLoader() {

    $("#loadMe").modal('hide');
}

$("#btnSubmitBlog").click(function () {

    if ($('#txtTitle').val() == '') {
        $('#txtTitle').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Title is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtContent').val() == '') {
        $('#txtContent').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Content is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtAuthor').val() == '') {
        $('#txtAuthor').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Author is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtBlogURL').val() == '') {
        $('#txtBlogURL').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Blog URL is a required field",
            showConfirmButton: true,
        });
        return false;
    }


    var data = $("#frmBlogs").serialize();

    $.ajax({

        type: "POST",

        url: "/Admin/Blogs/Create/",

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


$("#btnUpdateBlogs").click(function () {



    if ($('#txtTitle1').val() == '') {
        $('#txtTitle1').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Title is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtContent1').val() == '') {
        $('#txtContent1').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Content is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtAuthor1').val() == '') {
        $('#txtAuthor1').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Author is a required field",
            showConfirmButton: true,
        });
        return false;
    }

    if ($('#txtBlogURL1').val() == '') {
        $('#txtBlogURL1').focus();
        swal({
            position: 'top-end',
            type: "error",
            title: "Blog URL is a required field",
            showConfirmButton: true,
        });
        return false;
    }


    var data = $("#frmUpdateBlogs").serialize();

    $.ajax({

        type: "POST",

        url: "/Admin/Blogs/Update/",

        data: data,

        beforeSend: function () { ShowLoader(); },

        success: function (response) {

            if (response.success) {


                swal({

                    position: 'top-end',

                    type: "success",

                    title: response.responseText,

                    showConfirmButton: false,

                }), setTimeout(function () { window.location.href = "/Admin/Blogs/"; }, 3000);

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


function GetBlog(e) {

    var id = e;

    window.location.href = "/Admin/Blogs/UpdateBlogs/" + id;
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

                url: "/Admin/Blogs/Delete/" + id,

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