using MyBankService.Data;
using MyBankService.Infra;
using MyBankService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyBankService.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankAccountContext _dbContext;
        public BankAccountService(BankAccountContext dbContext)
        {
            _dbContext = dbContext;

            if (!_dbContext.Accounts.Any())
            {
                _dbContext.Accounts.Add(new BankAccount()
                {
                    Id = 111,
                    Amount = 1700000,
                    Owner = new Owner()
                    {
                        Id = 1,
                        FirstName = "User1",
                        LastName = "AAA",
                        EmailAddress = "user1@gmail.com",
                        Password = HashMD5("123456").Result
                    }
                });

                _dbContext.Accounts.Add(new BankAccount()
                {
                    Id = 222,
                    Amount = -150,
                    Owner = new Owner()
                    {
                        Id = 2,
                        FirstName = "User2",
                        LastName = "BBB",
                        EmailAddress = "user2@gmail.com",
                        Password = HashMD5("777").Result
                    }
                });

                _dbContext.SaveChanges();
            }
        }

        public async Task<string> HashMD5(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        public async Task<bool> VerifyMD5(string password, string originalHashedPassword)
        {
            string hashOfInput = await HashMD5(password);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, originalHashedPassword))
                return true;
            else
                return false;
        }


        public async Task<BankAccount> Login(UserLoginInfo loginDetails)
        {
            var selectedAccount = (from account in _dbContext.Accounts
                                where VerifyMD5(loginDetails.Password, account.Owner.Password).Result &&
                                account.Owner.EmailAddress == loginDetails.EmailAddress
                                select account).SingleOrDefault();

            return selectedAccount;
        }

        public async Task<double> UpdateAmount(int accountId, double amount)
        {
            try
            {
                var selectedUser = _dbContext.Accounts.SingleOrDefault(a => a.Id == accountId);
                if (selectedUser != null)
                {
                    selectedUser.Amount += amount;
                    await _dbContext.SaveChangesAsync();
                    return selectedUser.Amount;
                }
                else
                    throw new Exception("User not found!!");

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
