using AutoMapper;
using Count.App.Models;
using Count.Models;
using Microsoft.CodeAnalysis;

namespace Count.App.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostBindingModel>();
            CreateMap<PostBindingModel, Post>();

            CreateMap<User, User>();

            CreateMap<BmiUser, BmiUser>();

            CreateMap<Day, Day>();

            CreateMap<Meal, Meal>();

            CreateMap<Food, Food>();
        }
    }
}
