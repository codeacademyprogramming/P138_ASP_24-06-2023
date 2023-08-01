namespace P138Api.Entities
{
    public class Product : BaseEntity
    {
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
