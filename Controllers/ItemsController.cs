using CatalogAPI.Common;
using CatalogAPI.Dtos;
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
        public ActionResult<IEnumerable<ItemDto>> GetItems()
        {
            var items = _repository.GetItems();
            if (items is null)
                return NotFound();

            var itemsDto = items.Select(item => item.AsDto());

            return Ok(itemsDto);
        }

        //GET /items/id
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);
            if (item is null)
                return NotFound();

            return Ok(item.AsDto());
        }

        //POST /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItem)
        {
            var item = createItem.AsModel();
            _repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }
    }
}
