namespace CodingTest_BTSid.Dto
{
    public class ChecklistItemDto
    {
        public class ChecklistItemDetail
        {
            public int Id { get; set; }
            public string ItemName { get; set; }
            public bool IsCompleted { get; set; }
        }

        public class ChecklistItemCreation
        {
            public string ItemName { get; set; }
        }

        public class ChecklistItemUpdate
        {
            public string ItemName { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}
