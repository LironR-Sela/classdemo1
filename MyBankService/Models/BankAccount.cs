using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBankService.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public Owner Owner { get; set; }
        public double Amount { get; set; }
    }
}
