using AutoMapper;
using DataAccess.Models;
using DataTransfer.Objects;
using DataTransfer.Requests;

namespace Application.Mappings;

public class TransactionMapping: Profile
{
    public TransactionMapping()
    {
        CreateMap<Transactions, TransactionDTO>()
        .ForMember(dest => dest.TransactionId, options => options.MapFrom(src => src.Id));
    }
}
