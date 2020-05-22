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
    }
}
