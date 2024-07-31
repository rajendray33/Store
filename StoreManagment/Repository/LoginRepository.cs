using Microsoft.EntityFrameworkCore;
using StoreManagment.Models;
using StoreManagment.ViewModel;
namespace StoreManagment.Repository
{
    public class LoginRepository : ILogin
    {
        private readonly StoreDbContext DB;

        public LoginRepository(StoreDbContext db)
        {
            this.DB = db;
        }

        //Login Repository Method Code
        public LoginVM LoginMtd(LoginVM model)
        {
            try
            {
                var user = DB.Tbl_Registration.FirstOrDefault(x => x.Email == model.Email);

                if (user != null)
                {
                    // Compare decrypted password
                    string decryptedPassword = Common.DecryptPassword(user.Password);
                    if (decryptedPassword == model.Password)
                    {
                        return new LoginVM
                        {
                            Email = user.Email,
                            Name = user.Name
                        };
                    }
                }

                return null; // Return null if user not found or passwords don't match
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging in: {ex.Message}");
                return null; // Handle and log any exceptions that occur during login
            }
        }


        //New Registration
        public int? AddNew(RegistrationModel model)
        {
            if (model != null)
            {
                //User Email And Phone number Check That Not Alredy Exit Logic
                var EmialExit = DB.Tbl_Registration.Where(x => x.Email == model.Email).Any();
                if (EmialExit == true)
                {
                    return 1;

                };
                var mobilenumberExit = DB.Tbl_Registration.Where(x => x.Phone_Num == model.Phone_Num).Any();
                if (mobilenumberExit == true)
                {
                    return 2;

                }
                else
                {
                    model.CreateDate = DateTime.Now;
                    model.UpdateDate = null;
                    DB.Tbl_Registration.Add(model);
                    DB.SaveChanges();
                }
                return 0;

            }
            throw new NotImplementedException();
        }

        //Get Product List
        public List<AddProductVM> GetProductList()
        {
            var products = DB.Tbl_Add_Product.ToList();


            var productVMs = products.Select(p => new AddProductVM
            {
                P_Id = p.P_Id,
                Product_Category = p.Product_Category,
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate
            }).ToList();

            return productVMs;
        }

        //Add Category
        public int? AddProduct(AddProduct model)
        {
            if (model != null)
            {
                var CheckCategory = DB.Tbl_Add_Product.Where(x => x.Product_Category == model.Product_Category).Any();

                if (CheckCategory == true)
                {
                    return 0;
                }
                else
                {
                    model.CreateDate = DateTime.Now.Date;
                    model.UpdateDate = null;
                    DB.Tbl_Add_Product.Add(model);
                    DB.SaveChanges();
                    return 1;
                }
            }
            else
            {
                return 2;
            }

        }

        //Edit Category
        public AddProductVM? EditCategory(int id)
        {
            var CheckCatetogyExit = DB.Tbl_Add_Product.Where(x => x.P_Id == id).FirstOrDefault();
            if (CheckCatetogyExit != null)
            {
                var addProductVM = new AddProductVM
                {
                    P_Id = CheckCatetogyExit.P_Id,
                    Product_Category = CheckCatetogyExit.Product_Category

                };

                return addProductVM;
            }
            else
            {
                return null;
            }
        }

        public int? UpdatreCategory(AddProduct model)
        {
            var prodct = DB.Tbl_Add_Product.Where(x => x.Product_Category == model.Product_Category).Any();


            if (prodct == true)
            {
                return 0;
            }
            else
            {
                var adproduct = DB.Tbl_Add_Product.Where(x => x.P_Id == model.P_Id).FirstOrDefault();
                adproduct.Product_Category = model.Product_Category;
                adproduct.UpdateDate = DateTime.Now;
                DB.SaveChanges();
                return 1;
            }




        }











    };


}
