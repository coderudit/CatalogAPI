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
        private readonly IItemsRepository _repository;

        public ItemsController(IItemsRepository repository)
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

        //PUT /items
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto updateItem)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem is null)
                return NotFound();

            var updatedItem = existingItem with
            {
                Name = updateItem.Name,
                Price = updateItem.Price
            };

            _repository.UpdateItem(updatedItem);

            return NoContent();
        }

        //DELETE /items
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem is null)
                return NotFound();

            _repository.DeleteItem(id);

            return NoContent();
        }
    }
}
