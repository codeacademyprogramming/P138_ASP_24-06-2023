namespace P138Mentor.Models
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstragramUrl { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public int? CategoryId { get; set; }


        public Category? Category { get; set; }
    }
}
