using CatalogAPI.Models;
using CatalogAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemItemsRepository();
        }

        //GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            return repository.GetItems();
        }

        //GET /items/id
        [HttpGet("{id}")]
        public Item? GetItem(Guid id) {
            return repository.GetItem(id);
        }
    }
}
