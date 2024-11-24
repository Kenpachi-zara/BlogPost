using AutoMapper;
using BlogPost.DTOs;
using BlogPost.Models;
using BlogPost.Models.ViewModels;
using BlogPost.PostRequests;
using BlogPost.ViewsModels;

namespace BlogPost.Mappers
{
    public class PostMapper : Profile {
        public PostMapper()
        {
            CreateMap<PostReadDto, Post>();
            CreateMap<PostEditDto, Post>();
            CreateMap<Post, PostEditDto>();
            CreateMap<Post, PostViewDto>();
            CreateMap<PostCreateRequest, PostCreateViewModel>();
            CreateMap<PostEditRequest, PostEditViewModel>();
            CreateMap<PostRequest, PostReadDto>();
            CreateMap<EditRequest, PostEditDto>();
            CreateMap<PostPageRequest, PostIndexViewModel>()
            .ForMember(d => d.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(d => d.Topic, opt => opt.MapFrom(src => src.Topic))
            .ForMember(d => d.SortBy, opt => opt.MapFrom(src => src.SortBy));
            CreateMap<Topic, CheckBox>()
            .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(d => d.ItemId, opt => opt.MapFrom(src => src.TopicId));
        }       
    }
}