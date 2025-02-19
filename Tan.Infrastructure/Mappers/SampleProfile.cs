using AutoMapper;
using Tan.Application.Dtos;
using Tan.Domain.Models;

namespace Tan.Infrastructure.Mappers;

public class SampleProfile : Profile
{
    public SampleProfile()
    {
        CreateSampleProfile();
    }

    private void CreateSampleProfile()
    {
        CreateMap<SampleRequestDto, Domain.Entities.Sample>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.Modified, opt => opt.Ignore());

        CreateMap<Domain.Entities.Sample, SampleResponseDto>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((source, destination) => { destination.Name = source.Name; });

        CreateMap<SampleFilterDto, SampleFilter>();

        CreateMap<Pagination<Domain.Entities.Sample>, PaginationDto<SampleResponseDto>>()
            .AfterMap((source, converted, context) =>
            {
                converted.Result = context.Mapper.Map<List<SampleResponseDto>>(source.Result);
            });
    }
}