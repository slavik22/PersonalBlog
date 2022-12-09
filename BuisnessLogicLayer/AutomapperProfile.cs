using AutoMapper;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;

namespace BuisnessLogicLayer;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Tag, TagModel>()
            .ReverseMap();

        CreateMap<Post, PostModel>()
            .ForMember(pm => pm.AuthorName, p => p.MapFrom(x => $"{x.User.Name} {x.User.Surname}"));

        CreateMap<PostModel, Post>();

            
        CreateMap<Comment, CommentModel>()
            .ReverseMap();

        CreateMap<User, UserModel>()
            .ReverseMap();
        
        CreateMap<Category, CategoryModel>()
            .ReverseMap();
    }
}
