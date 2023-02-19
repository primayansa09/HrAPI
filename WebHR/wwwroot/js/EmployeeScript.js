$(document).ready(function () {
    $('#tb_employee').DataTable({
        "ajax": {
            url: "https://localhost:7148/api/Employees",
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
            { "data": "nik" },
            {
                "data": "firstName",
                "render": function (data, type, full, meta) {
                    return `${full.firstName} ${full.lastName}`;
                }
            },
            { "data": "phone" },
            { "data": "birthDate" },
            { "data": "salary" },
            { "data": "email" },
            {
                "data": "gender",
                "render": function (data) {
                    if (data == 0) { return "Male" }
                    if (data == 1) { return "Female" }
                }
            },
            { "data": "manager_Id" },
            {
                "data": "departement_Id",
                "render": function (data) {
                    if (data == 1) { return "IT Support" }
                    if (data == 2) { return "RAS" }
                    if (data == 3) { return "RPA" }
                    if (data == 4) { return "Consulting Service" }
                    if (data == 7) { return "HR" }

                }
            },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="GetById(' + row.nik + ')"><i class="fa fa-pen"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="ConfirmDelete(' + row.id + '); return Delete()"><i class="fa fa-trash"></i></button >'

                }
            }
        ]
    })
})

function GetById(nik) {
    $.ajax({
        url: "https://localhost:7148/api/Employees/" + nik,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //debugger;
            var obj = result.data;
            var gender = obj.gender == 0 ? "Male" : "Famale"
            var departName = obj.departement_Id;

            if (departName == 1) { return "IT Support" }
            if (departName == 2) { return "RAS" }
            if (departName == 3) { return "RPA" }
            if (departName == 4) { return "Consulting Service" }
            if (departName == 7) { return "HR" }

            $('#InputNIK').val(obj.nik);
            $('#InputFirstName').val(obj.firstName);
            $('#InputLastName').val(obj.lastName);
            $('#InputPhone').val(obj.phone);
            $('#InputBirthDate').val(obj.birthDate.slice(0, 10));
            $('#InputSalary').val(obj.salary);
            $('#InputEmail').val(obj.email);
            $('#InputGender').val(gender);
            $('#InputManagerId').val(obj.manager_Id);
            $('#InputDepartementId').val(obj.departName);
            $('#myModal').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    })
}

function Update() {
    var Employee = new Object();
    Employee.nik = $('#InputNIK').val();
    Employee.firstName = $('#InputFirstName').val();
    Employee.lastName = $('#InputLastName').val();
    Employee.phone = $('#InputPhone').val();
    Employee.birthDate = $('#InputBirthDate').val();
    Employee.salary = $('#InputSalary').val();
    Employee.email = $('#InputEmail').val();
    Employee.gender = $('#InputGender').val();
    Employee.manager_Id = $('#InputManagerId').val();
    Employee.departement_Id = $('#InputDepartementId').val();
    $.ajax({
        url: 'https://localhost:7148/api/Employees/',
        type: 'PUT',
        data: JSON.stringify(Employee),
        contentType: "application/json; charset=utf-8",
    }).then((result) => {
        if (result.status == 200) {
            $('#tb_employee').DataTable().ajax.reload();
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
        url: "https://localhost:7148/api/Employees/" + id,
        type: "DELETE",
        dataType: "json",
    }).then((result) => {
        /*debugger;*/
        if (result.status == 200) {
            $('#tb_employee').DataTable().ajax.reload();
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: 'Data successfully deleted',
            })
            $('#tb_employee').DataTable().ajax.reload();
        }
        else {
            alert("Failed to delete data");
        }
    });
}

//function BtnAction(type) {
//    var btnSave = 'none';
//    var btnUpdate = 'none';
//    var txtPassword = 'none';
//    var message = '';

//    switch (type) {
//        case 'Insert':
//            btnSave = 'block';
//            btnUpdate = 'none';
//            txtPassword = 'block'
//            message = 'Add new employee';
//            break;
//        case 'Update':
//            btnSave = 'none';
//            btnUpdate = 'block';
//            txtPassword = 'none';
//            message = 'Update employee';
//            break;
//        default:
//            break;
//    }

//    document.getElementById('Save').style.display = btnSave;
//    document.getElementById('Update').style.display = btnUpdate;
//    document.getElementById('InputPassword').style.display = txtPassword;
//}