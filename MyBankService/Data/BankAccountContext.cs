using Microsoft.EntityFrameworkCore;
using MyBankService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBankService.Data
{
    public class BankAccountContext : DbContext
    {
        public DbSet<BankAccount> Accounts { get; set; }

        public BankAccountContext(DbContextOptions options) : base(options) 
        {

        }
    }
}
