using Microsoft.AspNetCore.Mvc;
using StoreManagment.Models;
using StoreManagment.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using StoreManagment.ViewModel;
using System.Text;
using static StoreManagment.Models.Common;

namespace StoreManagment.Controllers
{
    [Route("LoginHome")]
    public class LoginController : Controller
    {
        private readonly LoginRepository repository;

        public LoginController(LoginRepository repository)
        {
            this.repository = repository;
        }


        //Login And Registration Action Methods

        [HttpGet]
        [Route("signin")]
        [Route("")]
        public ActionResult Signin()
        {
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            //var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            if (emailClaim != null)
            {
                return RedirectToAction("Home", "Dashbord");
            }
            else
            {
                return View();
            }
            //if (HttpContext.Session.GetString("LoginSession") != null)
            //{
            //    return RedirectToAction("Home", "Dashbord");
            //}
            //else
            //{
            //    return View();
            //}

        }
        [HttpPost]
        [Route("signin")]
        [Route("")]
        public async Task<JsonResult> Signin([FromBody] LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Enter valid details" });
            }

            var user = repository.LoginMtd(model);

            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                return Json(new { success = false, message = "Invalid login credentials" });
            }

            // Create claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Name)
    };

            // Create identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Create principal
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Set authentication properties
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(15), // Optional: Set cookie expiration
                IsPersistent = true // Optional: Maintain user session across multiple requests
            };

            // Sign in user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            return Json(new { success = true });
        }





        [Route("_AddNew")]
        [HttpGet]
        public IActionResult _AddNew()
        {
            return PartialView();
        }

        [Route("_AddNew")]
        [HttpPost]
        public IActionResult _AddNew([FromBody] RegistrationVM Vmodel)
        {
            if (ModelState.IsValid)
            {
                UserRole user = (UserRole)Vmodel.Role;

                RegistrationModel mdl = new RegistrationModel()
                {
                    Name = Vmodel.Name,
                    Email = Vmodel.Email,
                    Password = Common.EncryptPassword(Vmodel.Password),
                    Phone_Num = Vmodel.Phone_Num,
                    Role = user
                }; 

                var userAdd = repository.AddNew(mdl);
                if (userAdd == 1)
                {
                    return Json("Ema");
                }

                //MobileNumberExitAllready
                else if (userAdd == 2)
                {
                    //return Json(new { MobNumber = true });
                    return Json("mob");
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



    


      
    }
}
