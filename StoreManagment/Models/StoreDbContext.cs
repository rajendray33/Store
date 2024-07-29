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
    }
}
