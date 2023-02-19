
function Insert() {
    let validateForm = true;

    if (
        $('#txtFirstName').val() == "" ||
        $('#txtLastName').val() == "" ||
        $('#txtPhone').val() == "" ||
        $('#txtEmail').val() == "" ||
        $('#txtBirthDate').val() == "" ||
        $('#txtSalary').val() == "" ||
        $('#txtGender').val() == "" ||
        $('#txtRoleId').val() == "" ||
        $('#txtDepartementId').val() == "" ||
        $('#txtPassword').val() == ""
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
        Register.firstName = $('#txtFirstName').val();
        Register.lastName = $('#txtLastName').val();
        Register.phone = $('#txtPhone').val();
        Register.email = $('#txtEmail').val();
        Register.birthDate = $('#txtBirthDate').val();
        Register.salary = $('#txtSalary').val();
        Register.gender = parseInt($('#txtGender').val()); 
        Register.role_Id = $('#txtRoleId').val();
        Register.departement_Id = $('#txtDepartementId').val();
        Register.password = $('#txtPassword').val();

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
                        text: 'Data successfully register',
                    })
                    $('#tb_employee').DataTable().ajax.reload();
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: 'Data failed to register',
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

function ClearAction() {
    $('#txtFirstName').val('');
    $('#txtLastName').val('');
    $('#txtEmail').val('');
    $('#txtPhone').val('');
    $('#txtBirthDate').val('');
    $('#txtSalary').val('');
    $('#txtGender').val('Gender');
    $('#txtDepartementId').val('');
    $('#txtInputPassword').val('');
}

