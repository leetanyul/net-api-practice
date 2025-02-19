using AutoMapper;
using Tan.Application.Dtos;
using Tan.Domain.Models;

namespace Tan.Infrastructure.Mappers;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateAccountProfile();
    }

    private void CreateAccountProfile()
    {
        CreateMap<UserInfoDto, UserInfoFilter>();

        CreateMap<AcoountDto, AcoountFilter>();

        CreateMap<AccountSubkeyDto, AccountSubkeyFilter>();

        CreateMap<string, ApiResponseDto<string>>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(_ => 200)) // 기본 성공 코드
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => string.IsNullOrEmpty(src) ? "No data" : "Success"))
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src));
    }
}


