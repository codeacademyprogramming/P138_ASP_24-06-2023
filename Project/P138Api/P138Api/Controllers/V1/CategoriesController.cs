using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P138Api.DAL;
using P138Api.DTOs.CategoriesDtos;
using P138Api.Entities;

namespace P138Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates an Employee.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/v1/Categories
        ///     {        
        ///       "adi": "Mike",
        ///       "ustCategoriyadirmi": false,
        ///       "ustCategoriyasi": 5,
        ///       "file":""
        ///     }
        /// </remarks>
        /// <param name="categoryPostDto"></param>
        /// <returns>A newly created employee</returns>
        /// <response code="201">Returns the newly created item Id</response>
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CategoryPostDto categoryPostDto)
        {

            //Category category = new Category
            //{
            //    Name = categoryPostDto.Adi.Trim(),
            //    IsMain = categoryPostDto.UstCategoriyadirmi,
            //    ParentId = categoryPostDto.UstCategoriyasi
            //};

            Category category = _mapper.Map<Category>(categoryPostDto);

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, category.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            #region Old Mapping
            //List<CategoryListDto> categoryListDtos = await _context.Categories
            //    .Where(c=>c.IsDeleted == false)
            //    .Select(x=>new CategoryListDto 
            //    { 
            //        Name = x.Name,
            //        IsMain = x.IsMain,
            //        Id = x.Id
            //    })
            //    .ToListAsync();
            #endregion

            #region Old Old Mapping
            //List<CategoryListDto> categoryListDtos = new List<CategoryListDto>();

            //foreach (Category category in categories) 
            //{
            //    CategoryListDto categoryListDto = new CategoryListDto
            //    {
            //        Id = category.Id,
            //        IsMain = category.IsMain,
            //        Name = category.Name,
            //    };

            //    categoryListDtos.Add(categoryListDto);
            //}
            #endregion

            List<Category> categories = await _context.Categories.Include(c=>c.Products).Where(c=>c.IsDeleted == false).ToListAsync();

            List<CategoryListDto> categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);
            return Ok(categoryListDtos);
        }

        [HttpGet]
        [Route("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();

            Category category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == id && c.IsDeleted == false);

            if (category == null) return NotFound();

            //CategoryGetDto categoryGetDto = new CategoryGetDto
            //{
            //    Id = category.Id,
            //    Name = category.Name,
            //    IsMain = category.IsMain,
            //    Image = category.Image
            //};

            CategoryGetDto categoryGetDto = _mapper.Map<CategoryGetDto>(category);

            return Ok(categoryGetDto);
        }

        [HttpPut]
        public async Task<IActionResult> Put( CategoryPutDto categoryPutDto)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id == categoryPutDto.Id && c.IsDeleted == false);

            category.IsMain = categoryPutDto.IsMain;
            category.ParentId = categoryPutDto.ParentId;
            category.Name = categoryPutDto.Name;

            //category = _mapper.Map<Category>(categoryPutDto);

            //_context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
