using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using WebApi.Dto;

namespace WebApi.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<QuizItem, QuizItemDto>().ForMember(
                target => target.Options,
                op => op.MapFrom(
                    source => new List<string>(source.IncorrectAnswers) { source.CorrectAnswer }
                    )
                );


            CreateMap<Quiz, QuizDto>().ForMember(

                target => target.Items,
                op => op.MapFrom<List<QuizItem>>(s => s.Items)
                );
        }
    }
}
