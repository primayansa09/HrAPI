
function Insert() {
    let validateForm = true;

    if (
        $('#InputFirstName').val() == "" ||
        $('#InputLastName').val() == "" ||
        $('#InputPhone').val() == "" ||
        $('#InputEmail').val() == "" ||
        $('#InputBirthDate').val() == "" ||
        $('#InputSalary').val() == "" ||
        $('#InputGender').val() == "" ||
        $('#InputRole').val() == "" ||
        $('#InputDepartementId').val() == "" ||
        $('#InputPassword').val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    } else {
        if (!$("#InputPhone").val().match(/^\d*\d$/)) {
            Swal.fire({
                icon: 'error',
                title: 'Failed',
                text: "Sorry, your phone number is invalid",
            })
            validateForm = false
        }
    }

    if (validateForm) {
        var Register = {};
        Register.firstName = $('#InputFirstName').val();
        Register.lastName = $('#InputLastName').val();
        Register.phone = $('#InputPhone').val();
        Register.email = $('#InputEmail').val();
        Register.birthDate = $('#InputBirthDate').val();
        Register.salary = $('#InputSalary').val();
        Register.gender = parseInt($('#InputGender').val()); 
        Register.role_Id = $('#InputRoleId').val();
        Register.departement_Id = $('#InputDepartementId').val();
        Register.password = $('#InputPassword').val();

        console.log(Register)

        $.ajax({
            type: 'POST',
            url: 'https://localhost:7148/api/Employees/register',
            data: JSON.stringify(Register),
            contentType: "application/json; charset=utf-8",
            success: (result) => {
                if (result.status == 200 || result.status == 201) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Data successfully updated',
                    })
                    $('#tb_employee').DataTable().ajax.reload();
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: 'Data failed to update',
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
    } 
}

function SelectDepartement() {
    $.ajax({
        url: "https://localhost:7148/api/Departement",
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            var obj = result.data;

            for (var i = 0; i < obj.length; i++) {
                $('#InputDepartementId').append('<option value="' + obj[i].id + '">' + obj[i].name + '</option>');
            }
        },
        error: function (errorMessage) {
            alert(errorMessage.responseJSON);
        }
    });
}

function ClearAction() {
    $('#InputFirstName').val('');
    $('#InputLastName').val('');
    $('#InputEmail').val('');
    $('#InputPhone').val('');
    $('#InputBirthDate').val('');
    $('#InputSalary').val('');
    $('#InputGender').val('Gender');
    $('#InputManagerId').val('');
    $('#InputDepartementId').val('');
    $('#InputPassword').val('');
}

