using Aparta.Authentication.UseCases.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.UseCases.UseCases.Account.GetAccount;

public interface IGetAccount 
    : IRequestHandler<GetAccountInput, AccountModelOutput>
{ }
