using System.Linq;
using AutoMapper;
using CoreApplication.API.DTOs;
using CoreApplication.Data.DataEntities;

namespace CoreApplication.API.Helpers
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles()
        {
            // map non existing photourl in source with main pictures url
         CreateMap<LoginUser,UserForListDto>()
            .ForMember(dest=>dest.PhotoUrl, opt=> {
                opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
            })
            .ForMember(dest=>dest.Age,opt=> {
                opt.ResolveUsing(src=>src.DateOfBirth.CalculateAge());
            });   
         CreateMap<LoginUser,UserForDetailedDto>()
         .ForMember(dest=>dest.PhotoUrl, opt=> {
                opt.MapFrom(src=>src.Photos.FirstOrDefault(p=>p.IsMain).Url);
            })
            .ForMember(dest=>dest.Age,opt=> {
                opt.ResolveUsing(src=>src.DateOfBirth.CalculateAge());
            });    
          CreateMap<Photo,PhotosForDetailedDto>(); 
          CreateMap<UserForUpdateDto,LoginUser>();
          CreateMap<PhotoForCreationDto,Photo>(); 
        }
        
    }
}