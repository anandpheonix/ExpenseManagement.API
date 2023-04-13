using Application.Controllers;
using AutoMapper;
using DataAccess.Repositories;
using FluentAssertions;
using NSubstitute;

namespace Tests.ControllerTests;
public class TransactionsControllerTests
{
    private readonly TransactionsController transactionsController;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public TransactionsControllerTests()
    {
        _transactionRepository = Substitute.For<ITransactionRepository>();
        _mapper = Substitute.For<IMapper>();
        _categoryRepository = Substitute.For<ICategoryRepository>();
        transactionsController = new(_transactionRepository, _mapper, _categoryRepository);
    }

    [Fact]
    public async Task GetTransactions_Should_RetrieveTransactions()
    {
        var transactionData = DataGenerator.GetTransactionsData(10);

        _transactionRepository.GetTransactions(cancellationToken: CancellationToken.None)
            .Returns(transactionData);

        var result = await transactionsController.GetTransactions(cancellationToken: CancellationToken.None);

        result.Should().NotBeNull();
    }
}
