using Bogus;
using DataAccess.Models;

namespace Tests;

public static class DataGenerator
{
    public static IEnumerable<Transactions> TransactionsData(int count)
    {
        var dataRule = new Faker<Transactions>()
            .RuleFor(x => x.Id, f => f.Random.Number(1, 10))
            .RuleFor(x => x.CategoryId, f => f.Random.Number(1, 10))
            .RuleFor(x => x.Amount, f => (double)f.Finance.Amount(1, 800, 2));

        return dataRule.Generate(count);
    }
}
