using System.ComponentModel.DataAnnotations;

namespace CodingTest_BTSid.Models
{
    public class ChecklistItemModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        public bool IsCompleted { get; set; }

        public int ChecklistId { get; set; }

        public ChecklistModel Checklist { get; set; }
    }
}
