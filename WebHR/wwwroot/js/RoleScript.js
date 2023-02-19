$(document).ready(function () {
    $('#tb_role').DataTable({
        "ajax": {
            url: "https://localhost:7148/api/Roles",
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
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="BtnAction(\'Update\');return GetById(' + row.id + ')"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(' + row.id + '); return Delete()"><i class="fa fa-trash"></i></button >'

                }
            }
        ]
    })
})

function GetById(id) {
    $.ajax({
        url: "https://localhost:7148/api/Roles" + id,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //debugger;
            //console.log(result);
            var obj = result.data;
            $('#Id').val(obj.id);
            $('#RoleId').val(obj.name);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    })
}

function Insert() {
    var validateFrom = true;

    if (
        $('#RoleId').val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }

    if (validateFrom) {
        var Role = new Object();
        Role.name = $('#RoleId').val();
        $.ajax({
            type: 'POST',
            url: 'https://localhost:7148/api/Roles',
            data: JSON.stringify(Role),
            contentType: "application/json; charset=utf-8"
        }).then((result) => {
          /*  debugger;*/
            if (result.status == 200) {
                $('#tb_role').DataTable().ajax.reload();
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Data successfully insert',
                })
                $('#tb_role').DataTable().ajax.reload();
                $('#myModal').modal("hide");
            } else {
                alert("Data failed to insert");
            }
        })
    }
}

function Update() {
    var Role = new Object();
    Role.id = $('#Id').val();
    Role.name = $('#RoleId').val();
    $.ajax({
        url: 'https://localhost:7148/api/Roles',
        type: 'PUT',
        data: JSON.stringify(Role),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            $('#tb_role').DataTable().ajax.reload();
            Swal.fire(
                'Update Success',
                '',
                'success'
            )

        }
        else {
            alert("Failed to update data");
        }
    });
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
        url: "https://localhost:7148/api/Roles/" + id,
        type: "DELETE",
        dataType: "json",
    }).then((result) => {
        /*debugger;*/
        if (result.status == 200) {
            $('#tb_role').DataTable().ajax.reload();
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Data successfully deleted',
            })
            $('#tb_role').DataTable().ajax.reload();
        }
        else {
            alert("Failed to delete data");
        }
    });
}

function ClearAction() {
    $('#RoleId').val('');

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