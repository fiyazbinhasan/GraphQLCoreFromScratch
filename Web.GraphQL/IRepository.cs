using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public interface IRepository
    {
        Task<List<Item>> GetItems();
        Task<Item> GetItemByTag(string tag);
        Task<Item> AddItem(Item item);
        Task<List<Order>> GetOrders();
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int customerId);
        Task<List<Order>> GetOrdersByCustomerId(int customerId);
        Task<Order> AddOrder(Order order);
        Task<Customer> AddCustomer(Customer customer);
    }
}
