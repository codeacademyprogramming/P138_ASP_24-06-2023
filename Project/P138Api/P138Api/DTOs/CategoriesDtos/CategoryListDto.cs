namespace P138Api.DTOs.CategoriesDtos
{
    public class CategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int ProductsCount { get; set; }
    }
}
