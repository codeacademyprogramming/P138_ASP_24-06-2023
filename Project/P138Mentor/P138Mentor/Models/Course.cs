namespace P138Mentor.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
    }
}
