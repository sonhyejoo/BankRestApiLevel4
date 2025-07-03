namespace BankRestApi.Models.DTOs;

public enum ErrorCode
{
    NonnegativeOrInvalidAmount,
    InsufficientFunds,
    EmptyName,
    DuplicateId,
    NotFound
}