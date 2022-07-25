using CatalogAPI.Models;
using CatalogAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository _repository;

        public ItemsController(IRepository repository)
        {
            _repository = repository;
        }

        //GET /items
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            var items = _repository.GetItems();
            if (items is null)
                return NotFound();
            return Ok(items);
        }

        //GET /items/id
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id) {
            var item = _repository.GetItem(id);
            if (item is null)
                return NotFound();
            return Ok(_repository.GetItem(id));
        }
    }
}
