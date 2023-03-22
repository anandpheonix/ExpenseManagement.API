using AutoMapper;
using DataAccess.Models;
using DataTransfer.Objects;

namespace Application.Mappings;

public class TransactionMapping: Profile
{
    public TransactionMapping()
    {
        CreateMap<Transactions, TransactionDTO>()
        .ForMember(dest => dest.TransactionId, options => options.MapFrom(src => src.Id));
    }
}
