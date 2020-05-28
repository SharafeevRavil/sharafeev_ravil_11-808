using AutoMapper;
using SocialNetwork.Models;

namespace SocialNetwork.AutoMapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<AddedPost, Post>();
        }
    }
}