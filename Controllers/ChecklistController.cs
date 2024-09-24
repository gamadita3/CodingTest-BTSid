using CodingTest_BTSid.Dto;
using CodingTest_BTSid.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodingTest_BTSid.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ChecklistController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllChecklists()
        {
            var userId = int.Parse(User.FindFirst("sub").Value);
            var checklists = _context.Checklists
                .Where(c => c.UserId == userId)
                .Select(c => new ChecklistDto.ChecklistDetail
                {
                    Id = c.Id,
                    Name = c.Name,
                    Items = c.ToDoItems.Select(i => new ChecklistItemDto.ChecklistItemDetail
                    {
                        Id = i.Id,
                        ItemName = i.ItemName,
                        IsCompleted = i.IsCompleted
                    }).ToList()
                }).ToList();

            return Ok(checklists);
        }

        [HttpPost]
        public IActionResult CreateChecklist([FromBody] ChecklistDto.ChecklistCreation checklistDto)
        {
            var userId = int.Parse(User.FindFirst("sub").Value);

            var checklist = new ChecklistModel
            {
                Name = checklistDto.Name,
                UserId = userId
            };

            _context.Checklists.Add(checklist);
            _context.SaveChanges();

            return Ok(new ChecklistDto.ChecklistDetail
            {
                Id = checklist.Id,
                Name = checklist.Name,
                Items = new List<ChecklistItemDto.ChecklistItemDetail>()
            });
        }

        [HttpDelete("{checklistId}")]
        public IActionResult DeleteChecklist(int checklistId)
        {
            var checklist = _context.Checklists.Find(checklistId);
            if (checklist == null) return NotFound("Checklist could not be found");

            _context.Checklists.Remove(checklist);
            _context.SaveChanges();

            return Ok(new { message = "Checklist deleted successfully" });
        }
    }
}
