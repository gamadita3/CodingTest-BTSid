using CodingTest_BTSid.Dto;
using CodingTest_BTSid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodingTest_BTSid.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/checklist/{checklistId}/[controller]")]
    public class ChecklistItemController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ChecklistItemController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllItems(int checklistId)
        {
            var items = _context.ChecklistItems
                .Where(i => i.ChecklistId == checklistId)
                .Select(i => new ChecklistItemDto.ChecklistItemDetail
                {
                    Id = i.Id,
                    ItemName = i.ItemName,
                    IsCompleted = i.IsCompleted
                })
                .ToList();

            return Ok(items);
        }

        [HttpPost]
        public IActionResult CreateItem(int checklistId, [FromBody] ChecklistItemDto.ChecklistItemCreation itemDto)
        {
            var checklist = _context.Checklists.Find(checklistId);
            if (checklist == null) return NotFound("Checklist could not be found");

            var item = new ChecklistItemModel
            {
                ChecklistId = checklistId,
                ItemName = itemDto.ItemName,
                IsCompleted = false
            };

            _context.ChecklistItems.Add(item);
            _context.SaveChanges();

            return Ok(new ChecklistItemDto.ChecklistItemDetail
            {
                Id = item.Id,
                ItemName = item.ItemName,
                IsCompleted = item.IsCompleted
            });
        }

        [HttpPut("{checklistItemId}")]
        public IActionResult UpdateItem(int checklistId, int checklistItemId, [FromBody] ChecklistItemDto.ChecklistItemUpdate itemDto)
        {
            var existingItem = _context.ChecklistItems.FirstOrDefault(i => i.Id == checklistItemId && i.ChecklistId == checklistId);
            if (existingItem == null) return NotFound("Checklist item could not be found");

            existingItem.ItemName = itemDto.ItemName;
            existingItem.IsCompleted = itemDto.IsCompleted;
            _context.SaveChanges();

            return Ok(new ChecklistItemDto.ChecklistItemDetail
            {
                Id = existingItem.Id,
                ItemName = existingItem.ItemName,
                IsCompleted = existingItem.IsCompleted
            });
        }

        [HttpDelete("{checklistItemId}")]
        public IActionResult DeleteItem(int checklistId, int checklistItemId)
        {
            var item = _context.ChecklistItems.FirstOrDefault(i => i.Id == checklistItemId && i.ChecklistId == checklistId);
            if (item == null) return NotFound("Checklist item could not be found");

            _context.ChecklistItems.Remove(item);
            _context.SaveChanges();

            return Ok(new { message = "Checklist item deleted successfully" });
        }
    }
}