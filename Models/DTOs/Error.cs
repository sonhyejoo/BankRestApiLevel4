namespace BankRestApi.Models.DTOs;

public class Error(ErrorCode code, string message)
{
    public string Message { get; } = message;
    public static Error NonpositiveAmount() =>
        new Error(ErrorCode.NonpositiveAmount, "Please enter valid decimal amount greater than zero.");
    public static Error NotFound() =>
        new Error(ErrorCode.NotFound,"No account found with that ID.");
    public static Error InsufficientFunds() =>
        new Error(ErrorCode.InsufficientFunds,"Insufficient funds.");
    public static Error EmptyName() =>
        new Error(ErrorCode.EmptyName,"Name cannot be empty or whitespace.");
    public static Error DuplicateId() =>
        new Error(ErrorCode.DuplicateId,"Duplicate ids given for sender and recipient.");
    public static Error InternalServerError() =>
        new Error(ErrorCode.InternalServerError,"The server was unable to complete your request. Please try again later.");
}