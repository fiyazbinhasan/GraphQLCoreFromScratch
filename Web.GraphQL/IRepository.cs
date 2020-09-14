using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public interface IRepository
    {
        Task<IReadOnlyCollection<Item>> GetItems();
        Task<Item> GetItemByTag(string tag);
        Task<Item> AddItem(Item item);
        Task<IReadOnlyCollection<Order>> GetOrders();
        Task<IReadOnlyCollection<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<IReadOnlyCollection<Order>> GetOrdersByCustomerId(int customerId);
        Task<Order> AddOrder(Order order);
        Task<Customer> AddCustomer(Customer customer);
        Task<Item> GetItemById(int itemId);
        Task<Order> GetOrderById(int orderId);
        Task<OrderItem> AddOrderItem(OrderItem orderItem);
        Task<IReadOnlyCollection<OrderItem>> GetOrderItem();
        Task<IDictionary<int, Customer>> GetCustomersById(IEnumerable<int> ids, CancellationToken cancellationToken);
        Task<ILookup<int, Order>> GetOrdersByCustomerId(IEnumerable<int> customerIds, CancellationToken cancellationToken);
    }
}
