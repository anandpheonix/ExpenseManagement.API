using DataAccess.Models;
using DataTransfer.Objects;
using Riok.Mapperly.Abstractions;

namespace Application.Mappings;

[Mapper]
public partial class MapperlyMapper
{
    public partial Transactions Map(TransactionDTO transactionDTO);
}
