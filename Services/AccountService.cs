using System.Net;
using BankRestApi.ExtensionMethods;
using BankRestApi.Models;
using BankRestApi.Models.DTOs;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Account = BankRestApi.Models.DTOs.Account;

namespace BankRestApi.Services;

public class AccountService : IAccountService
{
    private readonly AccountContext _context;

    public AccountService(AccountContext context)
    {
        _context = context;
    }

    public async Task<AccountResult<Account>> Create(CreateAccountRequest request)
    {
        var name = request.Name;
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            return AccountResult<Account>.Failure(Error.EmptyName());
        }

        var accountToAdd = new Models.Account
        {
            Id = Guid.NewGuid(),
            Name = name,
            Balance = 0
        };
        
        _context.Accounts.Add(accountToAdd);
        await _context.SaveChangesAsync();

        return accountToAdd.CreateResult();
    }

    public async Task<AccountResult<Account>> Get(GetAccountRequest request)
    {
        var foundAccount = await  _context.Accounts.FindAsync(request.Id);

        if (foundAccount is null)
        {
            return AccountResult<Account>.Failure(Error.NotFound());
        }
        
        return foundAccount.CreateResult();
    }
    
    public async Task<AccountResult<Account>> Deposit(TransactionRequest request)
    {
        var foundAccount = await  _context.Accounts.FindAsync(request.Id);

        if (foundAccount is null)
        {
            return AccountResult<Account>.Failure(Error.NotFound());
        }

        if (request.Amount <= 0)
        {
            return AccountResult<Account>.Failure(Error.NonpositiveAmount());
        }
        
        foundAccount.Balance += request.Amount;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return AccountResult<Account>.Failure(Error.InternalServerError());
        }
        
        return foundAccount.CreateResult();
    }
    
    public async Task<AccountResult<Account>> Withdraw(TransactionRequest request)
    {
        var foundAccount = await  _context.Accounts.FindAsync(request.Id);

        if (foundAccount is null)
        {
            return AccountResult<Account>.Failure(Error.NotFound());
        }

        if (request.Amount <= 0)
        {
            return AccountResult<Account>.Failure(Error.NonpositiveAmount());
        }
        
        if (request.Amount > foundAccount.Balance)
        {
            return AccountResult<Account>.Failure(Error.InsufficientFunds());
        }
        
        foundAccount.Balance -= request.Amount;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return AccountResult<Account>.Failure(Error.InternalServerError());
        }
        
        return foundAccount.CreateResult();
    }
    
    public async Task<AccountResult<TransferDetails>> Transfer(TransactionRequest request)
    {
        var (amount, senderId, recipientId) = request;

        if (senderId == recipientId)
        {
            return AccountResult<TransferDetails>.Failure(Error.DuplicateId());
        }
        
        var sender = await  _context.Accounts.FindAsync(senderId);
        var recipient = await  _context.Accounts.FindAsync(recipientId);

        if (sender is null  || recipient is null)
        {
            return AccountResult<TransferDetails>.Failure(Error.NotFound());
        }

        if (amount <= 0)
        {
            return AccountResult<TransferDetails>.Failure(Error.NonpositiveAmount());
        }
        
        if (amount > sender.Balance)
        {
            return AccountResult<TransferDetails>.Failure(Error.InsufficientFunds());
        }
        
        sender.Balance -= request.Amount;
        recipient.Balance +=  request.Amount;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return AccountResult<TransferDetails>.Failure(Error.InternalServerError());
        }
        var senderDto = new Account
        {
            Id = sender.Id,
            Name = sender.Name,
            Balance = sender.Balance
        };
        var recipientDto = new Account
        {
            Id = recipient.Id,
            Name = recipient.Name,
            Balance = recipient.Balance
        };
        
        return AccountResult<TransferDetails>.Success(new TransferDetails(senderDto, recipientDto));
    }
}