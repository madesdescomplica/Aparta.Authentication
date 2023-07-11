using ApplicationUseCase = Aparta.Authentication.Application.UseCases.Account.DeleteAccount;
using Aparta.Authentication.Application.UseCases.Account.DeleteAccount;

using Aparta.Authentication.Infra.Data.EF;
using Aparta.Authentication.Infra.Data.EF.Repositories;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.DeleteAccount;

[Collection(nameof(DeleteAccountTestFixture))]
public class DeleteAccountTest
{
    private readonly DeleteAccountTestFixture _fixture;

    public DeleteAccountTest(DeleteAccountTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Should_DeleteAccount_With_Correct_Id))]
    [Trait("Integration/Application", "DeleteAccount - UseCases")]
    public async Task Should_DeleteAccount_With_Correct_Id()
    {
        var clientType = _fixture.GetRandomClientType();
        var accountExample = _fixture.GetValidAccount(clientType);
        var dbContext = _fixture.CreateDbContext();
        var exampleList = _fixture.GetExampleAccountsList(10);
        await dbContext.AddRangeAsync(exampleList);
        var tracking = await dbContext.AddAsync(accountExample);
        await dbContext.SaveChangesAsync();
        tracking.State = EntityState.Detached;
        var repository = new AccountRepository(dbContext);
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCase.DeleteAccount(
            repository, 
            unitOfWork
        );
        var input = new DeleteAccountInput(accountExample.Id);

        await useCase.Handle(input, CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var dbAccountDeleted = await assertDbContext
            .Accounts
            .FindAsync(accountExample.Id);
        var dbAccounts = await assertDbContext
            .Accounts
            .ToListAsync();

        dbAccountDeleted.Should().BeNull();
        dbAccounts.Should().HaveCount(exampleList.Count);
    }
}
