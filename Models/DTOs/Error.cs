namespace BankRestApi.Models.DTOs;

public class Error(string message)
{
    public string Message { get; } = message;
    public static Error NonpositiveAmount() =>
        new Error("Please enter valid decimal amount greater than zero.");
    public static Error NotFound() =>
        new Error("No account found with that ID.");
    public static Error InsufficientFunds() =>
        new Error("Insufficient funds.");
    public static Error EmptyName() =>
        new Error("Name cannot be empty or whitespace.");
    public static Error DuplicateId() =>
        new Error("Duplicate ids given for sender and recipient.");
    public static Error InternalServerError() =>
        new Error("The server was unable to complete your request. Please try again later.");
}