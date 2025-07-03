namespace BankRestApi.Models.DTOs;

public record Error(ErrorCode? Code, string Message);