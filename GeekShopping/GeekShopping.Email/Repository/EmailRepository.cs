using Microsoft.EntityFrameworkCore;
using GeekShopping.Email.Data;
using GeekShopping.Email.Messages;
using GeekShopping.Email.Models;

namespace GeekShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<ApplicationDbContext> _context;

        public EmailRepository(DbContextOptions<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task LogEmail(PaymentResultDTO paymentResultDTO)
        {
            EmailLog emailLog = new EmailLog()
            {
                Email = paymentResultDTO.Email,
                SentDate = DateTime.Now,
                Log = $"Order - {paymentResultDTO.OrderId} has been created successfully.",
            };

            await using var _database = new ApplicationDbContext(_context);
            _database.Emails.Add(emailLog);
            await _database.SaveChangesAsync();
        }
    }
}
