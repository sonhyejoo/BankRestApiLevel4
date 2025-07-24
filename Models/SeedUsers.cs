using Microsoft.EntityFrameworkCore;

namespace BankRestApi.Models;

public static class SeedUsers
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AccountContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<AccountContext>>()))
        {
            if (context?.Users == null)
            {
                throw new ArgumentNullException("Null Users table");
            }

            // Look for any movies.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.AddRange(
                new User
                {
                    AccountName = "Admin",
                    HashedPassword = ""                
                }
            );
            context.SaveChanges();
        }
    }
}