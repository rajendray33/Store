using Microsoft.EntityFrameworkCore;

namespace StoreManagment.Models
{
    public class StoreDbContext : DbContext
    {

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<RegistrationModel> Tbl_Registration { get; set; } = null!;
        public DbSet<AddProduct> Tbl_Add_Product { get; set; } = null!;
        public DbSet<Add_Sub_ProductModel> Tbl_Add_SubProduct { get; set; } = null!;
        public DbSet<Add_ItemModel> Tbl_AddItem { get; set; } = null!;
        public DbSet<PartiesModel> Tbl_AddParties { get; set; } = null!;
        public DbSet<CustomerModel> Tbl_Customer { get; set; } = null!;
    }
}
