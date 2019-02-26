using MyBankService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBankService.Infra
{
    public interface IBankAccountService
    {
        Task<BankAccount> Login(UserLoginInfo loginDetails);

        Task<string> HashMD5(string password);

        Task<bool> VerifyMD5(string password, string originalHashedPassword);


        Task<double> UpdateAmount(int accountId, double amount);
    }
}
