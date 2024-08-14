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
                        $('#URemail').css("border", "1.5px solid rgb(251, 0, 0)")
                    } else if (response == "mob") {
                        alertify.error("Mobile number already exists");
                        $('#Unumber').css("border", "1.5px solid rgb(251, 0, 0)")
                    } else {
                        $('#myModal').modal('toggle');
                        window.location.href = "/LoginHome/signin", 2000;
                        alertify.success("Submitted successfully", 2000);
                    }
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }
    });


    $(document).on('click', '#ForgetPassword', function () {
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
                        $('#CategoryName').css("border", "1.5px solid rgb(255, 0, 0)");

                    }
                    else if (response.success) {
                        GetProductList();
                        $('#CategoryName').val("");
                        alertify.success("Product Category Added Succesfully");
                        $('#CategoryName').css("border", "1.5px solid rgb(122, 245, 71)");

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
    });

    //Category Update Post Ajax
    $(document).on('click', '#BtnUpdate', function (event) {
        debugger;
        if (EditProductvalidatoin()) {

            var productname = $('#UCategory').val();
            var pid = $('#UP_id').val();
            $.ajax({
                type: 'POST',
                url: '/Dashbord/_EditCategory',
                contentType: 'application/json',
                data: JSON.stringify({ Product_Category: productname, P_Id: pid }),
                success: function (response) {
                    if (response.success) {
                        GetProductList();
                        $('#myModalEdit').modal('toggle');
                        alertify.success('Category Updated Successfully')
                    }
                    else {
                        alertify.error('Category Allready Exit')
                        $('#UCategory').css("border", "1.5px solid rgb(255, 0, 0)");

                    };
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }

    });

    //Delete Category Ajax
    $(document).on('click', '#DeleteCategory', function () {
        var id = $(this).data('id');

        alertify.confirm('Delete Confirmation', 'Are you sure you want to delete this Product?',
            function () { // If confirmed
                $.ajax({
                    type: 'POST',
                    url: '/Dashbord/_DeleteCategory?Cid=' + id, // Query string format
                    success: function (response) {
                        if (response.success) {
                            alertify.success("Category deleted successfully");
                            GetProductList();
                        } else {
                            alertify.error("Failed to delete category: " + response.message);
                        }
                    },
                    error: function () {
                        alertify.error('There was an error submitting the form.');
                    }
                });
            },
            function () { // If canceled
                alertify.error('Delete action canceled.');
            });
    });


    //Add Sub Product Get Ajax
    $(document).on('click', '#AddSubProductPopUp', function () {
        debugger;
        $.ajax({
            type: 'GET',
            url: '/Dashbord/_AddSubProduct',
            cache: false,
            success: function (responce) {
                $('#AddCategoryPopup').html(responce);
                GetSubProductList();

            },

        })
    });


    //Add SubProduct Post AJax
    $(document).on('click', '#AddSubProductBtn', function () {
        debugger;
        if (Add_Sub_Productvalidatoin()) {
            var userdata = {
                Sub_Product_Name: $('#SubPN').val(),
                P_Id: $('#SelectCate').val()
            };
            $.ajax({
                type: 'POST',
                url: '/Dashbord/_AddSubProduct',
                contentType: 'application/json',
                data: JSON.stringify(userdata),
                success: function (response) {
                    GetSubProductList();
                    if (response.success) {
                        $('#SubPN').val("");
                        $('#SelectCate').val("--Select Category--");
                        $('#SubPN').css("border", "1.5px solid rgb(122, 245, 71)");
                        $('#SelectCate').css("border", "1.5px solid rgb(122, 245, 71)");
                        alertify.success("Sub Product Added Succesfully");
                    }
                    else {
                        alertify.error("Sub Product Already Exit");
                        $('#SubPN').css("border", "1.5px solid rgb(255, 0, 0)");
                    }
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }

    });

    //Edit SubProduct Get Ajax
    $(document).on('click', '#EditSubProduct', function () {
        debugger;
        var id = $(this).data('id');
        $.ajax({
            type: 'GET',
            url: '/Dashbord/_EditSubProduct',
            data: { SubPId: id },
            success: function (responce) {
                if (responce.success) {
                    alertify.error('Sub Product Not Exit');
                }
                else {
                    $('#EditSubProductPopup').html(responce);
                    $('#myModalEdit').modal('toggle');
                }


            },

        })
    });


    //Sub Product Update Post Ajax 
    $(document).on('click', '#BtnUpdate_SubProduct', function (event) {
        debugger;
        if (Update_Sub_Productvalidatoin()) {
            var userdata = {
                Sub_P_Id: $('#Sub_P_UpdateId').val(),
                P_Id: $('#U_SubProductCategory').val(),
                Sub_Product_Name: $('#USub_Product').val()
            };

            $.ajax({
                type: 'POST',
                url: '/Dashbord/_EditSubProduct',
                contentType: 'application/json',
                data: JSON.stringify(userdata),
                success: function (response) {
                    if (response.success) {
                        GetSubProductList();
                        $('#myModalEdit').modal('toggle');
                        alertify.success('Sub Product Updated Successfully');
                    } else {
                        alertify.error('Category Already Exists');
                        $('#U_SubProductCategory').css("border", "1.5px solid rgb(255, 0, 0)");
                        $('#USub_Product').css("border", "1.5px solid rgb(255, 0, 0)");
                    }
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }
    });

    //Delete Sub Product  
    $(document).on('click', '#DeleteSubProduct', function () {
        debugger
        var id = $(this).data('id');

        alertify.confirm('Delete Confirmation', 'Are you sure you want to delete this Product?',
            function () { // If confirmed
                $.ajax({
                    type: 'POST',
                    url: '/Dashbord/_DeleteSubProduct?SpId=' + id, // Query string format
                    success: function (response) {
                        if (response.success) {
                            alertify.success("Product Deleted successfully");
                            GetSubProductList();
                        } else {
                            alertify.error("Failed to delete Product: " + response.message);
                        }
                    },
                    error: function () {
                        alertify.error('There was an error submitting the form.');
                    }
                });
            },
            function () { // If canceled
                alertify.error('Delete action canceled.');
            });
    });



    //Stock Iteam Add  Popup Ajax
    $(document).on('click', '#AddIteamPopup', function () {
        debugger;
        $.ajax({
            type: 'Get',
            url: '/Dashbord/_AddStockIteams',
            cache: false,
            success: function (responce) {
                $('#AddCategoryPopup').html(responce);
                DDlCategory();

            },
        })
    });

    //Add Item post Ajax
    $(document).on('click', '#AddItemBtn', function () {
        debugger;
        if (Add_Item_Productvalidatoin()) {
            var userdata = {
                S_P_Id: $('#ItmSelectSub').val(),
                P_Id: $('#ItmSelectCate').val(),
                Item_Name: $('#ItmName').val(),
                Item_SerialNumber: $('#ItmSrNumber').val(),
                Item_Quantity: $('#ItmQuntity').val(),
                Item_Price: $('#ItmPrice').val(),
                Item_Selling_Price: $('#ItmSellingPrice').val(),
                Item_Expiry_Date: $('#ItmExpiryDate').val()
            };
            $.ajax({
                type: 'POST',
                url: '/Dashbord/_AddStockIteams',
                contentType: 'application/json',
                data: JSON.stringify(userdata),
                success: function (response) {
                    if (response.success) {
                        //GetSubProductList();
                        alertify.success('Item Adedes Successfully');
                        restAll();
                    } else {
                        alertify.error('Item Not Added', 1000);

                    }
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }
    })

    //DDl Sub Product GEt Using ajax
    $(document).on('change', '#ItmSelectCate', function () {
        var CategoryId = $(this).val();
        debugger;
        if (CategoryId > 0) {
            $.ajax({
                type: 'GET',
                url: '/Dashbord/_SubproductListItm',
                data: { CategoryId: CategoryId },
                cache: false,
                success: function (response) {
                    $('#ItmSelectSub').empty();
                    $('#ItmSelectSub').append('<option value="">--Select Sub Product--</option>');

                    if (response != null && response.length > 0) {
                        $.each(response, function (index, item) {
                            $('#ItmSelectSub').append('<option value="' + item.value + '">' + item.text + '</option>');
                        });
                    } else {
                        $('#ItmSelectSub').append('<option value="">No Subcategories Found</option>'); // Optional: Message if no subcategories
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error); // Error handling
                }
            });
        }
        else {
            alertify.error("Please select a valid category.");
        }
    });



    $(document).on('click', '#ViewStockPopUp', function () {
        debugger;
        $.ajax({
            type: 'GET',
            url: '/Dashbord/_StockDetail',
            cache: false,
            success: function (responce) {
                $('#ViewStock').html(responce);
                DDlCategory();
            },

        })
    })

});






//DDl Category Itm
function DDlCategory() {
    var dropdowns = '#ItmSelectCate, #V_Category';
    $.ajax({
        type: 'GET',
        url: '/Dashbord/DDlCategory',
        cache: false,
        success: function (response) {
            $(dropdowns).empty();
            $(dropdowns).append('<option value="">--Select Category--</option>'); // Ensure the default option value is empty

            if (response && response.length > 0) {
                $.each(response, function (index, item) {
                    $(dropdowns).append('<option value="' + item.value + '">' + item.text + '</option>');
                });
            } else {
                $('#ItmSelectCate').append('<option value="">No Subproducts Found</option>');
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}

















//-----------------List Get Code Here--^^^^^^^----------------------------

//Get Category List Ajax
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

//Get Sub Product List Ajax
function GetSubProductList() {
    $.ajax({
        type: 'Get',
        url: '/Dashbord/_SubProductList',
        cache: false,
        success: function (responce) {
            $('#SubProductListContainer').html(responce);
            //$('#SubProductTable').DataTable();
            //let table = new DataTable('#SubProductTable', {
            //    //columnDefs: [
            //    //    { targets: [1, 2, 3, 4, 5, 6, 7], searchable: false } // Only the Name column (index 0) is searchable
            //    //]
            //});
        },
    })
}

//-----------------List Get Code Here--^^^^^^^----------------------------




















//Add Product Category Validaton
function EditProductvalidatoin() {
    let isValid = true;
    debugger;
    // Reset previous error messages and borders
    $(".error").remove();
    $("input").css("border", "");

    // Validate Product Name
    const productName = $("#UCategory").val();
    if (productName.length < 3 || productName.length > 50) {
        $("#UCategory").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Product Name must be between 3 and 50 characters long');
        isValid = false;
    } else {
        $("#UCategory").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    return isValid;
}


//Sub Product Validation
function Add_Sub_Productvalidatoin() {
    let isValid = true;

    // Reset previous error messages and borders
    $(".error").remove();
    $("input, select").css("border", "");

    // Validate Category
    const category = $("#SelectCate").val();
    if (category == "--Select Category--") {
        $("#SelectCate").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a category');
        isValid = false;
    }
    else {
        $("#SelectCate").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Sub Product Name
    const subProductName = $("#SubPN").val();
    if (subProductName.length < 3 || subProductName.length > 50) {
        $("#SubPN").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Sub Product Name must be between 3 and 50 characters long');
        isValid = false;
    } else {
        $("#SubPN").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    return isValid;
}

//Sub Product Update Validation

function Update_Sub_Productvalidatoin() {
    let isValid = true;

    // Reset previous error messages and borders
    $(".error").remove();
    $("input, select").css("border", "");

    // Validate Category
    const category = $("#U_SubProductCategory").val();
    if (category == "--Select Category--") {
        $("#U_SubProductCategory").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a category');
        isValid = false;
    }
    else {
        $("#U_SubProductCategory").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Sub Product Name
    const subProductName = $("#USub_Product").val();
    if (subProductName.length < 3 || subProductName.length > 50) {
        $("#USub_Product").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Sub Product Name must be between 3 and 50 characters long');
        isValid = false;
    } else {
        $("#USub_Product").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    return isValid;
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
        $("#CategoryName").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Product Name must be between 3 and 50 characters long');
        isValid = false;
    } else {
        $("#CategoryName").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    return isValid;
}


// Add Item Validation
function Add_Item_Productvalidatoin() {
    let isValid = true;

    // Reset previous error messages and borders
    $(".error").remove();
    $("input, select").css("border", "");

    // Validate Category
    const category = $("#ItmSelectCate").val();
    if (category === "" || category === "--Select Sub Product--") {
        $("#ItmSelectCate").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a category');
        isValid = false;
    } else {
        $("#ItmSelectCate").css("border", "1.5px solid rgb(122, 245, 71)");
    }



    // Validate Sub Product
    const subProduct = $("#ItmSelectSub").val();
    if (subProduct == "--Select Sub Product--") {
        $("#ItmSelectSub").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a sub product');
        isValid = false;
    } else {
        $("#ItmSelectSub").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Item Name
    const itemName = $("#ItmName").val();
    if (itemName.length < 3 || itemName.length > 50) {
        $("#ItmName").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Item Name must be between 3 and 50 characters long');
        isValid = false;
    } else {
        $("#ItmName").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Item Serial Number
    const itemSerialNumber = $("#ItmSrNumber").val();
    if (itemSerialNumber.length < 3 || itemSerialNumber.length > 50) {
        $("#ItmSrNumber").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Item Serial Number must be between 3 and 50 characters long');
        isValid = false;
    } else {
        $("#ItmSrNumber").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Item Quantity
    const itemQuantity = $("#ItmQuntity").val();
    if (itemQuantity <= 0) {
        $("#ItmQuntity").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Item Quantity must be greater than 0');
        isValid = false;
    } else {
        $("#ItmQuntity").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Item Price
    const itemPrice = $("#ItmPrice").val();
    if (itemPrice <= 0) {
        $("#ItmPrice").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Item Price must be greater than 0');
        isValid = false;
    } else {
        $("#ItmPrice").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Item Selling Price
    const itemSellingPrice = $("#ItmSellingPrice").val();
    if (itemSellingPrice <= 0) {
        $("#ItmSellingPrice").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Item Selling Price must be greater than 0');
        isValid = false;
    } else {
        $("#ItmSellingPrice").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Expiry Date
    const expiryDate = $("#ItmExpiryDate").val();
    if (expiryDate == "") {
        $("#ItmExpiryDate").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select an expiry date');
        isValid = false;
    } else {
        $("#ItmExpiryDate").css("border", "1.5px solid rgb(122, 245, 71)");
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
        $("#Uemail").css("border", "1.5px solid red");
        alertify.error('Please Enter A Valid Email Address');
        isValid = false;
    } else {
        $("#Uemail").css("border", "1.5px solid green");
    }

    // Validate Password
    const password = $("#Upassword").val();
    if (password.length < 6 || password.length > 15) {
        $("#Upassword").css("border", "1.5px solid red");
        alertify.error('Password Bust Be Between 6 And 15 Characters Long');
        isValid = false;
    } else {
        $("#Upassword").css("border", "1.5px solid green");
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
        $("#Uname").css("border", "1.5px solid red");
        alertify.error('Name must be between 3 and 15 characters long');
        isValid = false;
    } else {
        $("#Uname").css("border", "1.5px solid green");
    }

    // Validate Email
    const email = $("#URemail").val();
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        $("#URemail").css("border", "1.5px solid red");
        alertify.error('Please enter a valid email address');
        isValid = false;
    } else {
        $("#URemail").css("border", "1.5px solid green");
    }



    // Validate Password
    const password = $("#Upaasword").val();
    if (password.length < 6 || password.length > 15) {
        $("#Upaasword").css("border", "1.5px solid red");
        alertify.error('Password must be between 6 and 15 characters long');
        isValid = false;
    } else {
        $("#Upaasword").css("border", "1.5px solid green");
    }

    // Validate Phone
    const phone = $("#Unumber").val();
    const phoneRegex = /^[789]\d{9}$/;  // Example regex for Indian mobile numbers
    if (!phoneRegex.test(phone)) {
        $("#Unumber").css("border", "1.5px solid red");
        alertify.error('Please enter a valid 10-digit mobile number');
        isValid = false;
    } else {
        $("#Unumber").css("border", "1.5px solid green");
    }

    // Validate Role
    const role = $("#Urole").val();
    if (role === "-----User Role-----") {
        $("#Urole").css("border", "1.5px solid red");
        alertify.error('Please select a valid role');
        isValid = false;
    } else {
        $("#Urole").css("border", "1.5px solid green");
    }

    return isValid;
}

function restAll() {
    $('#ItmSelectSub').val('');
    $('#ItmSelectCate').val('');
    $('#ItmName').val('');
    $('#ItmSrNumber').val('');
    $('#ItmQuntity').val('');
    $('#ItmPrice').val('');
    $('#ItmSellingPrice').val('');
    $('#ItmExpiryDate').val('');
};