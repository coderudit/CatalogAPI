using CatalogAPI.Dtos;
using CatalogAPI.Models;

namespace CatalogAPI.Common
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }

        public static Item AsModel(this CreateItemDto item)
        {
            return new Item
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Price = item.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
