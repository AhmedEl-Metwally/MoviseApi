using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace MoviseApi.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, opt=>opt.Ignore());

        }

    }
}
