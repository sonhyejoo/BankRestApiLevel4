namespace BankRestApi.Models.DTOs;

public enum ErrorCode
{
    NonnegativeOrInvalidAmount = 1001,
    InsufficientFunds = 1002,
    EmptyName = 1003,
    DuplicateId = 1004,
    NotFound = 1005
}