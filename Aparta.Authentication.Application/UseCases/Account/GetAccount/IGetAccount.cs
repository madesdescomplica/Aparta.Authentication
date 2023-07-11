using Aparta.Authentication.Application.UseCases.Account.Common;

using MediatR;

namespace Aparta.Authentication.Application.UseCases.Account.GetAccount;

public interface IGetAccount 
    : IRequestHandler<GetAccountInput, AccountModelOutput>
{ }
