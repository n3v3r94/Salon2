
namespace Salon.Services.Implementation
{
    using Salon.Data;

    public class ProductService
    {
        private readonly SalonDbContext db;

        public ProductService(SalonDbContext db)
        {
            this.db = db;
        }

        public void EditProduct(int id)
        {

        }
    }
}
