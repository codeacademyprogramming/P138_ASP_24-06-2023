namespace P138Mentor.Models
{
    public class Pricing : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsAdvanced { get; set; }

        public List<PricingService> PricingServices { get; set; }
    }
}
