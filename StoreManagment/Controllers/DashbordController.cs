using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StoreManagment.ViewModel;
using StoreManagment.Repository;
using static StoreManagment.Models.Common;
using StoreManagment.Models;

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
            var viewModel = new ProductListViewModel
            {
                ProductsList = repository.GetProductList(),
                NewProduct = new AddProductVM()
            };
            return PartialView(viewModel);
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
    }
}















