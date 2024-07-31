using StoreManagment.Models;
using StoreManagment.ViewModel;

namespace StoreManagment.Repository
{
    public interface ILogin
    {
        // User Login Detail
        LoginVM? LoginMtd(LoginVM model);

        //Add New User
        int? AddNew(RegistrationModel model);


        List<AddProductVM>? GetProductList();

        //Add Product Category
        int? AddProduct(AddProduct model);

        // Get Category Update
        AddProductVM? EditCategory(int id);

        // Update post Category
        int? UpdatreCategory(AddProduct model);
    }
}
