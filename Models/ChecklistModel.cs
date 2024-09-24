using System.ComponentModel.DataAnnotations;

namespace CodingTest_BTSid.Models
{
    public class ChecklistModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }

        public ICollection<ChecklistItemModel> ToDoItems { get; set; } = new List<ChecklistItemModel>();
    }
}
