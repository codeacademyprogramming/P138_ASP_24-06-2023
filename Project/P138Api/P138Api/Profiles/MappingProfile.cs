using AutoMapper;
using P138Api.DTOs.AuthDTOs;
using P138Api.DTOs.CategoriesDtos;
using P138Api.Entities;

namespace P138Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryListDto>()
                /*.ForMember(des=>des.MehsulSayi, src=>src.MapFrom(s=>s.Products.Count()))*/;
            CreateMap<Category,CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>()
                .ForMember(des=>des.Name,src=>src.MapFrom(s=>s.Adi))
                .ForMember(des=>des.IsMain,src=>src.MapFrom(s=>s.UstCategoriyadirmi))
                .ForMember(des=>des.ParentId,src=>src.MapFrom(s=>(s.UstCategoriyasi <= 0 || s.UstCategoriyasi == null) ? null : s.UstCategoriyasi));

            CreateMap<CategoryPutDto, Category>();

            CreateMap<RegisterDto, AppUser>();
        }
    }
}