$(document).ready(function () {
    $('#tb_departement').DataTable({
        "ajax": {
            url: "https://localhost:7148/api/Departement",
            type: "GET",
            dataType: "json",
            dataSrc: "data"
        },
        "columns": [
            {
                render: function (data, tye, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "name" },
            { "data": "manager_Id" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="BtnAction(\'Update\');return GetById(' + row.id + ')"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(' + row.id + '); return Delete()"><i class="fa fa-trash"></i></button >'

                }
            }
        ]
    })
})

function Insert() {
    let validateForm = true;

    if ($('#txtDepartement').val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }

    if (validateForm) {
        var insert = new Object();
        insert.name = $('#txtDepartement').val();
        $.ajax({
            url: "https://localhost:7148/api/Departement",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.status == 200 || data.status == 201) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Data successfully insert',
                    })
                    $('#tb_departement').DataTable().ajax.reload();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: 'Data failed to insert',
                    })
                }
            },
            "error": (data) => {
                if (data.status == 400 || data.status == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: result.responseJSON.message,
                    })
                }
            }
        })
    }
}

function GetById(id) {
    $.ajax({
        url: "https://localhost:7148/api/Departement" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //debugger;
            //console.log(result);
            var obj = result.data;
            $('#InputId').val(obj.id);
            $('#InputName').val(obj.name);
            $('#InputManagerId').val(obj.manager_Id);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    })
}

function ConfirmDelete(id) {
    Swal.fire({
        title: 'Delete Data?',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Delete!'
    }).then((result) => {
        if (result.isConfirmed) {
            Delete(id);
            Swal.fire(
                'Data successfully deleted',
                '.',
                'success'
            )
        }
    })
}

function Delete(id) {
    //debugger;
    $.ajax({
        url: "https://localhost:7148/api/Departement" + id,
        type: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    }).then((result) => {
        /*debugger;*/
        if (result.status == 200) {
            $('#tb_departement').DataTable().ajax.reload();
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Data successfully deleted',
            })
            $('#tb_departement').DataTable().ajax.reload();
        }
        else {
            alert("Failed to delete data");
        }
    });
}

function SelectDepartement() {
    console.log("masuk");
    $.ajax({
        url: "https://localhost:7148/api/Departement",
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
             var obj = result.data;
            console.log("berhasil");
            //data.forEach(element => {
            //    $('#DepartementId').append('<option value="' + element.id + '">' + element.name + '</option>');
            //})

            for (var i = 0; i < obj.length; i++) {
                $('#').append('<option value="' + obj[i].id + '">' + obj[i].name + '</option>');
            }

        },
        error: function (error) {
            alert(error.responseJSON);
        }
    })
    console.log("gagal");
}

function ClearAction() {
    $('#txtDepartement').val('');
}

function BtnAction(type) {
    var btnSave = 'none';
    var btnUpdate = 'none';
    var message = '';

    switch (type) {
        case 'Insert':
            btnSave = 'block';
            btnUpdate = 'none';
            message = 'Add new employee';
            break;
        case 'Update':
            btnSave = 'none';
            btnUpdate = 'block';
            message = 'Update employee';
            break;
        default:
            break;
    }

    document.getElementById('Save').style.display = btnSave;
    document.getElementById('Update').style.display = btnUpdate;
}