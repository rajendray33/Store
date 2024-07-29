namespace StoreManagment.ViewModel
{
    public class ProductListViewModel
    {
        public IEnumerable<AddProductVM> ProductsList { get; set; }
        public AddProductVM NewProduct { get; set; }
    }
}
