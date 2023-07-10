using UnitOfWorkInfra = Aparta.Authentication.Infra.Data.EF;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Infra.Data.EF.UnitOfWork;

[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture;

    public UnitOfWorkTest(UnitOfWorkTestFixture testFixture)
        => _fixture = testFixture;

    [Fact(DisplayName = nameof(Should_Call_Commit_With_Correct_Values))]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task Should_Call_Commit_With_Correct_Values()
    {
        var dbContext = _fixture.CreateDbContext();
        var exampleAccountsList = _fixture.GetExampleAccountsList();
        await dbContext.AddRangeAsync(exampleAccountsList);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        await unitOfWork.Commit(CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var savedAccounts = assertDbContext.Accounts
            .AsNoTracking()
            .ToList();

        savedAccounts.Should().HaveCount(exampleAccountsList.Count);
    }
}