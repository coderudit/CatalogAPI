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
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
        {
            var items = await _repository.GetItemsAsync();
            if (items is null)
                return NotFound();

            var itemsDto = items.Select(item => item.AsDto());

            return Ok(itemsDto);
        }

        //GET /items/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item is null)
                return NotFound();

            return Ok(item.AsDto());
        }

        //POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto createItem)
        {
            var item = createItem.AsModel();
            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        //PUT /items
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto updateItem)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if (existingItem is null)
                return NotFound();

            var updatedItem = existingItem with
            {
                Name = updateItem.Name,
                Price = updateItem.Price
            };

           await _repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        //DELETE /items
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if (existingItem is null)
                return NotFound();

            await _repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
