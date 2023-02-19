
function Login() {
   
    let validateForm = true;

    if (
        $("#InputEmail").val() == "" ||
        $("#InputPassword").val() == ""
    ) {
        Swal.fire({
            icon: 'error',
            title: 'Failed',
            text: "Please fill out all your data",
        })
        validateForm = false
    }

    if (validateForm) {
        console.log("masuk");
        var Login = {};
        Login.Email = $("#InputEmail").val();
        Login.Password = $("#InputPassword").val();
        $.ajax({
            type: "POST",
            url: "https://localhost:7148/api/Accounts/login",
            data: JSON.stringify(Login),
            contentType: "application/json;charset=utf-8",
            success: (result) => {
                
                if (result.status == 200) {
                    localStorage.setItem("nik", result.data.nik);
                    localStorage.setItem("email", result.data.email);
                    localStorage.setItem("role", result.data.role);
                    localStorage.setItem("departement_Id", result.data.departementId);
                    localStorage.setItem("token", result.data.token);
                    window.location.href = "/dashboard/index"
                }
            },
            error: (result) => {
                
                if (result.status == 400 || result.status == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Failed',
                        text: result.responseJSON.message,
                    })
                }
            },
        })
        console.log("gagal");
    }
}

function Logout() {
    localStorage.clear();
    window.location.href = "/login/index"
}

function password_show_hide() {
    var x = document.getElementById("password");
    var show_eye = document.getElementById("show_eye");
    var hide_eye = document.getElementById("hide_eye");
    hide_eye.classList.remove("d-none");
    if (x.type === "password") {
        x.type = "text";
        show_eye.style.display = "none";
        hide_eye.style.display = "block";
    } else {
        x.type = "password";
        show_eye.style.display = "block";
        hide_eye.style.display = "none";
    }
}