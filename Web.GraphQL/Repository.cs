﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}