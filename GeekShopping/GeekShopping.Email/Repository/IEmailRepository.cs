using GeekShopping.Email.Messages;
using GeekShopping.Email.Models;

namespace GeekShopping.Email.Repository
{
    public interface IEmailRepository
    {
        Task LogEmail(PaymentResultDTO paymentResultDTO);
    }
}
