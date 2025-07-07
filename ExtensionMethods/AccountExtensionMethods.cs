using BankRestApi.Models.DTOs;

namespace BankRestApi.ExtensionMethods;

public static class AccountExtensionMethods
{
    public static AccountResult<Account> CreateResult(this BankRestApi.Models.Account account) =>
        AccountResult<Account>.Success(
            new Account
            {
                Id = account.Id,
                Name = account.Name,
                Balance = account.Balance
            }
        );
}