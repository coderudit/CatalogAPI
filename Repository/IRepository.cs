using CatalogAPI.Models;

namespace CatalogAPI.Repository
{
    public interface IRepository
    {
        IEnumerable<Item> GetItems();

        Item? GetItem(Guid id);

        void CreateItem(Item item);
    }
}
