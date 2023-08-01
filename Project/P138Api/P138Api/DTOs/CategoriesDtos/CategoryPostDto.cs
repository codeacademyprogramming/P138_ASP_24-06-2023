using FluentValidation;
using Microsoft.EntityFrameworkCore;
using P138Api.DAL;

namespace P138Api.DTOs.CategoriesDtos
{
    public class CategoryPostDto
    {
        /// <summary>
        /// Kategoriyanin Adi
        /// </summary>
        public string Adi { get; set; }
        /// <summary>
        /// Kategoriya Status
        /// </summary>
        public bool UstCategoriyadirmi { get; set; }
        public int? UstCategoriyasi { get; set; }
        public IFormFile? File { get; set; }
    }

    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidator(AppDbContext context)
        {
            RuleFor(r => r.Adi)
                .NotEmpty().WithMessage("Null ve Ya Bos Ola Bilmez")
                .MaximumLength(50).WithMessage("Maksimum 50 Simvol")
                .MinimumLength(2).WithMessage("Minimum 2 Simvol Olmalidir");


            RuleFor(r => r).Custom((r, ctx) =>
            {
                if (context.Categories.Any(c => c.IsDeleted == false && c.Name.ToLower() == r.Adi.Trim().ToLower()))
                {
                    ctx.AddFailure("Adi", "Bu Adda Categoriya Movcuddur");
                    return;
                }

                if (r.UstCategoriyadirmi)
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

                    r.UstCategoriyasi = null;
                }
                else
                {
                    if (r.UstCategoriyasi == null)
                    {
                        ctx.AddFailure("UstCategoriyasi", "Ust Categoriyasi Mutleq Olmalidir");
                        return;
                    }

                    if (!context.Categories.Any(c=>c.IsDeleted == false && c.Id == r.UstCategoriyasi))
                    {
                        ctx.AddFailure("UstCategoriyasi", "Ust Categoriyasi Duzgun Secin");
                    }

                    r.File = null;
                }
            });
        }
    }
}