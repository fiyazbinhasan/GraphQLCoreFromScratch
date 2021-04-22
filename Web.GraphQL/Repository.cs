using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class Repository : IRepository
    {
        private ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<Item> GetItemByTag(string tag)
        {
            return _applicationDbContext.Items.FirstAsync(i => i.Tag.Equals(tag));
        }

        public async Task<IReadOnlyCollection<Item>> GetItems()
        {
            return await _applicationDbContext.Items.ToListAsync();
        }

        public async Task<Item> AddItem(Item item)
        {
            var addedEntity = await _applicationDbContext.Items.AddAsync(item);
            await _applicationDbContext.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task<IReadOnlyCollection<Order>> GetOrders()
        {
            return await _applicationDbContext.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyCollection<Customer>> GetCustomers()
        {
            return await _applicationDbContext.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _applicationDbContext.Customers.FindAsync(customerId);
        }

        public async Task<IReadOnlyCollection<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _applicationDbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }

        public async Task<Order> AddOrder(Order order)
        {
            var addedOrder = await _applicationDbContext.Orders.AddAsync(order);
            await _applicationDbContext.SaveChangesAsync();
            return addedOrder.Entity;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            var addedCustomer = await _applicationDbContext.Customers.AddAsync(customer);
            await _applicationDbContext.SaveChangesAsync();
            return addedCustomer.Entity;
        }

        public async Task<Item> GetItemById(int itemId)
        {
            return await _applicationDbContext.Items.FindAsync(itemId);
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _applicationDbContext.Orders.FindAsync(orderId);
        }

        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            var addedOrderItem = await _applicationDbContext.OrderItem.AddAsync(orderItem);
            await _applicationDbContext.SaveChangesAsync();
            return addedOrderItem.Entity;
        }

        public async Task<IReadOnlyCollection<OrderItem>> GetOrderItem()
        {
            return await _applicationDbContext.OrderItem.AsNoTracking().ToListAsync();
        }

        public async Task<IDictionary<int, Customer>> GetCustomersById(IEnumerable<int> ids, CancellationToken token)
        {
            return await _applicationDbContext.Customers.Where(c => ids.Contains(c.CustomerId)).ToDictionaryAsync(c => c.CustomerId);
        }

        public async Task<ILookup<int, Order>> GetOrdersByCustomerId(IEnumerable<int> customerIds, CancellationToken cancellationToken)
        {
            var orders = await _applicationDbContext.Orders.Where(i => customerIds.Contains(i.CustomerId)).ToListAsync();
            return orders.ToLookup(i => i.CustomerId);
        }
    }
}