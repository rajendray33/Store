using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagment.Migrations;
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
                    model.CreateDate = DateTime.Now;
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

        //Get Category list For Drop Down In Sub Product
        public List<Sub_ProductVM> GetCategoryList()
        {
            var categoriesList = DB.Tbl_Add_Product.Select(p => new Sub_ProductVM
            {
                Sub_P_Id = p.P_Id,
                ProductCategory = p.Product_Category
            }).ToList();

            return categoriesList;
        }

        //Add Sub Product Post
        public bool Add_SubProduct(Add_Sub_ProductModel model)
        {

            var CheckSubProduct = DB.Tbl_Add_SubProduct.Where(x => x.Sub_Product_Name == model.Sub_Product_Name).Any();

            if (CheckSubProduct == true)
            {
                return false;
            }
            else
            {
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = null;
                DB.Tbl_Add_SubProduct.Add(model);
                DB.SaveChanges();
                return true;
            }


        }


        //Sub Product List
        public List<Sub_ProductVM> GetSubProductList()
        {
            var SubProductList = DB.Tbl_Add_SubProduct.Include(x => x.AddProduct).ToList();


            var SubproductVM = SubProductList.Select(p => new Sub_ProductVM
            {

                Sub_P_Id = p.Sub_P_Id,
                Sub_Product_Name = p.Sub_Product_Name,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate,
                ProductCategory = p.AddProduct?.Product_Category,
                //P_Id = p.P_Id,
            }).ToList();

            return SubproductVM;
        }

        //Edit Get  SubProduct And DropDow List
        public Sub_ProductVM EditSubproduct(int id)
        {
            //.Include(x => x.AddProduct)
            var subProduct = DB.Tbl_Add_SubProduct.Find(id);

            if (subProduct == null)
            {
                return null;
            }
            var categories = EditGetAllCategories();
            var addProductVM = new Sub_ProductVM
            {
                Sub_P_Id = subProduct.Sub_P_Id,
                Sub_Product_Name = subProduct.Sub_Product_Name,
                //ProductCategory = subProduct.AddProduct?.Product_Category,
                categoryList = categories,
                P_Id = subProduct.P_Id

            };


            return addProductVM;
        }

        public List<SelectListItem> EditGetAllCategories()
        {
            return DB.Tbl_Add_Product.Select(c => new SelectListItem
            {
                Value = c.P_Id.ToString(),
                Text = c.Product_Category
            }).ToList();
        }

        //Update Sub Product Post
        public bool UpdateSubProduct(Add_Sub_ProductModel model)
        {
            var Subprodct = DB.Tbl_Add_SubProduct.Where
            (x => x.Sub_Product_Name == model.Sub_Product_Name && x.P_Id == model.P_Id).Any();


            if (Subprodct == true)
            {
                return false;
            }
            else
            {
                var SubProductUpdated = DB.Tbl_Add_SubProduct.Where(x => x.Sub_P_Id == model.Sub_P_Id).FirstOrDefault();

                SubProductUpdated.Sub_Product_Name = model.Sub_Product_Name;
                SubProductUpdated.P_Id = model.P_Id;
                SubProductUpdated.UpdatedDate = DateTime.Now;
                DB.SaveChanges();
                return true;
            }
        }

        public bool DeleteCategory(int Pid)
        {
            if (Pid > 0)
            {
                var SubProductSelect = DB.Tbl_Add_SubProduct.Where(x => x.P_Id == Pid).ToList();
                //Sbhi Sub Product ko delete
                DB.Tbl_Add_SubProduct.RemoveRange(SubProductSelect);

                var category = DB.Tbl_Add_Product.Find(Pid);

                if (category != null)
                {
                    DB.Tbl_Add_Product.Remove(category);
                    DB.SaveChanges();
                    return true;
                }
            }

            return false;


        }

        public bool DeleteSubProduct(int SId)
        {
            if (SId > 0)
            {
                var checkproduct = DB.Tbl_Add_SubProduct.Where(x => x.Sub_P_Id == SId).FirstOrDefault();
                if (checkproduct != null)
                {
                    DB.Tbl_Add_SubProduct.Remove(checkproduct);
                    DB.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }



        //Add Item  Post Method -------------------
        public int AddItem(Add_ItemModel model)
        {
            try
            {

                if (model.ItemId > 0)
                {
                    var item = DB.Tbl_AddItem.Find(model.ItemId);
                    if (item != null)
                    {
                        //var img = DB.Tbl_AddItem.Where(x => x.ItemId == model.ItemId).Select(x => x.Image).FirstOrDefault();

                        // Updating the existing item
                        item.Item_Name = model.Item_Name;
                        item.Item_SerialNumber = model.Item_SerialNumber;
                        item.Item_Quantity = model.Item_Quantity;
                        item.Gst_No = model.Gst_No;
                        item.PartyId = model.PartyId;
                        item.P_Id = model.P_Id;
                        item.S_P_Id = model.S_P_Id;
                        item.Item_Expiry_Date = model.Item_Expiry_Date;
                        item.Image = model.Image;
                        item.Item_Price = model.Item_Price;
                        item.Item_Selling_Price = model.Item_Selling_Price;
                        item.UpdatedDate = DateTime.Now;

                        DB.SaveChanges();
                        return 4;

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    model.UpdatedDate = null;

                    DB.Tbl_AddItem.Add(model);
                    DB.SaveChanges();

                    return model.ItemId;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 0;
            }
        }


        public Add_ItemVM? SearchItem(Add_ItemModel model)
        {
            var ItemSearch = DB.Tbl_AddItem.Include(x => x.AddProduct).Include(x => x.Add_Sub_ProductModel)
            .Where(x => x.P_Id == model.P_Id && x.S_P_Id == model.S_P_Id && x.Item_Name == model.Item_Name).FirstOrDefault();

            if (ItemSearch == null)
            {
                return null;
            }
            else
            {
                // Map the found record to Add_ItemVM
                var itemData = new Add_ItemVM
                {
                    ItemId = ItemSearch.ItemId,
                    P_Id = ItemSearch.P_Id,
                    S_P_Id = ItemSearch.S_P_Id,
                    Item_Name = ItemSearch.Item_Name,
                    Item_SerialNumber = ItemSearch.Item_SerialNumber,
                    Item_Quantity = ItemSearch.Item_Quantity,
                    Item_Price = ItemSearch.Item_Price,
                    Item_Expiry_Date = ItemSearch.Item_Expiry_Date,
                    Item_Selling_Price = ItemSearch.Item_Selling_Price,
                    CreatedDate = ItemSearch.CreatedDate,
                    UpdatedDate = ItemSearch.UpdatedDate,
                    ProductCategory = ItemSearch.AddProduct?.Product_Category,
                    SubProduct = ItemSearch.Add_Sub_ProductModel?.Sub_Product_Name,
                    Image = ItemSearch.Image

                };

                return itemData;
            }

        }

        //Stock Item List
        public List<Add_ItemVM> Stocklist()

        {
            var stockItems = DB.Tbl_AddItem.Include(x => x.AddProduct).Include(x => x.Add_Sub_ProductModel).Include(x => x.PartiesModel).ToList();

            var itemViewModels = stockItems.Select(item => new Add_ItemVM
            {
                ItemId = item.ItemId,
                P_Id = item.P_Id,
                S_P_Id = item.S_P_Id,
                Party_Id = item.PartyId ?? 0, // Agar PartyId null hai to 0 set kare
                Item_Name = item.Item_Name,
                partyName = item.PartiesModel?.PartyName,
                Gst_No = item.PartiesModel?.GstNo,
                Item_SerialNumber = item.Item_SerialNumber,
                Item_Quantity = item.Item_Quantity,
                Item_Price = item.Item_Price,
                Item_Selling_Price = item.Item_Selling_Price,
                Item_Expiry_Date = item.Item_Expiry_Date,
                //Image= item.Image,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                ProductCategory = item.AddProduct?.Product_Category,
                SubProduct = item.Add_Sub_ProductModel?.Sub_Product_Name
            }).ToList();

            return itemViewModels;
        }


        //Delete Stock Item 
        public string? DeleteStockItem(int id)
        {
            var item = DB.Tbl_AddItem.FirstOrDefault(x => x.ItemId == id);

            if (item != null)
            {
                var imagePath = item.Image;

                DB.Tbl_AddItem.Remove(item);
                DB.SaveChanges();

                return imagePath; // Return the image path
            }
            else
            {
                return null; // Indicate that no item was found
            }
        }



        //Add Parties
        public string AddParties(PartiesModel model)
        {
            //var partyEmail = DB.Tbl_AddParties.Where(x => x.Email == model.Email || x.PhoneNo == model.PhoneNo).FirstOrDefault();
            //if (partyEmail != null)
            //{
            //    return "AlredyExit";
            //}

            if (model.PartyId > 0)
            {
                var party = DB.Tbl_AddParties.Where(x => x.PartyId == model.PartyId).FirstOrDefault();


                if (party != null)
                {
                    party.PartyName = model.PartyName;
                    party.GstNo = model.GstNo;
                    party.PhoneNo = model.PhoneNo;
                    party.Email = model.Email;
                    party.Address = model.Address;
                    party.StoreName = model.StoreName;
                    party.UpdatedDate = DateTime.Now;

                    // Saving the updated party
                    DB.Tbl_AddParties.Update(party);
                    DB.SaveChanges();

                    return "Updatesuccess";
                }
                else
                {
                    return "NotUpdate";
                }

            }
            else
            {

                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = null;
                DB.Tbl_AddParties.Add(model);
                DB.SaveChanges();
                return "AddSuccess";


            }

        }

        //Get Parties List
        public List<AddPartiesVM> GetPartiesList()
        {
            var PartiesList = DB.Tbl_AddParties.ToList();
            var SubproductVM = PartiesList.Select(p => new AddPartiesVM
            {
                PartyId = p.PartyId,
                PartyName = p.PartyName,
                GstNo = p.GstNo,
                Email = p.Email,
                PhoneNo = p.PhoneNo,
                Address = p.Address,
                StoreName = p.StoreName,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate,

            }).ToList();
            return SubproductVM;
        }

        //delete Parties
        public bool DeleteParties(int Id)
        {
            var party = DB.Tbl_AddParties.Find(Id);
            if (party != null)
            {
                DB.Tbl_AddParties.Remove(party);
                DB.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        //Add Customer
        public bool AddCustomer(CustomerModel model)
        {
            var chekcustomer = DB.Tbl_Customer.Where(x => x.Cust_Mobile_No == model.Cust_Mobile_No || x.Cust_Email == model.Cust_Email).FirstOrDefault();
            if (chekcustomer != null)
            {
                return false;
            }
            else
            {
                model.Created_Date = DateTime.Now;
                model.Updated_Date = null;
                DB.Tbl_Customer.Add(model);
                DB.SaveChanges();
                return true;
            }

        }


















        //Get Item List Form Search Item DDl
        public List<SelectListItem> GetItemsList(int SubProductId)
        {
            var items = new List<SelectListItem>();

            if (SubProductId > 0)
            {
                items = DB.Tbl_AddItem
                    .Where(x => x.S_P_Id == SubProductId)
                    .Select(x => new SelectListItem
                    {
                        Value = x.ItemId.ToString(),
                        Text = x.Item_Name
                    })
                    .ToList();
            }

            return items;
        }
        //Get Sub Product DDl----------------
        public List<SelectListItem> GetSubDDl(int CategoryId)
        {
            var subcategories = new List<SelectListItem>();

            if (CategoryId > 0)
            {
                subcategories = DB.Tbl_Add_SubProduct
                    .Where(x => x.P_Id == CategoryId)
                    .Select(x => new SelectListItem
                    {
                        Value = x.Sub_P_Id.ToString(),
                        Text = x.Sub_Product_Name
                    })
                    .ToList();
            }

            return subcategories;
        }

        //get DDl Category-
        public List<SelectListItem> DDLGetAllCategories()
        {
            var categories = DB.Tbl_Add_Product.Select(x => new SelectListItem
            {
                Value = x.P_Id.ToString(),
                Text = x.Product_Category
            }).ToList();

            return categories;
        }

        // Parties DDl __-
        public List<SelectListItem> GetPartiesDDl()
        {
            var parties = DB.Tbl_AddParties.Select(x => new SelectListItem
            {
                Value = x.PartyId.ToString(),
                Text = x.PartyName
            }).ToList();

            return parties;
        }

        public string GetGstNo(int partyid)
        {
            var GstNo = DB.Tbl_AddParties.Where(x => x.PartyId == partyid).Select(x => x.GstNo).FirstOrDefault();

            if (GstNo != null)
            {
                return GstNo.ToString();
            }
            else
            {
                return "";  // Return empty string if GST number is not found
            }
        }


    }

}
