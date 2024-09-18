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

        alertify.success('okok')
    })

    //Add Products Popup Ajax
    $(document).on('click', '#AddProductPopUp', function () {


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



    //Add Item Index => Use For Show Buttons
    $(document).on('click', '#AddItmIndex', function () {

        $.ajax({
            type: 'Get',
            url: '/Dashbord/_AddItemIndex',
            cache: false,
            success: function (responce) {
                $('#AddCategoryPopup').empty();
                $('#AddCategoryPopup').html(responce);
                $('#StockItmTblList').empty();
                GetStocklist();

                DDlParties();
                //DropDowns M Category And Sub Product Bind
                DDlCategory('#ItmSelectCateAdd');
                fetchSubProducts('#ItmSelectCateAdd', '#ItmSelectSub');

            },

        })
    });

    //Add Item PopUp
    $(document).on('click', '#AddItemPopUp', function () {
        $('#exampleModalLabel').text('Add Stock Item');
        $('#AddItemBtn').text('Add Item');
        $('#ItmSelectCateAdd').val('');
        $('#ItmSelectSub').empty();
        $('#ItmSelectSub').append('<option value="">--Select Sub Product--</option>');
        $('#imagePreview').html(''); // Remove Item Img PrieView
        $('#ItmimageInput').val('');
        restAll();

    })



    //Add Item post Ajax

    $(document).on('click', '#AddItemBtn', function () {


        if (Add_Item_Productvalidatoin()) {

            var itmid = $('#ItmIdEditValue').val();
            var formData = new FormData();

            formData.append('Party_Id', $('#ItmSelectParty').val());
            formData.append('Gst_No', $('#ItmSelectGstNo').val());
            formData.append('P_Id', $('#ItmSelectCateAdd').val());
            formData.append('S_P_Id', $('#ItmSelectSub').val());
            formData.append('ItemId', itmid);
            formData.append('Item_Name', $('#ItmName').val());
            formData.append('Item_SerialNumber', $('#ItmSrNumber').val());
            formData.append('Item_Quantity', $('#ItmQuntity').val());
            formData.append('Item_Price', $('#ItmPrice').val());
            formData.append('Item_Selling_Price', $('#ItmSellingPrice').val());
            formData.append('Item_Expiry_Date', $('#ItmExpiryDate').val());

            // Add image file to FormData
            var imageFile = $('#ItmimageInput')[0].files[0];
            formData.append('Item_Image', imageFile);

            $.ajax({
                type: 'POST',
                url: '/Dashbord/_AddStockIteams',
                contentType: false,
                processData: false,
                data: formData,
                success: function (response) {
                    varItmId = response.itemId;
                    if (response == "Updated") {
                        GetStocklist();
                        $('#ItemAdd').modal('toggle')
                        restAll();
                        alertify.success("Updated Successfully")
                    }
                    else if (response.success || varItmId > 0) {
                        GetStocklist();
                        $('#ItemAdd').modal('toggle')
                        restAll();
                        alertify.success('Item Added Successfully');
                    }
                    else {
                        alertify.error('Item Not Added');
                    }
                },
                error: function () {
                    alert('There was an error submitting the form.');
                }
            });
        }

    });


    //Item Edit
    $(document).on('click', '#EditItemBtn', function () {

        restAll();
        $('#ItmSelectSub').empty();
        $('#ItmSelectSub').append('<option value="">--Select Sub Product--</option>');

        var ItmId = $(this).data('id');

        var row = $(this).closest('tr');

        var SPID = row.find('td:eq(2)').text();
        var PID = row.find('td:eq(3)').text();
        var Name = row.find('td:eq(4)').text();
        var gst_no = row.find('td:eq(8)').text();
        var SNO = row.find('td:eq(9)').text();
        var Qty = row.find('td:eq(10)').text();
        var Price = row.find('td:eq(11)').text();
        var sellPrice = row.find('td:eq(12)').text();
        var Expr = row.find('td:eq(13)').text();
        var partyid = row.find('td:eq(14)').text();

        $('#ItemAdd').modal('toggle');
        $('#exampleModalLabel').text('Update Stock Item');
        $('#AddItemBtn').text('Update Item');



        $('#ItmSelectCateAdd').val(PID);
        $('#ItmSelectSub').val(SPID);
        $('#ItmIdEditValue').val(ItmId);
        $('#ItmName').val(Name);
        $('#ItmSelectGstNo').val(gst_no);
        $('#ItmSelectParty').val(partyid);
        $('#ItmSrNumber').val(SNO);
        $('#ItmQuntity').val(Qty);
        $('#ItmPrice').val(Price);
        $('#ItmSellingPrice').val(sellPrice);
        $('#ItmExpiryDate').val(Expr);

    });


    //Search Item Popup
    $(document).on('click', '#SearchItmPopUp', function () {

        $('#ItmSearchResult').empty();
        $('#StockSearch_Category').val('');
        $('#StockSearch_SubProduct,#searchItmName').empty();
        $('#StockSearch_SubProduct').append('<option value="">--Select Sub Product--</option>');
        $('#searchItmName').append('<option value="">--Select Item--</option>');
        $("#StockSearch_Category,#StockSearch_SubProduct,#searchItmName").css("border", "");


        DDlCategory('#StockSearch_Category');
        fetchSubProducts('#StockSearch_Category', '#StockSearch_SubProduct');
        fetchItems('#StockSearch_SubProduct', '#searchItmName');
    });


    //Search item Ajax
    $(document).on('click', '#SearchItmBtn', function () {

        //$("#spinner").show();
        if (ItemSearchValidation()) {
            var userData = {
                P_Id: $('#StockSearch_Category').val(),
                S_P_Id: $('#StockSearch_SubProduct').val(),
                Item_Name: $('#searchItmName').val(),
            };
            $.ajax({
                type: 'GET',
                url: '/Dashbord/_SearchItmResult',
                data: userData,  // Data sent as query string
                cache: false,
                success: function (response) {
                    $('#ItmSearchResult').empty();
                    if (response.length > 0) {
                        $('#ItmSearchResult').html(response);
                    } else {
                        alertify.error('Result Not Found');
                        $('#ItmSearchResult').text('Result Not Found').css('color', 'red');
                    }
                },
                //complete: function () {
                //    // Hide the spinner once the AJAX request completes
                //    $("#spinner").hide();
                //},
                error: function (error) {
                    console.log("Error: ", error);
                }
            });
        }

    });


    // Stock Item Delete Ajax
    $(document).on('click', '#Stock_ItmDeleteBtn', function () {
        var id = $(this).data('id');  // Button se item ID lena

        alertify.confirm('Delete Confirmation', 'Are you sure you want to delete this Item?',
            function () { // Agar user confirm kare
                $.ajax({
                    type: 'POST',
                    url: '/Dashbord/DeleteStockItem?ItmId=' + id,
                    success: function (response) {
                        if (response.success) {
                            GetStocklist();
                            alertify.success("Item deleted successfully.");
                            $('#ItmSearchResult').empty();
                            $('#StockSearch_Category,#searchItmName,#StockSearch_SubProduct').val('');
                            $("#StockSearch_Category,#StockSearch_SubProduct,#searchItmName").css("border", "");
                        } else {
                            alertify.error("Failed to delete Product: " + response.message);
                        }
                    },
                    error: function () {
                        alertify.error('There was an error deleting the product.', 10000);


                    }
                });
            },
            function () { // Agar user cancel kare
                alertify.error('Delete action canceled.');
            });
    });


    //Item Add PriView
    $(document).on('change', '#ItmimageInput', function () {
        var file = this.files[0];
        var reader = new FileReader();

        reader.onload = function (event) {
            var image = $('<img>').attr('src', event.target.result).attr('id', 'ItemImg');
            $('#imagePreview').html(image);
        };

        reader.readAsDataURL(file);
    });


    $(document).on('click', '#AddPartyBtn1', function () {

        $('#PartyModelPopupLabl').text('Add Party');
        $('#SubmitParty').text('Add Party');
        $('#Partyname, #PartyGstNo, #PartyPhoneNo, #PartyEmail, #PartyAddress, #PartyStoreName').css("border", "");


        ResetParties();
    })

    //Add Parties Model popup
    $(document).on('click', '#AddParty', function () {

        $.ajax({
            type: 'get',
            url: '/Dashbord/_PartyView',
            success: function (responce) {
                $('#AddCategoryPopup').empty();
                $('#AddCategoryPopup').html(responce);
                GetPartiesList();
            }
        })

    })

    //Parties Post Ajax 
    $(document).on('click', '#SubmitParty', function () {

        var id = $('#partiesidinput').val();

        var partyData = {
            PartyId: id,
            PartyName: $('#Partyname').val(),
            GstNo: $('#PartyGstNo').val(),
            PhoneNo: $('#PartyPhoneNo').val(),
            Email: $('#PartyEmail').val(),
            Address: $('#PartyAddress').val(),
            StoreName: $('#PartyStoreName').val()
        };

        if (AddPartyValidation()) {
            $.ajax({
                type: 'POST',
                url: '/Dashbord/_PartyView',
                contentType: 'application/json',
                data: JSON.stringify(partyData),
                success: function (response) {
                    if (response == "Updatesuccess") {
                        GetPartiesList();
                        alertify.success('Party Updated Succesfully');
                        ResetParties();
                        $('#AddpartyModelPopup').modal('toggle')
                    }
                    else if (response == "NotUpdate") {
                        alertify.error('Party Not Updated');
                    }

                    else if (response == "AlredyExit") {
                        alertify.error('Parties Alredy Exit Please Ensure Details');
                        $('#PartyPhoneNo').css("border", "1.5px solid red");
                        $('#PartyEmail').css("border", "1.5px solid red");
                    }
                    else if (response == "AddSuccess") {
                        GetPartiesList();
                        ResetParties();
                        $('#AddpartyModelPopup').modal('toggle')
                        alertify.success('Party Added Succesfully');
                    }
                    else {
                        alertify.error("Error Try Again Latter")
                    }

                },
            });
        }

    })

    //Delete Parties
    $(document).on('click', '#DeletePartiesBtn', function () {
        var partyid = $(this).data('id');


        alertify.confirm('Delete Confirmation', 'Are you sure you want to delete this Party?',
            function () {
                $.ajax({
                    type: 'POST',
                    url: '/Dashbord/DeleteParties?Id=' + partyid,
                    dataType: 'json',
                    success: function (response) {
                        if (response.success) {
                            GetPartiesList();
                            alertify.success('Party deleted successfully!');

                        } else {
                            alertify.error("Failed to delete Party: " + response.message);
                        }
                    },
                    error: function (xhr, status, error) {
                        alertify.error('There was an error deleting the party: ' + error);
                        console.log(xhr.responseText);
                    }
                });
            },
            function () {
                alertify.error('Delete action canceled.');
            });
    });

    //Edit Party
    $(document).on('click', '#EditPartyBtn', function () {

        var partyid = $(this).data('id');

        var row = $(this).closest('tr');

        var Name = row.find('td:eq(2)').text();
        var Gst = row.find('td:eq(3)').text();
        var Mobile = row.find('td:eq(4)').text();
        var Email = row.find('td:eq(5)').text();
        var Address = row.find('td:eq(6)').text();
        var Store = row.find('td:eq(7)').text();


        $('#AddpartyModelPopup').modal('toggle')
        $('#PartyModelPopupLabl').text('Update Party')
        $('#SubmitParty').text('Update')



        $('#partiesidinput').val(partyid);
        $('#Partyname').val(Name);
        $('#PartyGstNo').val(Gst);
        $('#PartyPhoneNo').val(Mobile);
        $('#PartyEmail').val(Email);
        $('#PartyAddress').val(Address);
        $('#PartyStoreName').val(Store);


    })

    //Get Gst Number on Party Change 
    $(document).on('change', '#ItmSelectParty', function () {
        var selectedPartyId = $(this).val();
        if (selectedPartyId) {
            $.ajax({
                type: 'GET',
                url: '/Dashbord/GetGstNumber',
                data: { partid: selectedPartyId },
                cache: false,
                success: function (response) {

                    if (response.length > 0) {
                        $('#ItmSelectGstNo').val(response);
                    }
                    else {
                        alertify.error('Gst Number Not Found')
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                }
            });
        } else {
            $('#gstNumber').val(''); // Clear the field if no party is selected
        }
    });


    //Sale Item Index 
    $(document).on('click', '#SaleitemPopup', function () {

        $.ajax({
            type: 'GET',
            url: '/Dashbord/_SaleIndex',
            cache: false,
            success: function (response) {
                $('#AddCategoryPopup').empty();
                $('#AddCategoryPopup').html(response);

            },
            error: function (xhr, status, error) {
                console.error("Error: " + error);
            }
        });
    })

    //Submint Customer post
    $(document).on('click', '#SaveCustomer', function () {
        var customerData = {
            Customer_Id: $('#customerid').val(),
            Cust_Name: $('#Cust_Name').val(),
            Cust_Mobile_No: $('#Cust_MobileNo').val(),
            Cust_Email: $('#Cust_Email').val(),
            Cust_City: $('#Cust_City').val(),
            Cust_Gstin_No: $('#Cust_Gstnumber').val()
        };
        debugger;
        if (CustomerFormValidation()) {
            $.ajax({
                type: 'POST',
                url: '/Dashbord/AddCustomer',
                contentType: 'application/json',
                data: JSON.stringify(customerData),
                success: function (response) {
                    if (response.success) {
                        alertify.success('Customer Addes Successfully')
                    }
                    else {
                        alertify.error('Customer Allredy Exit')
                    }
                    //if (response == "Updatesuccess") {
                    //    GetPartiesList();
                    //    alertify.success('Party Updated Succesfully');
                    //    ResetParties();
                    //    $('#AddpartyModelPopup').modal('toggle')
                    //}
                    //else if (response == "NotUpdate") {
                    //    alertify.error('Party Not Updated');
                    //}

                    //else if (response == "AlredyExit") {
                    //    alertify.error('Parties Alredy Exit Please Ensure Details');
                    //    $('#PartyPhoneNo').css("border", "1.5px solid red");
                    //    $('#PartyEmail').css("border", "1.5px solid red");
                    //}
                    //else if (response == "AddSuccess") {
                    //    GetPartiesList();
                    //    ResetParties();
                    //    $('#AddpartyModelPopup').modal('toggle')
                    //    alertify.success('Party Added Succesfully');
                    //}
                    //else {
                    //    alertify.error("Error Try Again Latter")
                    //}

                },
            });
        }

    })
})









//Item List In Search Boxfunction fetchItems
function fetchItems(subProductDropdownId, itemDropdownId) {
    $(document).on('change', subProductDropdownId, function () {
        var SubProductId = $(this).val();

        if (SubProductId > 0) {
            $.ajax({
                type: 'GET',
                url: '/Dashbord/GetItmList',
                data: { SubProductId: SubProductId },
                cache: false,
                success: function (response) {
                    var $itemDropdown = $(itemDropdownId);
                    $itemDropdown.empty();
                    $itemDropdown.append('<option value="">--Select Item--</option>');

                    if (response != null && response.length > 0) {
                        $.each(response, function (index, item) {
                            $itemDropdown.append('<option value="' + item.text + '">' + item.text + '</option>');
                        });
                    } else {
                        $itemDropdown.append('<option value="">No Items Found</option>');
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                },

            });
        } else {
            alertify.error("Please select a valid sub product.");
        }
    });
}


//DDl Category Itm
function DDlCategory(ProductId) {

    $.ajax({
        type: 'GET',
        url: '/Dashbord/DDlCategory',
        cache: false,
        success: function (response) {
            var $dropdown = $(ProductId);
            $dropdown.empty();
            $dropdown.append('<option value="">--Select Category--</option>');

            if (response && response.length > 0) {
                $.each(response, function (index, item) {
                    $dropdown.append('<option value="' + item.value + '">' + item.text + '</option>');
                });
            } else {
                $dropdown.append('<option value="">No Subproducts Found</option>');
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
        }
    });
}

//DDl Sub Product 

function fetchSubProducts(CategoryDDL, SubProductDDL) {

    $(document).on('change', CategoryDDL, function () {
        var CategoryId = $(this).val();
        if (CategoryId > 0) {
            $.ajax({
                type: 'GET',
                url: '/Dashbord/_SubproductListItm',
                data: { CategoryId: CategoryId },
                cache: false,
                success: function (response) {
                    var $subProductDropdown = $(SubProductDDL);
                    $subProductDropdown.empty();
                    $subProductDropdown.append('<option value="">--Select Sub Product--</option>');

                    if (response != null && response.length > 0) {
                        $.each(response, function (index, item) {
                            $subProductDropdown.append('<option value="' + item.value + '">' + item.text + '</option>');
                        });
                    } else {
                        $subProductDropdown.append('<option value="">No Subcategories Found</option>');
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error: " + error);
                }
            });
        } else {
            alertify.error("Please select a valid category.");
        }
    });
}


//Ddl Parties
function DDlParties() {
    debugger
    $.ajax({
        type: 'GET',
        url: '/Dashbord/DDlParties',
        cache: false,
        success: function (response) {
            var $dropdown = $("#ItmSelectParty");
            $dropdown.empty();
            $dropdown.append('<option value="">--Select Party--</option>');

            if (response && response.length > 0) {
                $.each(response, function (index, item) {
                    $dropdown.append('<option value="' + item.value + '">' + item.text + '</option>');
                });
            } else {
                $dropdown.append('<option value="">No Subproducts Found</option>');
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

//Stock Item List
function GetStocklist() {

    $.ajax({
        type: 'GET',
        url: '/dashbord/_GetStockList',
        cache: false,
        success: function (response) {

            $('#StockItmTblList').html(response);
        },
        error: function (xhr, status, error) {
            console.log('Error: ' + error);
        }
    });
}

//Parties List
function GetPartiesList() {

    $.ajax({
        type: 'GET',
        url: '/Dashbord/_GetPartyList',
        cache: false,
        success: function (response) {
            if (response.length > 0) {

                $('#PartiesListShow').html(response);
            }
            else {
                alertify.error('Parties lISt Not  Found')
            }
        },
        error: function (xhr, status, error) {

            console.log("Error: " + error);
        }
    });

}

//-----------------List Get Code Here--^^^^^^^----------------------------


























//Add Customer Validation
function CustomerFormValidation() {
    let isValid = true;

    // Reset previous error messages and borders
    $(".error").remove();
    $("input").css("border", "");

    // Validate Customer Name
    const customerName = $("#Cust_Name").val();
    if (customerName.length < 3 || customerName.length > 50) {
        $("#Cust_Name").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Customer name should be between 3 and 50 characters');
        isValid = false;
    } else {
        $("#Cust_Name").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Phone Number
    const phoneNumber = $("#Cust_MobileNo").val();
    const phoneRegex = /^[6-9]\d{9}$/; // Indian phone number format
    if (!phoneRegex.test(phoneNumber)) {
        $("#Cust_MobileNo").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please enter a valid phone number');
        isValid = false;
    } else {
        $("#Cust_MobileNo").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Customer Email
    const customerEmail = $("#Cust_Email").val();
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailRegex.test(customerEmail)) {
        $("#Cust_Email").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please enter a valid email address');
        isValid = false;
    } else {
        $("#Cust_Email").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate City
    const city = $("#Cust_City").val();
    if (city.length < 3) {
        $("#Cust_City").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please enter a valid city name');
        isValid = false;
    } else {
        $("#Cust_City").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Gstin Number is optional, but validate format if provided
    //const gstinNumber = $("#Cust_Gstnumber").val();
    //const gstinRegex = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[A-Z0-9]{3}$/;
    //if (gstinNumber && !gstinRegex.test(gstinNumber)) {
    //    $("#Cust_Gstnumber").css("border", "1.5px solid rgb(255, 0, 0)");
    //    alertify.error('Please enter a valid GSTIN number');
    //    isValid = false;
    //} else if (gstinNumber) {
    //    $("#Cust_Gstnumber").css("border", "1.5px solid rgb(122, 245, 71)");
    //}

    return isValid;
}




//Add Product Category Validaton
function EditProductvalidatoin() {
    let isValid = true;

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

    //party valiadatiom
    const partyName = $("#ItmSelectParty").val();
    if (partyName === "" || partyName === "--Select Party--") {
        $("#ItmSelectParty").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a Party');
        isValid = false;
    } else {
        $("#ItmSelectParty").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    //Gt valiadatiom
    const Gst = $("#ItmSelectGstNo").val();
    if (Gst === "" || Gst === "--Select Party--") {
        $("#ItmSelectGstNo").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a Party');
        isValid = false;
    } else {
        $("#ItmSelectGstNo").css("border", "1.5px solid rgb(122, 245, 71)");
    }



    // Validate Category
    const category = $("#ItmSelectCateAdd").val();
    if (category === "" || category === "--Select Sub Product--") {
        $("#ItmSelectCateAdd").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a category');
        isValid = false;
    } else {
        $("#ItmSelectCateAdd").css("border", "1.5px solid rgb(122, 245, 71)");
    }



    // Validate Sub Product
    const subProduct = $("#ItmSelectSub").val();
    if (subProduct === "" || subProduct === "--Select Sub Product--") {
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
    //const expiryDate = $("#ItmExpiryDate").val();
    //if (expiryDate == "") {
    //    $("#ItmExpiryDate").css("border", "1.5px solid rgb(255, 0, 0)");
    //    alertify.error('Please select an expiry date');
    //    isValid = false;
    //} else {
    //    $("#ItmExpiryDate").css("border", "1.5px solid rgb(122, 245, 71)");
    //}

    return isValid;
}


//Search Iteam Validation
function ItemSearchValidation() {
    let isValid = true;

    // Reset previous error messages and borders
    $(".error").remove();
    $("input, select").css("border", "");

    // Validate Category
    const category = $("#StockSearch_Category").val();
    if (category == "--Select Category--" || category == '') {
        $("#StockSearch_Category").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a category');


        isValid = false;
    } else {
        $("#StockSearch_Category").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Sub Product
    const subProduct = $("#StockSearch_SubProduct").val();
    if (subProduct == "--Select Sub Product--" || subProduct == '') {
        $("#StockSearch_SubProduct").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please select a Sub Product');
        isValid = false;
    } else {
        $("#StockSearch_SubProduct").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Item Name
    const itemName = $("#searchItmName").val();
    if (itemName.length < 3 || itemName.length > 50) {
        $("#searchItmName").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please Full Iteam Name');

        isValid = false;
    } else {
        $("#searchItmName").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    return isValid;
}

//Validations Code Here
//Login Validaton Funcation

function customValidateLoginForm() {
    let isValid = true;

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

//Add Party Validation 
function AddPartyValidation() {
    let isValid = true;

    // Reset previous error messages and borders
    $(".error").remove();
    $("input").css("border", "");

    // Validate Party Name (e.g., minimum 3 characters)
    const partyName = $("#Partyname").val();
    if (partyName.length < 3) {
        $("#Partyname").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Party Name must be at least 3 characters long');
        isValid = false;
    } else {
        $("#Partyname").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate GST Number (e.g., must be exactly 15 characters)
    const gstNo = $("#PartyGstNo").val();
    if (gstNo.length !== 15) {
        $("#PartyGstNo").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('GST Number must be exactly 15 characters long');
        isValid = false;
    } else {
        $("#PartyGstNo").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Phone Number (e.g., must be 10 digits)
    const phoneNo = $("#PartyPhoneNo").val();
    const phonePattern = /^[6-9]\d{9}$/;

    if (!phonePattern.test(phoneNo)) {
        $("#PartyPhoneNo").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Mobile Number incorret formate and be exactly 10 digits long');
        isValid = false;
    } else {
        $("#PartyPhoneNo").css("border", "1.5px solid rgb(122, 245, 71)");
    }


    // Validate Email (e.g., simple email pattern)
    const email = $("#PartyEmail").val();
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(email)) {
        $("#PartyEmail").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Please enter a valid email address');
        isValid = false;
    } else {
        $("#PartyEmail").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Address (e.g., minimum 5 characters)
    const address = $("#PartyAddress").val();
    if (address.length < 5) {
        $("#PartyAddress").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Address must be at least 5 characters long');
        isValid = false;
    } else {
        $("#PartyAddress").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    // Validate Store Name (e.g., minimum 3 characters)
    const storeName = $("#PartyStoreName").val();
    if (storeName.length < 3) {
        $("#PartyStoreName").css("border", "1.5px solid rgb(255, 0, 0)");
        alertify.error('Store Name must be at least 3 characters long');
        isValid = false;
    } else {
        $("#PartyStoreName").css("border", "1.5px solid rgb(122, 245, 71)");
    }

    return isValid;
}

function restAll() {
    $('#ItmSelectParty, #ItmSelectGstNo, #ItmSelectCateAdd, #ItmSelectSub, #ItmName, #ItmSrNumber, #ItmQuntity, #ItmPrice, #ItmSellingPrice, #ItmExpiryDate').val('').css('border', '');

};
//Clear Parties Form
function ResetParties() {
    $('#Partyname').val('');
    $('#PartyGstNo').val('');
    $('#PartyPhoneNo').val('');
    $('#PartyEmail').val('');
    $('#PartyAddress').val('');
    $('#PartyStoreName').val('');
}