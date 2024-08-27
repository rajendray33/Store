using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagment.Migrations;
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

        //Get Category List For DropDown In Sub Product
        List<Sub_ProductVM>? GetCategoryList();

        //Add Sub Product
        bool Add_SubProduct(Add_Sub_ProductModel model);

        //Sub Product List
        List<Sub_ProductVM>? GetSubProductList();

        //Edit Sub Product Get Data
        Sub_ProductVM EditSubproduct(int id);
        List<SelectListItem> EditGetAllCategories();

        //Sub Product Update Method
        bool UpdateSubProduct(Add_Sub_ProductModel model);
        //Delete Category
        bool DeleteCategory(int Pid);
        //Delete Sub Product
        bool DeleteSubProduct(int SId);

        ////Add StckItem Category And Sub Product DropDown LIst
        //List<Add_ItemVM>? ItmGetCategoryList();

        //Get Sub Product DDl 
        List<SelectListItem> GetSubDDl(int CategoryId);

        //GetDDL Category For itm 
        List<SelectListItem> DDLGetAllCategories();
        //Add Stock Iteam------
        int AddItem(Add_ItemModel model);

        //Search Item
        Add_ItemVM? SearchItem(Add_ItemModel model);

        //Delete Stock Item
        string? DeleteStockItem(int id);

        //Item Featch List For Search Itm
        List<SelectListItem> GetItemsList(int SubProductId);

        //StockList
        List<Add_ItemVM> Stocklist();

    }
}
