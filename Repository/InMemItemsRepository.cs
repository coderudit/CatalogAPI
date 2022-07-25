using CatalogAPI.Models;

namespace CatalogAPI.Repository
{
    public class InMemItemsRepository : IRepository
    {
        private readonly List<Item> items = new() {
            new Item{Id = Guid.NewGuid(), Name ="Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow},
            new Item{Id = Guid.NewGuid(), Name ="Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow},
            new Item{Id = Guid.NewGuid(), Name ="Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow}
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item? GetItem(Guid id)
        {
            return items.SingleOrDefault(x => x.Id == id);
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            int itemIndex = items.FindIndex(x => item.Id == x.Id);
            items[itemIndex] = item; 
        }

        public void DeleteItem(Guid id)
        {
            int itemIndex = items.FindIndex(x => x.Id == id );
            items.RemoveAt(itemIndex);
        }
    }
}
