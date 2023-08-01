using FluentValidation;
using P138Api.DAL;

namespace P138Api.DTOs.CategoriesDtos
{
    public class CategoryPutDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int? ParentId { get; set; }
        public IFormFile? File { get; set; }
    }

    public class CategoryPutDtoValidator : AbstractValidator<CategoryPutDto>
    {
        public CategoryPutDtoValidator(AppDbContext context)
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Null ve Ya Bos Ola Bilmez")
                .MaximumLength(50).WithMessage("Maksimum 50 Simvol")
                .MinimumLength(2).WithMessage("Minimum 2 Simvol Olmalidir");


            RuleFor(r => r).Custom((r, ctx) =>
            {
                if (context.Categories.Any(c => c.IsDeleted == false && c.Name.ToLower() == r.Name.Trim().ToLower()))
                {
                    ctx.AddFailure("Adi", "Bu Adda Categoriya Movcuddur");
                    return;
                }

                if (r.IsMain)
                {
                    if (r.File == null)
                    {
                        ctx.AddFailure("File", "File Mutleq Secilmelidir");
                        return;
                    }

                    if (!r.File.ContentType.Contains("image/"))
                    {
                        ctx.AddFailure("File", "File Mutleq Sekil Olmalidir");
                    }

                    if ((r.File.Length / 1024) > 100)
                    {
                        ctx.AddFailure("File", "File  100 kb az olmalidir");
                    }

                    r.ParentId = null;
                }
                else
                {
                    if (r.ParentId == null)
                    {
                        ctx.AddFailure("UstCategoriyasi", "Ust Categoriyasi Mutleq Olmalidir");
                        return;
                    }

                    if (!context.Categories.Any(c => c.IsDeleted == false && c.Id == r.ParentId))
                    {
                        ctx.AddFailure("UstCategoriyasi", "Ust Categoriyasi Duzgun Secin");
                    }

                    r.File = null;
                }
            });
        }
    }
}
