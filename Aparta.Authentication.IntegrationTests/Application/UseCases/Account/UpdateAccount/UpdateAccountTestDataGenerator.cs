namespace Aparta.Authentication.IntegrationTests.Application.UseCases.Account.UpdateAccount;

public class UpdateAccountTestDataGenerator
{
    public static IEnumerable<object[]> GetAccountsToUpdate(int times = 10)
    {
        var fixture = new UpdateAccountTestFixture();

        for (int indice = 0; indice < times; indice++)
        {
            var clientType = fixture.GetRandomClientType();
            var account = fixture.GetValidAccount(clientType);
            var input = fixture.GetValidInput(account.Id);

            yield return new object[] {
                account, 
                input
            };
        }
    }
}