namespace P138Mentor.Models
{
    public class PricingService : BaseEntity
    {
        public int PricingId { get; set; }
        public Pricing Pricing { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
