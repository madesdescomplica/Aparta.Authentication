using Aparta.Authentication.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.Account.GetAccount;

public interface IGetAccount 
    : IRequestHandler<GetAccountInput, AccountModelOutput>
{ }
