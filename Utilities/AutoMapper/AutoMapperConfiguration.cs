using AutoMapper;
using BookstoreAPI.Model;
using Entities;

namespace BookstoreAPI.Utilities.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration() : this("MyProfile")
        {

        }

        public AutoMapperConfiguration(string profileName) : base(profileName)
        {
            CreateMap<BooksViewModel, Books>().ReverseMap();
            CreateMap<PicturesViewModel, Pictures>().ReverseMap();
            CreateMap<PictureBooksViewModel, PictureBooks>().ReverseMap();
            CreateMap<AuthorsViewModel, Authors>().ReverseMap();
            CreateMap<LanguagesViewModel, Languages>().ReverseMap();
            CreateMap<ClassTypesViewModel, ClassTypes>().ReverseMap();
            CreateMap<CategoryTypesViewModel, CategoryTypes>().ReverseMap();
            CreateMap<CartViewModel, Carts>().ReverseMap();
        }
    }
}
