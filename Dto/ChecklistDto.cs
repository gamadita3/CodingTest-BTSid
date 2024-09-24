namespace CodingTest_BTSid.Dto
{
    public class ChecklistDto
    {
        public class ChecklistDetail
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<ChecklistItemDto.ChecklistItemDetail> Items { get; set; }
        }

        public class ChecklistCreation
        {
            public string Name { get; set; }
        }

        public class ChecklistUpdate
        {
            public string Name { get; set; }
        }
    }
}
