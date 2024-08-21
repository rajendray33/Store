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


        [HttpGet("_AddItem")]
        public IActionResult _AddItem()
        {
            return PartialView();
        }


        //Add Stock Item Index
        [HttpGet("_AddItemIndex")]
        public IActionResult _AddItemIndex()
        {

            return PartialView();

        }


        //Add Stock Item Post
        [HttpPost("_AddStockIteams")]
        public IActionResult _AddStockIteams([FromBody] Add_ItemVM model)
        {
            if (ModelState.IsValid)
            {
                Add_ItemModel mdl = new Add_ItemModel()
                {
                    P_Id = model.P_Id,
                    S_P_Id = model.S_P_Id,
                    Item_Name = model.Item_Name,
                    Item_SerialNumber = model.Item_SerialNumber,
                    Item_Quantity = model.Item_Quantity,
                    Item_Price = model.Item_Price,
                    Item_Selling_Price = model.Item_Selling_Price,
                    Item_Expiry_Date = model.Item_Expiry_Date,

                };
                var item = repository.AddItem(mdl);
                if (item == true)
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




















































































        //Stock Item List
        [HttpGet("_GetStockList")]
        public IActionResult _GetStockList()
        {
            var productList = repository.Stocklist(); // Yeh method database se product list la raha hai
            return PartialView(productList);
        }

        //Item Stock Partial view 
        [HttpGet("_StockDetail")]
        public IActionResult _StockDetail()
        {
            return PartialView();
        }



        [HttpPost("_SearchItm")]
        public IActionResult _SearchItm([FromBody] Add_ItemModel model)
        {
            var item = repository.SearchItem(model); // Fetch the item using repository
            if (item == null)
            {
                return Json(new { success = false, message = "Item not found" });
            }
            else
            {
                return PartialView(item);
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
    }
}















