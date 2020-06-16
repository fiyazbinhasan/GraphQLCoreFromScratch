using Microsoft.EntityFrameworkCore;
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
    }
}
