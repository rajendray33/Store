$(document).ready(function () {


    //Login Ajax
    $(document).on('click', '#LoginBtn', function (e) {
        e.preventDefault(); // Prevent form submission

        // Perform validation
        if (customValidateLoginForm()) {
            var userdata = {
                Email: $('#Uemail').val(),
                Password: $('#Upassword').val()
            };

            $.ajax({
                type: 'POST',
                url: '/LoginHome/signin',
                data: JSON.stringify(userdata),
                contentType: 'application/json',
                success: function (response) {
                    if (response.success) {
                        // Redirect to dashboard on success
                        window.location.href = "/Dashbord/Home";
                    } else {
                        // Show error message
                        alertify.error('Incorrect Details');
                        $('#Uemail').css('border', '1px groove red');
                        $('#Upassword').css('border', '1px groove red');
                    }
                },
                error: function (xhr, status, error) {
                    // Handle error response here
                    alert("An error occurred: " + status + " " + error);
                }
            });
        }
    });


    //Registraton PopUp

    $(document).on('click', '#AddNewBtn', function () {
        debugger;
        $.ajax({
            type: 'Get',
            url: '/LoginHome/_AddNew',
            Cache: false,
            success: function (responce) {

                $('#AddNewPopUp').html(responce);
                $('#myModal').modal('toggle');
            },

        })
    });


    //Registration Add User Data Ajax
    $(document).on('click', '#SubmitBtn', function (event) {
        event.preventDefault();  // Prevent default form submission
        debugger
        if (customValidateForm()) {
            // Form is valid, proceed with form submission via AJAX
            const userData = {
                Name: $('#Uname').val(),
                Email: $('#URemail').val(),
                Password: $('#Upaasword').val(),
                Phone_Num: $('#Unumber').val(),
                Role: $('#Urole').val()
            };

            $.ajax({
                type: 'POST',
                url: '/LoginHome/_AddNew',
                contentType: 'application/json',
                data: JSON.stringify(userData),
                success: function (response) {
                    if (response == "Ema") {
                        alertify.error("Email already exists");
                        $('#URemail').css("border", "2px solid rgb(251, 0, 0)")
                    } else if (response == "mob") {
                        alertify.error("Mobile number already exists");
                        $('#Unumber').css("border", "2px solid rgb(251, 0, 0)")
                    } else {
                        $('#myModal').modal('toggle');
                        window.location.href = "/LoginHome/signin", 1000;
                        alertify.success("Submitted successfully", 2000);
                    }
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }
    });


    $(document).on('click', '#btnn', function () {
        debugger;
        alertify.success('okok')
    })

    //Add Products Popup Ajax
    $(document).on('click', '#AddProductPopUp', function () {
        debugger;

        $.ajax({
            type: 'Get',
            url: '/Dashbord/_AddProduct',
            cache: false,
            success: function (responce) {
                $('#AddCategoryPopup').html(responce);
                GetProductList();
            },

        })
    })

    //Product Add Post Ajax
    $(document).on('click', '#AddProductBtn', function (event) {
        debugger;
        if (AddProductvalidatoin()) {
            var productname = $('#CategoryName').val();

            $.ajax({
                type: 'POST',
                url: '/Dashbord/_AddProduct',
                contentType: 'application/json',
                data: JSON.stringify({ Product_Category: productname }),
                success: function (response) {
                    if (response == "Exit") {
                        alertify.error("Product Category Already Exit");
                        $('#CategoryName').css("border", "2px solid rgb(255, 0, 0)");

                    }
                    else if (response.success) {
                        GetProductList();
                        $('#CategoryName').val("");
                        alertify.success("Product Category Added Succesfully");
                        $('#CategoryName').css("border", "2px solid rgb(122, 245, 71)");
                       
                    }
                    else {
                        alertify.error("Fill The Product Name");
                    }
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }

    });


    //Product Category Edit Ajax
    $(document).on('click', '#EditProductCategory', function () {
        debugger;
        var id = $(this).data('id');
        $.ajax({
            type: 'GET',
            url: '/Dashbord/_EditCategory',
            data: { Pid: id }, 
            success: function (responce) {
                $('#EditCategoryPopup').html(responce);
                $('#myModalEdit').modal('toggle');
                //GetProductList();
            },

        })
    })
});





















function GetProductList() {
    $.ajax({
        type: 'Get',
        url: '/Dashbord/_ProductGetLIst',
        cache: false,
        success: function (responce) {
            $('#ProductListContainer').html(responce);
        },

    })
}
//Add Product Caegory Validaton
function AddProductvalidatoin() {
    let isValid = true;
    debugger;
    // Reset previous error messages and borders
    $(".error").remove();
    $("input").css("border", "");

    // Validate Product Name
    const productName = $("#CategoryName").val();
    if (productName.length < 3 || productName.length > 50) {
        $("#CategoryName").css("border", "2px solid rgb(255, 0, 0)");
        alertify.error('Product Name must be between 3 and 50 characters long');
        isValid = false;
    } else {
        $("#CategoryName").css("border", "2px solid rgb(122, 245, 71)");
    }

    return isValid;
}


//Validations Code Here
//Login Validaton Funcation

function customValidateLoginForm() {
    let isValid = true;
    debugger;
    // Reset previous error messages and borders
    $(".error").remove();
    $("input").css("border", "");

    // Validate Email
    const email = $("#Uemail").val();
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        $("#Uemail").css("border", "2px solid red");
        alertify.error('Please Enter A Valid Email Address');
        isValid = false;
    } else {
        $("#Uemail").css("border", "2px solid green");
    }

    // Validate Password
    const password = $("#Upassword").val();
    if (password.length < 6 || password.length > 15) {
        $("#Upassword").css("border", "2px solid red");
        alertify.error('Password Bust Be Between 6 And 15 Characters Long');
        isValid = false;
    } else {
        $("#Upassword").css("border", "2px solid green");
    }

    return isValid;
}

//Registration vAlidation
function customValidateForm() {
    let isValid = true;
    debugger;
    // Reset previous error messages and borders
    $(".error").remove();
    $("input").css("border", "");

    // Validate Name
    const name = $("#Uname").val();
    if (name.length < 3 || name.length > 20) {
        $("#Uname").css("border", "2px solid red");
        alertify.error('Name must be between 3 and 15 characters long');
        isValid = false;
    } else {
        $("#Uname").css("border", "2px solid green");
    }

    // Validate Email
    const email = $("#URemail").val();
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        $("#URemail").css("border", "2px solid red");
        alertify.error('Please enter a valid email address');
        isValid = false;
    } else {
        $("#URemail").css("border", "2px solid green");
    }



    // Validate Password
    const password = $("#Upaasword").val();
    if (password.length < 6 || password.length > 15) {
        $("#Upaasword").css("border", "2px solid red");
        alertify.error('Password must be between 6 and 15 characters long');
        isValid = false;
    } else {
        $("#Upaasword").css("border", "2px solid green");
    }

    // Validate Phone
    const phone = $("#Unumber").val();
    const phoneRegex = /^[789]\d{9}$/;  // Example regex for Indian mobile numbers
    if (!phoneRegex.test(phone)) {
        $("#Unumber").css("border", "2px solid red");
        alertify.error('Please enter a valid 10-digit mobile number');
        isValid = false;
    } else {
        $("#Unumber").css("border", "2px solid green");
    }

    // Validate Role
    const role = $("#Urole").val();
    if (role === "-----User Role-----") {
        $("#Urole").css("border", "2px solid red");
        alertify.error('Please select a valid role');
        isValid = false;
    } else {
        $("#Urole").css("border", "2px solid green");
    }

    return isValid;
}