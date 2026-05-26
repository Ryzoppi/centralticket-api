using CentralTicket.Contexts.Billing.Data;
using CentralTicket.Contexts.Billing.Entities;
using CentralTicket.Contexts.Billing.Interfaces.IRepositories;

namespace CentralTicket.Contexts.Billing.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly BillingDbContext _database;

        public SaleRepository(BillingDbContext database)
        {
            _database = database;
        }

        public List<Sale> List()
        {
            List<Sale> sales = _database.Sales.Select(sales => sales).ToList();

            return sales;
        }

        public Sale GetById(Guid id)
        {
            Sale sale = _database.Sales.Select(sale => sale).Where(sale => sale.Id == id).FirstOrDefault();

            return sale;
        }

        public void Create(Sale newSale)
        {
            _database.Sales.Add(newSale);
            _database.SaveChanges();
        }

        public void Update(Sale sale)
        {
            _database.Set<Sale>().Update(sale);
            _database.SaveChanges();
        }
    }
}
