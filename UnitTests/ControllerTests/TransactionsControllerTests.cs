using Application.Controllers;
using AutoMapper;
using DataAccess.Repositories;

namespace Tests.ControllerTests;

public class TransactionsControllerTests
{
    #region Global Object Declaration

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

    #endregion

    [Fact]
    public async Task GetTransactions_Should_RetrieveTransactions()
    {
        #region mock dependencies
        var transactionData = DataGenerator.TransactionsData(10);

        _transactionRepository
            .GetTransactions(cancellationToken: CancellationToken.None)
            .Returns(transactionData);
        #endregion

        var result = await transactionsController.GetTransactions(CancellationToken.None);

        result.Should().NotBeNull();

    }
}
