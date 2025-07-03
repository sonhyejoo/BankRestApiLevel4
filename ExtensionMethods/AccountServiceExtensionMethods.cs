using BankRestApi.Models.DTOs;

namespace BankRestApi.ExtensionMethods;

public static class AccountServiceExtensionMethods
{
    public static Error GreaterThanZeroError(this AccountResult<Account> accountResult) =>
        new Error(ErrorCode.NonnegativeOrInvalidAmount, "Please enter valid decimal amount greater than zero.");
    public static Error NotFoundError(this AccountResult<Account> accountResult) =>
        new Error(ErrorCode.NotFound, "No account found with that ID.");
    public static Error InsufficientFundsError(this AccountResult<Account> accountResult) =>
        new Error(ErrorCode.InsufficientFunds, "Insufficient funds.");
    public static Error EmptyNameError(this AccountResult<Account> accountResult) =>
        new Error(ErrorCode.EmptyName, "Name cannot be empty or whitespace.");
    public static Error DuplicateIdError(this AccountResult<Account> accountResult) =>
        new Error(ErrorCode.DuplicateId, "Duplicate ids given for sender and recipient.");
}