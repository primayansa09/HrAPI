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
    console.log("masuk");
    let validateForm = true;

    if (
        $("#NameDepartement").val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }

    if (validateForm) {
        console.log("berhasil");
        var Insert = new Object();
        Insert.name = $("#NameDepartement").val();
        Insert.manager_Id = $('#ManagerId').val();
        $.ajax({
            url: "https://localhost:7148/api/Departement",
            type: "POST",
            data: JSON.stringify(Insert),
            contentType: "application/json; charset=utf-8",
            success: (result) => {

                if (result.status == 200) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Insert Success',
                    })
                    $('#tb_departement').DataTable().ajax.reload();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: 'Insert Failed',
                    })
                }
            },
            "error": (result) => {
                if (result.status == 400 || result.status == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: result.responseJSON.message,
                    })
                }
            }
        })
        console.log("gagal");
    }

}

function GetById(id) {
    $.ajax({
        url: "https://localhost:7148/api/Departement/" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //debugger;
            //console.log(result);
            var obj = result.data;
            $('#Id').val(obj.id);
            $('#NameDepartement').val(obj.name);
            $('#ManagerId').val(obj.manager_Id);
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

function Update() {
    var validateForm = true;

    if (
        $('#NameDepartement').val();
        $('ManagerId').val();
      ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }else {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Sorry, your phone number is invalid",
        })
        validateForm = false
    }

    var Update = new Object();
    Update.Id = $('#Id').val();
    Update.Name = $('#NameDepartement').val();
    Update.manager_Id = $('#ManagerId').val();
    $.ajax({
        url: 'https://localhost:7148/api/Departement',
        type: 'PUT',
        data: JSON.stringify(Update),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        /*debugger;*/
        if (result.status == 200) {
            $('#tb_departement').DataTable().ajax.reload();
            Swal.fire(
                'Update Success',
                '',
                'success'
            )

        }
        else {
            alert("Update Failed");
        }
    });
}

function Delete() {
    //debugger;
    $.ajax({
        url: "https://localhost:7148/api/Departement",
        type: "DELETE",
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

function ClearAction() {
    $('#NameDepartement').val('');
}

function BtnAction(type) {
    var btnSave = 'none';
    var btnUpdate = 'none';
    var fieldManager = 'none'
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
            fieldManager = 'block'
            message = 'Update employee';
            break;
        default:
            break;
    }

    document.getElementById('Save').style.display = btnSave;
    document.getElementById('Update').style.display = btnUpdate;
    document.getElementById('ManagerId').style.display = fieldManager;
}