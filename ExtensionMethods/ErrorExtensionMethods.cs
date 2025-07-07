using BankRestApi.Models.DTOs;

namespace BankRestApi.ExtensionMethods;

public static class ErrorExtensionMethods
{
    public static Error NonnegativeOrInvalidAmount(this Error error) =>
        new Error(ErrorCode.NonnegativeOrInvalidAmount, "Please enter valid decimal amount greater than zero.");
    public static Error NotFound(this Error error) =>
        new Error(ErrorCode.NotFound, "No account found with that ID.");
    public static Error InsufficientFunds(this Error error) =>
        new Error(ErrorCode.InsufficientFunds, "Insufficient funds.");
    public static Error EmptyName(Error error) =>
        new Error(ErrorCode.EmptyName, "Name cannot be empty or whitespace.");
    public static Error DuplicateId(this Error error) =>
        new Error(ErrorCode.DuplicateId, "Duplicate ids given for sender and recipient.");
}