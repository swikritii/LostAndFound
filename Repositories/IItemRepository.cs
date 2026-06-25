using LostAndFound.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Repositories
{
    public interface IItemRepository 
    {
        Task<List<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddAsync(Item item);
        Task DeleteAsync(Item item);
    }
}
