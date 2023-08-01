using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P138Alup.DataAccessLayer;
using P138Alup.Models;

namespace P138Alup.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            IQueryable<Product> products = _context.Products
                .Include(b => b.Brand)
                .Include(b => b.Category)
                .Include(b => b.ProductTags.Where(pt=>pt.IsDeleted == false)).ThenInclude(pt=>pt.Tag)
                .Where(b => b.IsDeleted == false)
                .OrderByDescending(b => b.Id);

            int totalPages = (int)Math.Ceiling((decimal)products.Count() / 3);
            ViewBag.TotalPages = totalPages;
            ViewBag.PageIndex = pageIndex;
            return View(products.Skip((pageIndex - 1) * 3).Take(3).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(c=>c.IsDeleted == false).ToListAsync();
            ViewBag.Brands = await _context.Brands.Where(c=>c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(c=>c.IsDeleted == false).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Brands = await _context.Brands.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(c => c.IsDeleted == false).ToListAsync();

            if(!ModelState.IsValid) return View(product);

            if(product.BrandId == null)
            {
                ModelState.AddModelError("BrandId", "Mutleq Secilmelidir");
                return View(product);
            }

            if(!await _context.Brands.AnyAsync(c => c.IsDeleted == false && c.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Is InCorrect");
                return View(product);
            }

            if (product.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Mutleq Secilmelidir");
                return View(product);
            }

            if (!await _context.Categories.AnyAsync(c => c.IsDeleted == false && c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Is InCorrect");
                return View(product);
            }

            if (product.Images != null && product.Images.Count() > 5)
            {
                ModelState.AddModelError("Images", "Maksimum 5 sekil Olmalidir");
                return View(product);
            }

            if (product.MainFile != null)
            {
                if (!product.MainFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("MainFile", "File Type Is InCorrect");
                    return View();
                }

                if ((product.MainFile.Length / 1024) > 100)
                {
                    ModelState.AddModelError("MainFile", "File Size Can Be Max 100 kb");
                    return View();
                }

                string fileName = DateTime.UtcNow.AddHours(4).ToString("yyyyMMddHHmmssfff") + product.MainFile.FileName.Substring(product.MainFile.FileName.LastIndexOf('.'));

                string fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", fileName);

                using (FileStream fileStream = new FileStream(fullpath, FileMode.Create))
                {
                    await product.MainFile.CopyToAsync(fileStream);
                }

                product.MainImage = fileName;
            }
            else
            {
                ModelState.AddModelError("MainFile", "Sekil Mutleq Olmalidir");
                return View(product);
            }

            if (product.HoverFile != null)
            {
                if (!product.HoverFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("HoverFile", "File Type Is InCorrect");
                    return View();
                }

                if ((product.HoverFile.Length / 1024) > 100)
                {
                    ModelState.AddModelError("HoverFile", "File Size Can Be Max 100 kb");
                    return View();
                }

                string fileName = DateTime.UtcNow.AddHours(4).ToString("yyyyMMddHHmmssfff") + product.HoverFile.FileName.Substring(product.MainFile.FileName.LastIndexOf('.'));

                string fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", fileName);

                using (FileStream fileStream = new FileStream(fullpath, FileMode.Create))
                {
                    await product.HoverFile.CopyToAsync(fileStream);
                }

                product.HoverImage = fileName;
            }
            else
            {
                ModelState.AddModelError("HoverFile", "Sekil Mutleq Olmalidir");
                return View(product);
            }

            List<ProductImage> productImages = new List<ProductImage>();

            if (product.Images != null)
            {
                foreach (IFormFile file in product.Images)
                {
                    if (!file.ContentType.Contains("image/"))
                    {
                        ModelState.AddModelError("HoverFile", "File Type Is InCorrect");
                        return View();
                    }

                    if ((file.Length / 1024) > 100)
                    {
                        ModelState.AddModelError("HoverFile", "File Size Can Be Max 100 kb");
                        return View();
                    }

                    string fileName = DateTime.UtcNow.AddHours(4).ToString("yyyyMMddHHmmssfff") + file.FileName.Substring(file.FileName.LastIndexOf('.'));

                    string fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", fileName);

                    using (FileStream fileStream = new FileStream(fullpath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    ProductImage productImage = new ProductImage { 
                        Image = fileName,
                        CreatedAt = DateTime.UtcNow.AddHours(4),
                        CreatedBy = "System"
                    };

                    productImages.Add(productImage);
                }
            }
            else
            {
                ModelState.AddModelError("Images", "Minimum 1 sekil Olmalidir");
                return View(product);
            }

            if (product.TagIds == null)
            {
                ModelState.AddModelError("TagIds", "Mutleq Secilmelidir");
                return View(product);
            }
           
            List<ProductTag> productTags = new List<ProductTag>();

            foreach (int tagId in product.TagIds)
            {
                if (!await _context.Tags.AnyAsync(c => c.IsDeleted == false && c.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "Is InCorrect");
                    return View(product);
                }

                ProductTag productTag = new ProductTag
                {
                    TagId = tagId,
                    CreatedBy = "System",
                    CreatedAt = DateTime.UtcNow.AddHours(4)
                };

                productTags.Add(productTag);
            }

            string? seria = $"{_context.Categories.FirstOrDefault(c => c.Id == product.CategoryId).Name.Substring(0, 2).ToLowerInvariant()}{_context.Brands.FirstOrDefault(c => c.Id == product.CategoryId).Name.Substring(0, 2).ToLowerInvariant()}";
            int? code = _context.Products.OrderBy(p=>p.Id).LastOrDefault(p => p.Seria == seria) != null ? _context.Products.OrderBy(p => p.Id).LastOrDefault(p => p.Seria == seria).Code + 1 : 1;

            product.Seria = seria;
            product.Code = code;
            product.CreatedAt = DateTime.UtcNow.AddHours(4);
            product.CreatedBy = "System";

            product.ProductTags = productTags;
            product.ProductImages = productImages;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id == null) return BadRequest();

            Product product = await _context.Products
                .Include(p=>p.ProductImages.Where(pi=>pi.IsDeleted == false))
                .Include(p=>p.ProductTags)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);

            if(product == null) return NotFound();

            ViewBag.Categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Brands = await _context.Brands.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(c => c.IsDeleted == false).ToListAsync();

            product.TagIds = product.ProductTags?.Select(c => (int)c.TagId).ToList();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product)
        {
            ViewBag.Categories = await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Brands = await _context.Brands.Where(c => c.IsDeleted == false).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(c => c.IsDeleted == false).ToListAsync();

            if(id == null) return BadRequest();

            if (product.Id != id) return BadRequest();

            Product dbproduct = await _context.Products
               .Include(p => p.ProductImages.Where(pi => pi.IsDeleted == false))
               .Include(p => p.ProductTags)
               .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);

            int imageCount = int.Parse(_context.Settings.FirstOrDefault(s => s.Key == "ImageCount").Value);

            int canUpload = imageCount - dbproduct.ProductImages.Count();

            if (product.Images != null && product.Images.Count() > canUpload)
            {
                ModelState.AddModelError("Images", $"Maksimum {canUpload} qeder Sekil Elave Ede Bilersen");
                return View(dbproduct);
            }

            if (dbproduct == null) return NotFound();

            if(!ModelState.IsValid) return View(product);

            if (product.BrandId == null)
            {
                ModelState.AddModelError("BrandId", "Mutleq Secilmelidir");
                return View(product);
            }

            if (!await _context.Brands.AnyAsync(c => c.IsDeleted == false && c.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Is InCorrect");
                return View(product);
            }

            if (product.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Mutleq Secilmelidir");
                return View(product);
            }

            if (!await _context.Categories.AnyAsync(c => c.IsDeleted == false && c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Is InCorrect");
                return View(product);
            }

            if (product.MainFile != null)
            {
                if (!product.MainFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("MainFile", "File Type Is InCorrect");
                    return View();
                }

                if ((product.MainFile.Length / 1024) > 100)
                {
                    ModelState.AddModelError("MainFile", "File Size Can Be Max 100 kb");
                    return View();
                }

                string fileName = DateTime.UtcNow.AddHours(4).ToString("yyyyMMddHHmmssfff") + product.MainFile.FileName.Substring(product.MainFile.FileName.LastIndexOf('.'));

                string fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", dbproduct.MainImage);

                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }

                fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", fileName);

                using (FileStream fileStream = new FileStream(fullpath, FileMode.Create))
                {
                    await product.MainFile.CopyToAsync(fileStream);
                }

                dbproduct.MainImage = fileName;
            }

            if (product.HoverFile != null)
            {
                if (!product.HoverFile.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("HoverFile", "File Type Is InCorrect");
                    return View();
                }

                if ((product.HoverFile.Length / 1024) > 100)
                {
                    ModelState.AddModelError("HoverFile", "File Size Can Be Max 100 kb");
                    return View();
                }

                string fileName = DateTime.UtcNow.AddHours(4).ToString("yyyyMMddHHmmssfff") + product.HoverFile.FileName.Substring(product.MainFile.FileName.LastIndexOf('.'));

                string fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", dbproduct.HoverImage);

                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }

                fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", fileName);

                using (FileStream fileStream = new FileStream(fullpath, FileMode.Create))
                {
                    await product.HoverFile.CopyToAsync(fileStream);
                }

                dbproduct.HoverImage = fileName;
            }

            List<ProductImage> productImages = new List<ProductImage>();

            if (product.Images != null)
            {
                foreach (IFormFile file in product.Images)
                {
                    if (!file.ContentType.Contains("image/"))
                    {
                        ModelState.AddModelError("HoverFile", "File Type Is InCorrect");
                        return View();
                    }

                    if ((file.Length / 1024) > 100)
                    {
                        ModelState.AddModelError("HoverFile", "File Size Can Be Max 100 kb");
                        return View();
                    }

                    string fileName = DateTime.UtcNow.AddHours(4).ToString("yyyyMMddHHmmssfff") + file.FileName.Substring(file.FileName.LastIndexOf('.'));

                    string fullpath = Path.Combine(_env.WebRootPath, "assets", "images", "product", fileName);

                    using (FileStream fileStream = new FileStream(fullpath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    ProductImage productImage = new ProductImage
                    {
                        Image = fileName,
                        CreatedAt = DateTime.UtcNow.AddHours(4),
                        CreatedBy = "System"
                    };

                    productImages.Add(productImage);
                }
            }

            dbproduct.ProductImages.AddRange(productImages);

            if (product.TagIds != null)
            {
                //_context.ProductTags.RemoveRange(dbproduct.ProductTags);

                foreach (ProductTag productTag in dbproduct.ProductTags)
                {
                    productTag.IsDeleted = true;
                }

                List<ProductTag> productTags = new List<ProductTag>();

                foreach (int tagId in product.TagIds)
                {
                    if (!await _context.Tags.AnyAsync(c => c.IsDeleted == false && c.Id == tagId))
                    {
                        ModelState.AddModelError("TagIds", "Is InCorrect");
                        return View(product);
                    }

                    ProductTag productTag = new ProductTag
                    {
                        TagId = tagId,
                        CreatedBy = "System",
                        CreatedAt = DateTime.UtcNow.AddHours(4)
                    };

                    productTags.Add(productTag);
                }

                dbproduct.ProductTags.AddRange(productTags);
            }

            dbproduct.Title = product.Title.Trim();
            dbproduct.Description = product.Description;
            dbproduct.Price = product.Price;
            dbproduct.DiscountedPrice = product.DiscountedPrice;
            dbproduct.ExTax = product.ExTax;
            dbproduct.BrandId = product.BrandId;
            dbproduct.CategoryId = product.CategoryId;
            dbproduct.Count = product.Count;
            dbproduct.IsBestSeller = product.IsBestSeller;
            dbproduct.IsFeatured = product.IsFeatured;
            dbproduct.IsNewArrival = product.IsNewArrival;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage(int? id,int? productId)
        {
            if (id == null) return BadRequest();

            if (productId == null) return BadRequest();

            Product product = await _context.Products
                .Include(p=>p.ProductImages.Where(pi=>pi.IsDeleted == false))
                .FirstOrDefaultAsync(p => p.Id == productId && p.IsDeleted == false);

            if(product == null) return NotFound();

            if (product.ProductImages == null)
            {
                return BadRequest();
            }

            if (product.ProductImages.Count() < 2)
            {
                return BadRequest();
            }

            if (!product.ProductImages.Any(pi=>pi.Id == id && pi.IsDeleted == false))
            {
                return NotFound();
            }

            string filePath = Path.Combine(_env.WebRootPath, "assets", "images", "product", product.ProductImages.FirstOrDefault(p => p.Id == id).Image);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            product.ProductImages.FirstOrDefault(p => p.Id == id).IsDeleted = true;

            await _context.SaveChangesAsync();

            return PartialView("_ProductImagePartial", product.ProductImages.Where(pi => pi.IsDeleted == false).ToList());
        }
    }
}
