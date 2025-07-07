namespace BankRestApi.Models.DTOs;

public enum ErrorCode
{
    EmptyName = 1001,
    NonpositiveAmount = 1002,
    InsufficientFunds = 1003,
    DuplicateId = 1004,
    NotFound = 1404,
    InternalServerError = 1500,
}