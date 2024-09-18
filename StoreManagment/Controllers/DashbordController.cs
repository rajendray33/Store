using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StoreManagment.ViewModel;
using StoreManagment.Repository;
using static StoreManagment.Models.Common;
using StoreManagment.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using StoreManagment.Migrations;

namespace StoreManagment.Controllers
{
    [Route("Dashbord")]
    public class DashbordController : Controller
    {
        private readonly LoginRepository repository;

        public DashbordController(LoginRepository repository)
        {
            this.repository = repository;
        }


        // GET: DashbordController
        [HttpGet]
        [Route("Home")]
        public ActionResult Home()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            if (emailClaim == null)
            {
                return RedirectToAction("signin", "Login");
            }
            else
            {
                ViewBag.Email = emailClaim;
                ViewBag.Name = nameClaim;
                return View();
            }

        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {

            //HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("signin", "Login");

        }


        //Add Product Category method
        [HttpGet("_AddProduct")]
        public IActionResult _AddProduct()
        {
            return PartialView();
        }


        //ProductList Get Method Dayniically
        [HttpGet("_ProductGetLIst")]
        public IActionResult _ProductGetLIst()
        {
            var categoryList = repository.GetProductList();
            return PartialView(categoryList);
        }

        //Add Caategory Post
        [HttpPost("_AddProduct")]
        public IActionResult _AddProduct([FromBody] AddProductVM vmodel)
        {
            if (ModelState.IsValid)
            {
                AddProduct mdl = new AddProduct()
                {
                    Product_Category = vmodel.Product_Category
                };
                //Repository method
                var Addproductname = repository.AddProduct(mdl);

                if (Addproductname == 0)
                {
                    return Json("Exit");
                }
                else if (Addproductname == 1)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }

        //category Edit method
        [HttpGet("_EditCategory")]
        public IActionResult _EditCategory(int Pid)
        {
            if (Pid > 0)
            {
                var EditCategory = repository.EditCategory(Pid);

                if (EditCategory == null)
                {
                    return Json(new { success = false });
                }
                else
                {
                    return PartialView(EditCategory);
                }
            }
            return PartialView();
        }

        //category Edit Update method
        [HttpPost("_EditCategory")]
        public IActionResult _EditCategory([FromBody] AddProductVM model)
        {
            if (ModelState.IsValid)
            {
                var EditModel = new AddProduct
                {
                    Product_Category = model.Product_Category,
                    P_Id = model.P_Id
                };
                var Updatecategory = repository.UpdatreCategory(EditModel);

                if (Updatecategory == 0)
                {
                    return Json(new { success = false });
                }
                else
                {
                    return Json(new { success = true });
                }
            }

            else
            {
                return PartialView();
            }


        }

        //Delete Category
        [HttpPost("_DeleteCategory")]
        public IActionResult _DeleteCategory(int Cid)
        {
            if (Cid > 0)
            {
                var deletecategory = repository.DeleteCategory(Cid);

                if (deletecategory == true)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return NotFound(new { success = false, message = "Category not found" });
            }
        }



        //Sub Product popup And Category List
        [HttpGet("_AddSubProduct")]
        public IActionResult _AddSubProduct()
        {
            var categoriesList = repository.GetCategoryList();

            // Creating a single Sub_ProductVM object
            var viewModel = new Sub_ProductVM
            {
                categoryList = categoriesList.Select(c => new SelectListItem
                {
                    Value = c.Sub_P_Id.ToString(),
                    Text = c.ProductCategory
                }).ToList()
            };

            return PartialView(viewModel);
        }

        //Sub Product Add Post
        [HttpPost("_AddSubProduct")]
        public IActionResult _AddSubProduct([FromBody] Sub_ProductVM Vmodel)
        {
            if (ModelState.IsValid)
            {
                Add_Sub_ProductModel mdl = new Add_Sub_ProductModel()
                {
                    Sub_Product_Name = Vmodel.Sub_Product_Name,
                    P_Id = Vmodel.P_Id
                };
                //Repository method
                var Addproductname = repository.Add_SubProduct(mdl);



                if (Addproductname == false)
                {
                    return Json(new { success = false });
                }
                else
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });



        }


        //Get Sub Product List 
        [HttpGet("_SubProductList")]
        public IActionResult _SubProductList()
        {
            var SubProductList = repository.GetSubProductList();
            return PartialView(SubProductList);
        }

        //Edit SubProduct Get  And Category Dropdown List
        [HttpGet("_EditSubProduct")]
        public IActionResult _EditSubProduct(int SubPId)
        {
            var EditSubProduct = repository.EditSubproduct(SubPId);
            if (EditSubProduct == null)
            {
                return Json(new { success = true });
            }
            else
            {
                return PartialView(EditSubProduct);
            }

        }

        //Sub Product Update  
        [HttpPost("_EditSubProduct")]
        public IActionResult _EditSubProduct([FromBody] Sub_ProductVM model)
        {
            if (ModelState.IsValid)
            {
                var EditModel = new Add_Sub_ProductModel
                {
                    Sub_Product_Name = model.Sub_Product_Name,
                    Sub_P_Id = model.Sub_P_Id,
                    P_Id = model.P_Id    //reparsent to SubProduct Category
                };
                var UpdateSubProduct = repository.UpdateSubProduct(EditModel);

                if (UpdateSubProduct == false)
                {
                    return Json(new { success = false });
                }
                else
                {
                    return Json(new { success = true });
                }
            }

            else
            {
                return Json(new { success = false });
            }
        }

        //Delete Sub Product Method 
        [HttpPost("_DeleteSubProduct")]
        public IActionResult _DeleteSubProduct(int SpId)
        {
            if (SpId > 0)
            {
                var deletecategory = repository.DeleteSubProduct(SpId);

                if (deletecategory == true)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return NotFound(new { success = false, message = "Category not found" });
            }
        }


        //Add Stock Item Index
        [HttpGet("_AddItemIndex")]
        public IActionResult _AddItemIndex()
        {

            return PartialView();

        }


        //Add Stock Item Post
        [HttpPost("_AddStockIteams")]
        public async Task<IActionResult> _AddStockIteams([FromForm] Add_ItemVM model, IFormFile? Item_Image)
        {
            if (ModelState.IsValid)
            {
                string? imagePath = null;


                if (Item_Image != null && Item_Image.Length > 0)
                {
                    var fileName = Path.GetFileName(Item_Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Item_Image.CopyToAsync(stream);
                    }

                    imagePath = "/Image/" + fileName; // Save the relative path to the database
                }

                // Create model object
                Add_ItemModel mdl = new Add_ItemModel()
                {
                    ItemId = model.ItemId,
                    PartyId = model.Party_Id,
                    Gst_No = model.Gst_No,
                    P_Id = model.P_Id,
                    S_P_Id = model.S_P_Id,
                    Item_Name = model.Item_Name,
                    Item_SerialNumber = model.Item_SerialNumber,
                    Item_Quantity = model.Item_Quantity,
                    Item_Price = model.Item_Price,
                    Item_Selling_Price = model.Item_Selling_Price,
                    Item_Expiry_Date = model.Item_Expiry_Date,
                    Image = imagePath // Save the image path, which can be null
                };

                // Save item
                var itemId = repository.AddItem(mdl);
                if (itemId == 4)
                {
                    return Json("Updated");
                }
                if (itemId > 0)
                {
                    return Json(new { success = true, itemId });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }
        }

        //Stock Item List
        [HttpGet("_GetStockList")]
        public IActionResult _GetStockList()
        {
            var productList = repository.Stocklist(); // Yeh method database se product list la raha hai
            return PartialView(productList);
        }



        //Search Item Result Partial View 
        [HttpGet("_SearchItmResult")]
        public IActionResult _SearchItmResult(int P_Id, int S_P_Id, string Item_Name)
        {

            if (P_Id > 0 && S_P_Id > 0 && Item_Name != null)
            {
                var model = new Add_ItemModel
                {
                    P_Id = P_Id,
                    S_P_Id = S_P_Id,
                    Item_Name = Item_Name
                };

                var item = repository.SearchItem(model);
                if (item == null)
                {
                    return Json(new { success = false });
                }
                else
                {
                    return PartialView(item);
                }
            }
            else
            {
                return Json(new { success = false });
            }


        }


        [HttpPost("DeleteStockItem")]
        public IActionResult DeleteStockItem(int ItmId)
        {
            try
            {
                if (ItmId > 0)
                {
                    var imagePath = repository.DeleteStockItem(ItmId);

                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        var fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath.TrimStart('/'));

                        if (System.IO.File.Exists(fullImagePath))
                        {
                            System.IO.File.Delete(fullImagePath);
                        }
                    }

                    // Return success regardless of whether an image was present
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }


        //Party Index
        [HttpGet("_PartyView")]
        public IActionResult _PartyView()
        {
            return PartialView();
        }


        //Party Add Post
        [HttpPost("_PartyView")]
        public IActionResult _PartyView([FromBody] AddPartiesVM model)
        {
            if (ModelState.IsValid)
            {

                var partymodel = new PartiesModel
                {
                    PartyId = model.PartyId,
                    PartyName = model.PartyName,
                    GstNo = model.GstNo,
                    PhoneNo = model.PhoneNo,
                    Email = model.Email,
                    Address = model.Address,
                    StoreName = model.StoreName
                };


                var parties = repository.AddParties(partymodel);

                if (parties == "Updatesuccess")
                {
                    return Json("Updatesuccess");
                }
                else if (parties == "NotUpdate")
                {
                    return Json("NotUpdate");
                }
                else if (parties == "AlredyExit")
                {
                    return Json("AlredyExit");
                }
                else if (parties == "AddSuccess")
                {
                    return Json("AddSuccess");
                }

                else
                {
                    return Json("Error");
                }
            }
            else
            {
                return Json("Invalidmoelstate");
            }
        }

        //Parties List Get
        [HttpGet("_GetPartyList")]
        public IActionResult _GetPartyList()
        {
            var partylist = repository.GetPartiesList();
            return PartialView(partylist);
        }

        //Delete Parties
        [HttpPost("DeleteParties")]
        public IActionResult DeleteParties(int Id)
        {

            if (Id > 0)
            {
                var party = repository.DeleteParties(Id);
                if (party == true)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }

        }



        //Product sale Index
        [HttpGet("_SaleIndex")]
        public IActionResult _SaleIndex()
        {
            return PartialView();
        }


        //Add Customer Post
        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody] CustomerVm model)
        {
            if (ModelState.IsValid)
            {
                var modeldata = new CustomerModel()
                {
                    Customer_Id = model.Customer_Id,
                    Cust_Name = model.Cust_Name,
                    Cust_Mobile_No = model.Cust_Mobile_No,
                    Cust_City = model.Cust_City,
                    Cust_Email = model.Cust_Email,
                    Cust_Gstin_No = model.Cust_Gstin_No

                };
                var customer = repository.AddCustomer(modeldata);
                if (customer == true)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }
        }



























































































        //Get Itm List For Search Itm
        [HttpGet("GetItmList")]
        public JsonResult GetItmList(int SubProductId)
        {
            var ItmList = repository.GetItemsList(SubProductId);
            if (ItmList == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return new JsonResult(ItmList);
            }
        }

        //DDl parties
        [HttpGet("DDlParties")]
        public JsonResult DDlParties()
        {
            var Parties = repository.GetPartiesDDl();

            if (Parties != null)
            {
                return new JsonResult(Parties);
            }
            else
            {
                return Json("Null");
            }

        }

        //Get Gst Number
        [HttpGet("GetGstNumber")]
        public JsonResult GetGstNumber(int partid)
        {
            if (partid > 0)
            {
                var gstNo = repository.GetGstNo(partid);

                if (gstNo != null)
                {
                    return new JsonResult(gstNo);
                }
                else
                {
                    return Json("null");
                }
            }
            else
            {
                return Json("null");
            }

        }

        //Get Caategory For Item Json Action
        [HttpGet("_SubproductListItm")]
        public JsonResult _SubproductListItm(int CategoryId)
        {
            var category = repository.GetSubDDl(CategoryId); // Fetch data from repository
            return new JsonResult(category); // Return data as JSON
        }

        [HttpGet("DDlCategory")]
        public JsonResult DDlCategory()
        {
            var getddlcategory = repository.DDLGetAllCategories();
            return new JsonResult(getddlcategory);
        }



        [HttpGet("test")]
        public IActionResult test()
        {
            return View();
        }
    }
}















